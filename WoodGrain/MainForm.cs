using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WoodGrain
{
	public partial class FormMain : Form
	{
		private RangedValue<decimal> Temperature;
		private RangedValue<int> LayerSteps;

		private bool SuppressSaving = false;
		private bool SuppressPreview = false;

		private IList<(int, int)> Pattern;

		private OutputType outputType;

		public int PreviewZoom { get; private set; } = 1;

		public const int PreviewTempMin = 150;
		public const int PreviewTempMax = 300;

		public FormMain()
		{
			try
			{
				InitializeComponent();
				Temperature.Min = NumericTemperatureMin.Value;
				Temperature.Max = NumericTemperatureMax.Value;
				LayerSteps.Min = (int)NumericStepsMin.Value;
				LayerSteps.Max = (int)NumericStepsMax.Value;

				try
				{
					SuppressPreview = true;
					LoadFromSettings();
					SetOutputTextbox();
					SuppressPreview = false;
					GetPreview();   // set it once, rather than any time the textbox change
				}
				catch
				{

				}
				finally
				{
					SuppressPreview = false;
				}
			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
				Application.Exit();
			}
		}

		private void LoadFromSettings()
		{
			try
			{
				SuppressSaving = true;
				Temperature.SuppressEvents(true);
				LayerSteps.SuppressEvents(true);

				var settings = Properties.Settings.Default;

				TextBoxFilename.Text = settings.InputPath;
				NumericLayers.Value = settings.Layers;

				if (settings.MinTemp > settings.MaxTemp)
					settings.MinTemp = settings.MaxTemp;
				if (settings.MinSteps > settings.MaxSteps)
					settings.MinSteps = settings.MaxSteps;

				NumericTemperatureMin.Value = settings.MinTemp;
				NumericTemperatureMax.Value = settings.MaxTemp;
				NumericStepsMin.Value = settings.MinSteps;
				NumericStepsMax.Value = settings.MaxSteps;
				NumericStartingLayers.Value = settings.StartingLayers;
				NumericStartingTemp.Value = settings.StartingTemperature;

				outputType = settings.OutputType;

				switch (settings.OutputType)
				{
					case OutputType.Clipboard:
						RadioClipboard.Checked = true;
						break;
					case OutputType.File:
						RadioFile.Checked = true;
						break;
				}
			} finally {
				SuppressSaving = false;
				Temperature.SuppressEvents(false);
				LayerSteps.SuppressEvents(false);
			}
		}

		private void SaveToSettings()
		{
			if (SuppressSaving)
				return;

			var settings = Properties.Settings.Default;
			settings.InputPath = TextBoxFilename.Text;
			settings.Layers = (int)NumericLayers.Value;
			settings.MinTemp = NumericTemperatureMin.Value;
			settings.MaxTemp = NumericTemperatureMax.Value;
			settings.MinSteps = (int)NumericStepsMin.Value;
			settings.MaxSteps = (int)NumericStepsMax.Value;
			settings.StartingTemperature = NumericStartingTemp.Value;
			settings.StartingLayers = (int)NumericStartingLayers.Value;

			if (RadioClipboard.Checked)
				settings.OutputType = OutputType.Clipboard;
			else if (RadioFile.Checked)
				settings.OutputType = OutputType.File;

			settings.Save();
		}

		private void ToggleOutputCheckbox()
		{
			if (RadioClipboard.Checked)
			{
				TextBoxOut.Enabled = false;
				ButtonBrowseOut.Enabled = false;
				RadioFile.Checked = false;
				outputType = OutputType.Clipboard;
			}
			else
			{
				TextBoxOut.Enabled = true;
				ButtonBrowseOut.Enabled = true;
				RadioClipboard.Checked = false;
				SetOutputTextbox();
				outputType = OutputType.File;
			}
		}

		private void SetOutputTextbox()
		{
			if (!TextBoxOut.Enabled)
			{
				TextBoxOut.Text = "";
				return;
			}
			if (!string.IsNullOrWhiteSpace(TextBoxOut.Text))
				return;

			var inputPath = TextBoxFilename.Text.Trim('"');
			if (File.Exists(inputPath))
			{
				var path = Path.GetDirectoryName(inputPath);
				var filename = Path.GetFileNameWithoutExtension(inputPath);
				var extension = Path.GetExtension(inputPath);

				var outputPath = $"{path}\\{filename}_1{extension}";

				TextBoxOut.Text = outputPath;
			}
		}

		private void RadioFile_CheckedChanged(object sender, EventArgs e)
			=> ToggleOutputCheckbox();

		private void RadioClipboard_CheckedChanged(object sender, EventArgs e)
			 => ToggleOutputCheckbox();

		private void ButtonGo_Click(object sender, EventArgs e)
		{
			var outFileName = TextBoxOut.Text.Trim('"');

			// do some very basic sanity validation
			var inFileName = TextBoxFilename.Text.Trim('"');
			if (!File.Exists(inFileName))
			{
				MessageBox.Show(
					$"File {inFileName} not found!", "File not found!",
					MessageBoxButtons.OK, MessageBoxIcon.Error
				);
				return;
			}

			GetPreview();
			if (Pattern == null)
			{
				// If pattern is still null, that means the operation failed somewhere
				MessageBox.Show(
					"Invalid settings!", "",
					MessageBoxButtons.OK, MessageBoxIcon.Error
				);
				return;
			}

			// user is sure and everything is valid -- save settings
			SaveToSettings();

			try
			{
				switch (outputType)
				{
					case OutputType.Clipboard:
						Simplify3DClipboard.Instance.Apply(inFileName, null, Pattern);
						break;
					case OutputType.File:
						Simplify3DXmlFileOutput.Instance.Apply(inFileName, outFileName, Pattern);
						break;
				}
			} catch (System.Xml.XmlException xmlex) {
				MessageBox.Show(
					"Malformed XML\n\n" + xmlex.ToString(), "Unexpected Error!",
					MessageBoxButtons.OK, MessageBoxIcon.Error
				);
			} catch (Exception ex) {
				MessageBox.Show(
					"Something unexpected went wrong.\n\n" + ex.ToString(), "Unexpected Error!",
					MessageBoxButtons.OK, MessageBoxIcon.Error
				);
			}
		}

		private void ButtonBrowseOut_Click(object sender, EventArgs e)
		{
			using (var sfd = new SaveFileDialog())
			{
				sfd.RestoreDirectory = true;
				sfd.Filter = "fff files (*.fff)|*.fff";

				// Try finding initial directory
				var filename = TextBoxOut.Text.Trim('"');
				if (File.Exists(filename))
					sfd.InitialDirectory = Path.GetDirectoryName(filename);

				if (sfd.ShowDialog() == DialogResult.OK)
					TextBoxOut.Text = sfd.FileName;
			}
		}

		private void SetPreviewImage()
		{
			if (Pattern == null)
				return;

			var preview = new LowHighColorInterpPreview();
			preview.Low = Color.LightSalmon; // tbd
			preview.High = Color.Maroon;
			preview.LowTemp = 150;
			preview.HighTemp = 250;
			var bitmap = preview.GetPreviewImage(Pattern, PictureBoxPreview.Width, PreviewZoom, out var backColor);

			SetPreviewImage(bitmap, backColor);
			SetPreviewBackColor(backColor);
		}

		private void GetGrainPattern()
		{
			Pattern = null;

			// for now, only GrainLayerSettings are supported
			var builder = new GrainLayerSettings.Builder
			{
				Layers = (int)NumericLayers.Value,
				TempMin = NumericTemperatureMin.Value,
				TempMax = NumericTemperatureMax.Value,
				StepsMin = (int)NumericStepsMin.Value,
				StepsMax = (int)NumericStepsMax.Value,
				StartTemp = NumericStartingTemp.Value,
				StartLayers = (int)NumericStartingLayers.Value
			};

			if (!builder.TryBuild(out var settings))
				return;

			Pattern = GrainGenerator.Instance.GetLayers(settings).ToList();
		}

		private void GetPreview()
		{
			// todo: generating the numbers should be separate from setting the preview
			if (SuppressPreview)
				return;
			GetGrainPattern();
			Task.Run(() => SetPreviewImage());
		}

		private delegate void SetPreviewImageDelegate(Image image, Color backColor);
		private void SetPreviewImage(Image image, Color backColor)
		{
			if (PictureBoxPreview.InvokeRequired)
				Invoke(new SetPreviewImageDelegate(SetPreviewImage), image, backColor);
			else
			{
				PictureBoxPreview.Image?.Dispose();
				PictureBoxPreview.Image = image;
				PictureBoxPreview.BackColor = backColor;
			}
		}

		private delegate void SetPreviewBackColorDelegate(Color backColor);
		private void SetPreviewBackColor(Color backColor)
		{
			if (PanelPreviewScroll.InvokeRequired)
				Invoke(new SetPreviewBackColorDelegate(SetPreviewBackColor), backColor);
			else
				PanelPreviewScroll.BackColor = backColor;
		}

		private void ButtonBrowse_Click(object sender, EventArgs e)
		{
			using (var ofd = new OpenFileDialog())
			{
				ofd.RestoreDirectory = true;
				ofd.Filter = "fff files (*.fff)|*.fff|All Files (*.*)|*.*";

				// Try finding initial directory
				var filename = TextBoxFilename.Text.Trim('"');
				if (File.Exists(filename))
					ofd.InitialDirectory = Path.GetDirectoryName(filename);

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					TextBoxFilename.Text = ofd.FileName;
					SetOutputTextbox();
				}
			}
		}

		private void NumericTemperatureMin_ValueChanged(object sender, EventArgs e)
		{
			if (Temperature.SuppressMinEvents)
				return;
			if (NumericTemperatureMin.Value <= Temperature.Max)
			{
				Temperature.Min = NumericTemperatureMin.Value;
				GetPreview();
			}
			else try
				{
					Temperature.SuppressMinEvents = true;
					NumericTemperatureMin.Value = Temperature.Min;
				}
				finally
				{
					Temperature.SuppressMinEvents = false;
				}
		}

		private void NumericTemperatureMax_ValueChanged(object sender, EventArgs e)
		{
			if (Temperature.SuppressMaxEvents)
				return;
			if (NumericTemperatureMax.Value >= Temperature.Min)
			{
				Temperature.Max = NumericTemperatureMax.Value;
				GetPreview();
			}
			else try
				{
					Temperature.SuppressMaxEvents = true;
					NumericTemperatureMax.Value = Temperature.Max;
				}
				finally
				{
					Temperature.SuppressMaxEvents = false;
				}
		}

		private void NumericStepsMin_ValueChanged(object sender, EventArgs e)
		{
			if (LayerSteps.SuppressMinEvents)
				return;
			if (NumericStepsMin.Value <= LayerSteps.Max)
			{
				LayerSteps.Min = (int)NumericStepsMin.Value;
				GetPreview();
			}
			else try
				{
					LayerSteps.SuppressMinEvents = true;
					NumericStepsMin.Value = LayerSteps.Min;
				}
				finally
				{
					LayerSteps.SuppressMinEvents = false;
				}
		}

		private void NumericStepsMax_ValueChanged(object sender, EventArgs e)
		{
			if (LayerSteps.SuppressMaxEvents)
				return;
			if (NumericStepsMax.Value >= LayerSteps.Min)
			{
				LayerSteps.Max = (int)NumericStepsMax.Value;
				GetPreview();
			}
			else try
				{
					LayerSteps.SuppressMaxEvents = true;
					NumericStepsMax.Value = LayerSteps.Max;
				}
				finally
				{
					LayerSteps.SuppressMaxEvents = false;
				}
		}

		private void NumericLayers_ValueChanged(object sender, EventArgs e)
			=> GetPreview();

		private void NumericStartingTemp_ValueChanged(object sender, EventArgs e)
			=> GetPreview();

		private void NumericStartingLayers_ValueChanged(object sender, EventArgs e)
			=> GetPreview();

		private struct RangedValue<T> where T : struct
		{
			public T Min { get; set; }
			public T Max { get; set; }
			public bool SuppressMinEvents { get; set; }
			public bool SuppressMaxEvents { get; set; }

			public void SuppressEvents(bool value)
			{
				SuppressMaxEvents = value;
				SuppressMaxEvents = value;
			}
		}
	}
}
