using System;
using System.IO;
using System.Windows.Forms;

namespace WoodGrain
{
	public partial class FormMain : Form
	{
		private RangedValue<decimal> Temperature;
		private RangedValue<int> LayerSteps;

		private bool SuppressSaving = false;

		private OutputType outputType;

		public FormMain()
		{
			InitializeComponent();
			Temperature.Min = NumericTemperatureMin.Value;
			Temperature.Max = NumericTemperatureMax.Value;
			LayerSteps.Min = (int)NumericStepsMin.Value;
			LayerSteps.Max = (int)NumericStepsMax.Value;

			try
			{
				LoadFromSettings();
				SetOutputTextbox();
			} 
			catch 
			{ 
				// Initial settings might be invalid - we don't care.
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
			if (outputType == OutputType.File && File.Exists(outFileName))
			{
				var result = MessageBox.Show(
					$"File {outFileName} exists!\n\nOverwrite?", "Overwrite file?",
					MessageBoxButtons.OKCancel, MessageBoxIcon.Warning
				);
				if (result != DialogResult.OK)
					return;
			}

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

			var builder = new LayerSettings.Builder();
			builder.Layers = (int)NumericLayers.Value;
			builder.TempMin = NumericTemperatureMin.Value;
			builder.TempMax = NumericTemperatureMax.Value;
			builder.StepsMin = (int)NumericStepsMin.Value;
			builder.StepsMax = (int)NumericStepsMax.Value;

			if (!builder.TryBuild(out var settings))
			{
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
				var result = GrainMaker.Apply(inFileName, settings);

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
					"Something unexpected went wrong. Send me a screenshot of the error message.\n\n" + ex.ToString(), "Unexpected Error!",
					MessageBoxButtons.OK, MessageBoxIcon.Error
				);
			}
		}

		private void ButtonBrowseOut_Click(object sender, EventArgs e)
		{
			using (var ofd = new OpenFileDialog())
			{
				ofd.RestoreDirectory = true;
				ofd.Filter = "fff files (*.fff)|*.fff";

				// Try finding initial directory
				var filename = TextBoxOut.Text.Trim('"');
				if (File.Exists(filename))
					ofd.InitialDirectory = Path.GetDirectoryName(filename);

				if (ofd.ShowDialog() == DialogResult.OK)
					TextBoxOut.Text = ofd.FileName;
			}
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
				Temperature.Min = NumericTemperatureMin.Value;
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
				Temperature.Max = NumericTemperatureMax.Value;
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
				LayerSteps.Min = (int)NumericStepsMin.Value;
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
				LayerSteps.Max = (int)NumericStepsMax.Value;
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
