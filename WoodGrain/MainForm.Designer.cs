namespace WoodGrain
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.Label LabelPath;
			System.Windows.Forms.GroupBox GroupBoxSettings;
			System.Windows.Forms.Label LabelStepsMax;
			System.Windows.Forms.Label LabelTempMax;
			System.Windows.Forms.Label LabelStepsMin;
			System.Windows.Forms.Label LabelTempMin;
			System.Windows.Forms.Label LabelSteps;
			System.Windows.Forms.Label LabelTemp;
			System.Windows.Forms.Label LabelLayers;
			System.Windows.Forms.Label LabelPreview;
			System.Windows.Forms.GroupBox GroupBoxOutput;
			this.TextBoxFilename = new System.Windows.Forms.TextBox();
			this.ButtonBrowse = new System.Windows.Forms.Button();
			this.NumericStepsMax = new System.Windows.Forms.NumericUpDown();
			this.NumericStepsMin = new System.Windows.Forms.NumericUpDown();
			this.NumericTemperatureMax = new System.Windows.Forms.NumericUpDown();
			this.NumericTemperatureMin = new System.Windows.Forms.NumericUpDown();
			this.NumericLayers = new System.Windows.Forms.NumericUpDown();
			this.PanelPreviewScroll = new System.Windows.Forms.Panel();
			this.RadioFile = new System.Windows.Forms.RadioButton();
			this.RadioClipboard = new System.Windows.Forms.RadioButton();
			this.TextBoxOut = new System.Windows.Forms.TextBox();
			this.ButtonBrowseOut = new System.Windows.Forms.Button();
			this.ButtonGo = new System.Windows.Forms.Button();
			LabelPath = new System.Windows.Forms.Label();
			GroupBoxSettings = new System.Windows.Forms.GroupBox();
			LabelStepsMax = new System.Windows.Forms.Label();
			LabelTempMax = new System.Windows.Forms.Label();
			LabelStepsMin = new System.Windows.Forms.Label();
			LabelTempMin = new System.Windows.Forms.Label();
			LabelSteps = new System.Windows.Forms.Label();
			LabelTemp = new System.Windows.Forms.Label();
			LabelLayers = new System.Windows.Forms.Label();
			LabelPreview = new System.Windows.Forms.Label();
			GroupBoxOutput = new System.Windows.Forms.GroupBox();
			GroupBoxSettings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumericStepsMax)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericStepsMin)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericTemperatureMax)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericTemperatureMin)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericLayers)).BeginInit();
			GroupBoxOutput.SuspendLayout();
			this.SuspendLayout();
			// 
			// LabelPath
			// 
			LabelPath.AutoSize = true;
			LabelPath.Location = new System.Drawing.Point(13, 9);
			LabelPath.Name = "LabelPath";
			LabelPath.Size = new System.Drawing.Size(32, 13);
			LabelPath.TabIndex = 0;
			LabelPath.Text = "Path:";
			// 
			// TextBoxFilename
			// 
			this.TextBoxFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxFilename.Location = new System.Drawing.Point(16, 30);
			this.TextBoxFilename.Name = "TextBoxFilename";
			this.TextBoxFilename.Size = new System.Drawing.Size(365, 20);
			this.TextBoxFilename.TabIndex = 1;
			// 
			// ButtonBrowse
			// 
			this.ButtonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonBrowse.Location = new System.Drawing.Point(387, 28);
			this.ButtonBrowse.Name = "ButtonBrowse";
			this.ButtonBrowse.Size = new System.Drawing.Size(55, 23);
			this.ButtonBrowse.TabIndex = 2;
			this.ButtonBrowse.Text = "Browse";
			this.ButtonBrowse.UseVisualStyleBackColor = true;
			this.ButtonBrowse.Click += new System.EventHandler(this.ButtonBrowse_Click);
			// 
			// GroupBoxSettings
			// 
			GroupBoxSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			GroupBoxSettings.Controls.Add(this.NumericStepsMax);
			GroupBoxSettings.Controls.Add(this.NumericStepsMin);
			GroupBoxSettings.Controls.Add(this.NumericTemperatureMax);
			GroupBoxSettings.Controls.Add(LabelStepsMax);
			GroupBoxSettings.Controls.Add(this.NumericTemperatureMin);
			GroupBoxSettings.Controls.Add(LabelTempMax);
			GroupBoxSettings.Controls.Add(LabelStepsMin);
			GroupBoxSettings.Controls.Add(this.NumericLayers);
			GroupBoxSettings.Controls.Add(LabelTempMin);
			GroupBoxSettings.Controls.Add(LabelSteps);
			GroupBoxSettings.Controls.Add(LabelTemp);
			GroupBoxSettings.Controls.Add(LabelLayers);
			GroupBoxSettings.Location = new System.Drawing.Point(16, 57);
			GroupBoxSettings.Name = "GroupBoxSettings";
			GroupBoxSettings.Size = new System.Drawing.Size(423, 103);
			GroupBoxSettings.TabIndex = 3;
			GroupBoxSettings.TabStop = false;
			GroupBoxSettings.Text = "Settings";
			// 
			// NumericStepsMax
			// 
			this.NumericStepsMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.NumericStepsMax.Location = new System.Drawing.Point(214, 72);
			this.NumericStepsMax.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.NumericStepsMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NumericStepsMax.Name = "NumericStepsMax";
			this.NumericStepsMax.Size = new System.Drawing.Size(202, 20);
			this.NumericStepsMax.TabIndex = 0;
			this.NumericStepsMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.NumericStepsMax.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.NumericStepsMax.ValueChanged += new System.EventHandler(this.NumericStepsMax_ValueChanged);
			// 
			// NumericStepsMin
			// 
			this.NumericStepsMin.Location = new System.Drawing.Point(117, 72);
			this.NumericStepsMin.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.NumericStepsMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NumericStepsMin.Name = "NumericStepsMin";
			this.NumericStepsMin.Size = new System.Drawing.Size(58, 20);
			this.NumericStepsMin.TabIndex = 0;
			this.NumericStepsMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.NumericStepsMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NumericStepsMin.ValueChanged += new System.EventHandler(this.NumericStepsMin_ValueChanged);
			// 
			// NumericTemperatureMax
			// 
			this.NumericTemperatureMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.NumericTemperatureMax.Location = new System.Drawing.Point(214, 46);
			this.NumericTemperatureMax.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.NumericTemperatureMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NumericTemperatureMax.Name = "NumericTemperatureMax";
			this.NumericTemperatureMax.Size = new System.Drawing.Size(202, 20);
			this.NumericTemperatureMax.TabIndex = 0;
			this.NumericTemperatureMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.NumericTemperatureMax.Value = new decimal(new int[] {
            220,
            0,
            0,
            0});
			this.NumericTemperatureMax.ValueChanged += new System.EventHandler(this.NumericTemperatureMax_ValueChanged);
			// 
			// LabelStepsMax
			// 
			LabelStepsMax.AutoSize = true;
			LabelStepsMax.Location = new System.Drawing.Point(181, 74);
			LabelStepsMax.Name = "LabelStepsMax";
			LabelStepsMax.Size = new System.Drawing.Size(27, 13);
			LabelStepsMax.TabIndex = 0;
			LabelStepsMax.Text = "Max";
			// 
			// NumericTemperatureMin
			// 
			this.NumericTemperatureMin.Location = new System.Drawing.Point(117, 46);
			this.NumericTemperatureMin.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.NumericTemperatureMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NumericTemperatureMin.Name = "NumericTemperatureMin";
			this.NumericTemperatureMin.Size = new System.Drawing.Size(58, 20);
			this.NumericTemperatureMin.TabIndex = 0;
			this.NumericTemperatureMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.NumericTemperatureMin.Value = new decimal(new int[] {
            190,
            0,
            0,
            0});
			this.NumericTemperatureMin.ValueChanged += new System.EventHandler(this.NumericTemperatureMin_ValueChanged);
			// 
			// LabelTempMax
			// 
			LabelTempMax.AutoSize = true;
			LabelTempMax.Location = new System.Drawing.Point(181, 48);
			LabelTempMax.Name = "LabelTempMax";
			LabelTempMax.Size = new System.Drawing.Size(27, 13);
			LabelTempMax.TabIndex = 0;
			LabelTempMax.Text = "Max";
			// 
			// LabelStepsMin
			// 
			LabelStepsMin.AutoSize = true;
			LabelStepsMin.Location = new System.Drawing.Point(87, 74);
			LabelStepsMin.Name = "LabelStepsMin";
			LabelStepsMin.Size = new System.Drawing.Size(24, 13);
			LabelStepsMin.TabIndex = 0;
			LabelStepsMin.Text = "Min";
			// 
			// NumericLayers
			// 
			this.NumericLayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.NumericLayers.Location = new System.Drawing.Point(117, 20);
			this.NumericLayers.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.NumericLayers.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NumericLayers.Name = "NumericLayers";
			this.NumericLayers.Size = new System.Drawing.Size(299, 20);
			this.NumericLayers.TabIndex = 0;
			this.NumericLayers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.NumericLayers.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			// 
			// LabelTempMin
			// 
			LabelTempMin.AutoSize = true;
			LabelTempMin.Location = new System.Drawing.Point(87, 48);
			LabelTempMin.Name = "LabelTempMin";
			LabelTempMin.Size = new System.Drawing.Size(24, 13);
			LabelTempMin.TabIndex = 0;
			LabelTempMin.Text = "Min";
			// 
			// LabelSteps
			// 
			LabelSteps.AutoSize = true;
			LabelSteps.Location = new System.Drawing.Point(6, 74);
			LabelSteps.Name = "LabelSteps";
			LabelSteps.Size = new System.Drawing.Size(63, 13);
			LabelSteps.TabIndex = 0;
			LabelSteps.Text = "Layer Steps";
			// 
			// LabelTemp
			// 
			LabelTemp.AutoSize = true;
			LabelTemp.Location = new System.Drawing.Point(6, 48);
			LabelTemp.Name = "LabelTemp";
			LabelTemp.Size = new System.Drawing.Size(67, 13);
			LabelTemp.TabIndex = 0;
			LabelTemp.Text = "Temperature";
			// 
			// LabelLayers
			// 
			LabelLayers.AutoSize = true;
			LabelLayers.Location = new System.Drawing.Point(6, 22);
			LabelLayers.Name = "LabelLayers";
			LabelLayers.Size = new System.Drawing.Size(38, 13);
			LabelLayers.TabIndex = 0;
			LabelLayers.Text = "Layers";
			// 
			// LabelPreview
			// 
			LabelPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			LabelPreview.AutoSize = true;
			LabelPreview.Location = new System.Drawing.Point(445, 9);
			LabelPreview.Name = "LabelPreview";
			LabelPreview.Size = new System.Drawing.Size(45, 13);
			LabelPreview.TabIndex = 0;
			LabelPreview.Text = "Preview";
			// 
			// PanelPreviewScroll
			// 
			this.PanelPreviewScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PanelPreviewScroll.BackColor = System.Drawing.SystemColors.ControlDark;
			this.PanelPreviewScroll.Location = new System.Drawing.Point(448, 30);
			this.PanelPreviewScroll.Name = "PanelPreviewScroll";
			this.PanelPreviewScroll.Size = new System.Drawing.Size(65, 204);
			this.PanelPreviewScroll.TabIndex = 4;
			// 
			// GroupBoxOutput
			// 
			GroupBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			GroupBoxOutput.Controls.Add(this.RadioFile);
			GroupBoxOutput.Controls.Add(this.RadioClipboard);
			GroupBoxOutput.Controls.Add(this.TextBoxOut);
			GroupBoxOutput.Controls.Add(this.ButtonBrowseOut);
			GroupBoxOutput.Location = new System.Drawing.Point(13, 167);
			GroupBoxOutput.Name = "GroupBoxOutput";
			GroupBoxOutput.Size = new System.Drawing.Size(426, 73);
			GroupBoxOutput.TabIndex = 5;
			GroupBoxOutput.TabStop = false;
			GroupBoxOutput.Text = "Output To...";
			// 
			// RadioFile
			// 
			this.RadioFile.AutoSize = true;
			this.RadioFile.Location = new System.Drawing.Point(87, 20);
			this.RadioFile.Name = "RadioFile";
			this.RadioFile.Size = new System.Drawing.Size(41, 17);
			this.RadioFile.TabIndex = 0;
			this.RadioFile.TabStop = true;
			this.RadioFile.Text = "File";
			this.RadioFile.UseVisualStyleBackColor = true;
			this.RadioFile.CheckedChanged += new System.EventHandler(this.RadioFile_CheckedChanged);
			// 
			// RadioClipboard
			// 
			this.RadioClipboard.AutoSize = true;
			this.RadioClipboard.Location = new System.Drawing.Point(12, 20);
			this.RadioClipboard.Name = "RadioClipboard";
			this.RadioClipboard.Size = new System.Drawing.Size(69, 17);
			this.RadioClipboard.TabIndex = 0;
			this.RadioClipboard.TabStop = true;
			this.RadioClipboard.Text = "Clipboard";
			this.RadioClipboard.UseVisualStyleBackColor = true;
			this.RadioClipboard.CheckedChanged += new System.EventHandler(this.RadioClipboard_CheckedChanged);
			// 
			// TextBoxOut
			// 
			this.TextBoxOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxOut.Location = new System.Drawing.Point(12, 43);
			this.TextBoxOut.Name = "TextBoxOut";
			this.TextBoxOut.Size = new System.Drawing.Size(347, 20);
			this.TextBoxOut.TabIndex = 1;
			// 
			// ButtonBrowseOut
			// 
			this.ButtonBrowseOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonBrowseOut.Location = new System.Drawing.Point(365, 41);
			this.ButtonBrowseOut.Name = "ButtonBrowseOut";
			this.ButtonBrowseOut.Size = new System.Drawing.Size(55, 23);
			this.ButtonBrowseOut.TabIndex = 2;
			this.ButtonBrowseOut.Text = "Browse";
			this.ButtonBrowseOut.UseVisualStyleBackColor = true;
			this.ButtonBrowseOut.Click += new System.EventHandler(this.ButtonBrowseOut_Click);
			// 
			// ButtonGo
			// 
			this.ButtonGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonGo.Location = new System.Drawing.Point(458, 244);
			this.ButtonGo.Name = "ButtonGo";
			this.ButtonGo.Size = new System.Drawing.Size(55, 23);
			this.ButtonGo.TabIndex = 2;
			this.ButtonGo.Text = "Start";
			this.ButtonGo.UseVisualStyleBackColor = true;
			this.ButtonGo.Click += new System.EventHandler(this.ButtonBrowse_Click);
			// 
			// FormMain
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(525, 279);
			this.Controls.Add(GroupBoxOutput);
			this.Controls.Add(this.PanelPreviewScroll);
			this.Controls.Add(GroupBoxSettings);
			this.Controls.Add(this.ButtonGo);
			this.Controls.Add(this.ButtonBrowse);
			this.Controls.Add(this.TextBoxFilename);
			this.Controls.Add(LabelPreview);
			this.Controls.Add(LabelPath);
			this.MinimumSize = new System.Drawing.Size(394, 318);
			this.Name = "FormMain";
			this.Text = "Wood Grain Maker";
			GroupBoxSettings.ResumeLayout(false);
			GroupBoxSettings.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumericStepsMax)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericStepsMin)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericTemperatureMax)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericTemperatureMin)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericLayers)).EndInit();
			GroupBoxOutput.ResumeLayout(false);
			GroupBoxOutput.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox TextBoxFilename;
		private System.Windows.Forms.Button ButtonBrowse;
		private System.Windows.Forms.Panel PanelPreviewScroll;
		private System.Windows.Forms.NumericUpDown NumericLayers;
		private System.Windows.Forms.NumericUpDown NumericTemperatureMin;
		private System.Windows.Forms.NumericUpDown NumericTemperatureMax;
		private System.Windows.Forms.NumericUpDown NumericStepsMax;
		private System.Windows.Forms.NumericUpDown NumericStepsMin;
		private System.Windows.Forms.RadioButton RadioFile;
		private System.Windows.Forms.RadioButton RadioClipboard;
		private System.Windows.Forms.TextBox TextBoxOut;
		private System.Windows.Forms.Button ButtonBrowseOut;
		private System.Windows.Forms.Button ButtonGo;
	}
}

