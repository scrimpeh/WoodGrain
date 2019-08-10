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

		private IList<(int, int)> Grain;

		private OutputType outputType;

		public int PreviewZoom { get; private set; } = 1;

		public const int PreviewTempMin = 150;
		public const int PreviewTempMax = 300;

		public FormMain()
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
			finally
			{
				SuppressPreview = false;
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
			if (Grain == null)
			{
				// If grain is still null, that means the operation failed somewhere
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
				var result = GrainMaker.Apply(inFileName, Grain);

				switch (outputType)
				{
					case OutputType.Clipboard:
						Clipboard.SetText(result);
						break;
					case OutputType.File:
						File.WriteAllText(outFileName, result);
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
			if (Grain == null)
				return;

			var layers = new List<int>();
			for (var i = 0; i < Grain.Count - 1; i++)
			{
				var (layer, temp) = Grain[i];
				var (layerNext, _) = Grain[i + 1];

				for (var j = layer; j < layerNext; j++)
					layers.Add(temp);
			}
			layers.Add(Grain.Last().Item2);

			var width = PictureBoxPreview.Width;
			var height = layers.Count * PreviewZoom;
			var bitmap = new Bitmap(width, height);

			for (var y = 0; y < layers.Count; y++)
			{
				for (var i = 0; i < PreviewZoom; i++)
				{
					var grey = layers[y];
					if (grey > 255)
						grey = 255;
					for (var x = 0; x < bitmap.Width; x++)
					{
						bitmap.SetPixel(x, y * PreviewZoom + i, Color.FromArgb(grey, grey, grey));
					}
				}
			}
			var finalColor = layers[layers.Count - 1];

			if (finalColor > 255)
				finalColor = 255;

			var backColor = Color.FromArgb(finalColor, finalColor, finalColor);
			SetPreviewImage(bitmap, backColor);
			SetPreviewBackColor(backColor);
		}

		private void GetGrain()
		{
			Grain = null;

			var builder = new LayerSettings.Builder();
			builder.Layers = (int)NumericLayers.Value;
			builder.TempMin = NumericTemperatureMin.Value;
			builder.TempMax = NumericTemperatureMax.Value;
			builder.StepsMin = (int)NumericStepsMin.Value;
			builder.StepsMax = (int)NumericStepsMax.Value;

			if (!builder.TryBuild(out var settings))
				return;

			Grain = StepObtainer.Generate(settings).ToList();
		}

		private void GetPreview()
		{
			// todo: generating the numbers should be separate from setting the preview
			if (SuppressPreview)
				return;
			GetGrain();
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
