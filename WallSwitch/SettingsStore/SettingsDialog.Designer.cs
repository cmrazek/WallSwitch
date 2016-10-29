namespace WallSwitch.SettingsStore
{
	partial class SettingsDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
			this.c_startUpDelayLabel = new System.Windows.Forms.Label();
			this.c_startUpDelayTextBox = new System.Windows.Forms.TextBox();
			this.c_startUpDelayUnitsLabel = new System.Windows.Forms.Label();
			this.c_okButton = new System.Windows.Forms.Button();
			this.c_cancelButton = new System.Windows.Forms.Button();
			this.c_startWithWindowsCheckBox = new System.Windows.Forms.CheckBox();
			this.c_checkForUpdatesCheckBox = new System.Windows.Forms.CheckBox();
			this.c_checkForUpdatesButton = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.c_ignoreHiddenFilesCheckbox = new System.Windows.Forms.CheckBox();
			this.c_logLevelLabel = new System.Windows.Forms.Label();
			this.c_logLevelCombo = new System.Windows.Forms.ComboBox();
			this.c_loadHistoryImages = new System.Windows.Forms.CheckBox();
			this.c_tabControl = new System.Windows.Forms.TabControl();
			this.c_startupTab = new System.Windows.Forms.TabPage();
			this.c_imagesTab = new System.Windows.Forms.TabPage();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.c_importRatingsButton = new System.Windows.Forms.Button();
			this.c_exportRatingsButton = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.c_tabControl.SuspendLayout();
			this.c_startupTab.SuspendLayout();
			this.c_imagesTab.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// c_startUpDelayLabel
			// 
			this.c_startUpDelayLabel.AutoSize = true;
			this.c_startUpDelayLabel.Location = new System.Drawing.Point(7, 95);
			this.c_startUpDelayLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.c_startUpDelayLabel.Name = "c_startUpDelayLabel";
			this.c_startUpDelayLabel.Size = new System.Drawing.Size(159, 17);
			this.c_startUpDelayLabel.TabIndex = 6;
			this.c_startUpDelayLabel.Text = "Start-up/Resume Delay:";
			// 
			// c_startUpDelayTextBox
			// 
			this.c_startUpDelayTextBox.Location = new System.Drawing.Point(174, 92);
			this.c_startUpDelayTextBox.Margin = new System.Windows.Forms.Padding(4);
			this.c_startUpDelayTextBox.Name = "c_startUpDelayTextBox";
			this.c_startUpDelayTextBox.Size = new System.Drawing.Size(79, 22);
			this.c_startUpDelayTextBox.TabIndex = 7;
			// 
			// c_startUpDelayUnitsLabel
			// 
			this.c_startUpDelayUnitsLabel.AutoSize = true;
			this.c_startUpDelayUnitsLabel.Location = new System.Drawing.Point(261, 95);
			this.c_startUpDelayUnitsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.c_startUpDelayUnitsLabel.Name = "c_startUpDelayUnitsLabel";
			this.c_startUpDelayUnitsLabel.Size = new System.Drawing.Size(71, 17);
			this.c_startUpDelayUnitsLabel.TabIndex = 8;
			this.c_startUpDelayUnitsLabel.Text = "(seconds)";
			// 
			// c_okButton
			// 
			this.c_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.c_okButton.Location = new System.Drawing.Point(168, 5);
			this.c_okButton.Margin = new System.Windows.Forms.Padding(4);
			this.c_okButton.Name = "c_okButton";
			this.c_okButton.Size = new System.Drawing.Size(100, 28);
			this.c_okButton.TabIndex = 0;
			this.c_okButton.Text = "&OK";
			this.c_okButton.UseVisualStyleBackColor = true;
			this.c_okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// c_cancelButton
			// 
			this.c_cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.c_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.c_cancelButton.Location = new System.Drawing.Point(272, 5);
			this.c_cancelButton.Margin = new System.Windows.Forms.Padding(4);
			this.c_cancelButton.Name = "c_cancelButton";
			this.c_cancelButton.Size = new System.Drawing.Size(100, 28);
			this.c_cancelButton.TabIndex = 1;
			this.c_cancelButton.Text = "&Cancel";
			this.c_cancelButton.UseVisualStyleBackColor = true;
			this.c_cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// c_startWithWindowsCheckBox
			// 
			this.c_startWithWindowsCheckBox.AutoSize = true;
			this.c_startWithWindowsCheckBox.Location = new System.Drawing.Point(7, 7);
			this.c_startWithWindowsCheckBox.Margin = new System.Windows.Forms.Padding(4);
			this.c_startWithWindowsCheckBox.Name = "c_startWithWindowsCheckBox";
			this.c_startWithWindowsCheckBox.Size = new System.Drawing.Size(152, 21);
			this.c_startWithWindowsCheckBox.TabIndex = 1;
			this.c_startWithWindowsCheckBox.Text = "Start With Windows";
			this.c_startWithWindowsCheckBox.UseVisualStyleBackColor = true;
			// 
			// c_checkForUpdatesCheckBox
			// 
			this.c_checkForUpdatesCheckBox.AutoSize = true;
			this.c_checkForUpdatesCheckBox.Location = new System.Drawing.Point(7, 36);
			this.c_checkForUpdatesCheckBox.Margin = new System.Windows.Forms.Padding(4);
			this.c_checkForUpdatesCheckBox.Name = "c_checkForUpdatesCheckBox";
			this.c_checkForUpdatesCheckBox.Size = new System.Drawing.Size(223, 21);
			this.c_checkForUpdatesCheckBox.TabIndex = 2;
			this.c_checkForUpdatesCheckBox.Text = "Check for Updates on Start Up";
			this.c_checkForUpdatesCheckBox.UseVisualStyleBackColor = true;
			// 
			// c_checkForUpdatesButton
			// 
			this.c_checkForUpdatesButton.Location = new System.Drawing.Point(238, 31);
			this.c_checkForUpdatesButton.Margin = new System.Windows.Forms.Padding(4);
			this.c_checkForUpdatesButton.Name = "c_checkForUpdatesButton";
			this.c_checkForUpdatesButton.Size = new System.Drawing.Size(100, 28);
			this.c_checkForUpdatesButton.TabIndex = 3;
			this.c_checkForUpdatesButton.Text = "Check Now";
			this.c_checkForUpdatesButton.UseVisualStyleBackColor = true;
			this.c_checkForUpdatesButton.Click += new System.EventHandler(this.CheckForUpdatesButton_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.c_okButton);
			this.panel1.Controls.Add(this.c_cancelButton);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 207);
			this.panel1.Margin = new System.Windows.Forms.Padding(4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(376, 37);
			this.panel1.TabIndex = 0;
			// 
			// c_ignoreHiddenFilesCheckbox
			// 
			this.c_ignoreHiddenFilesCheckbox.AutoSize = true;
			this.c_ignoreHiddenFilesCheckbox.Location = new System.Drawing.Point(7, 7);
			this.c_ignoreHiddenFilesCheckbox.Margin = new System.Windows.Forms.Padding(4);
			this.c_ignoreHiddenFilesCheckbox.Name = "c_ignoreHiddenFilesCheckbox";
			this.c_ignoreHiddenFilesCheckbox.Size = new System.Drawing.Size(231, 21);
			this.c_ignoreHiddenFilesCheckbox.TabIndex = 4;
			this.c_ignoreHiddenFilesCheckbox.Text = "Ignore Hidden Files and Folders";
			this.c_ignoreHiddenFilesCheckbox.UseVisualStyleBackColor = true;
			// 
			// c_logLevelLabel
			// 
			this.c_logLevelLabel.AutoSize = true;
			this.c_logLevelLabel.Location = new System.Drawing.Point(7, 10);
			this.c_logLevelLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.c_logLevelLabel.Name = "c_logLevelLabel";
			this.c_logLevelLabel.Size = new System.Drawing.Size(93, 17);
			this.c_logLevelLabel.TabIndex = 9;
			this.c_logLevelLabel.Text = "Loging Level:";
			// 
			// c_logLevelCombo
			// 
			this.c_logLevelCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.c_logLevelCombo.FormattingEnabled = true;
			this.c_logLevelCombo.Location = new System.Drawing.Point(108, 7);
			this.c_logLevelCombo.Margin = new System.Windows.Forms.Padding(4);
			this.c_logLevelCombo.Name = "c_logLevelCombo";
			this.c_logLevelCombo.Size = new System.Drawing.Size(143, 24);
			this.c_logLevelCombo.TabIndex = 10;
			// 
			// c_loadHistoryImages
			// 
			this.c_loadHistoryImages.AutoSize = true;
			this.c_loadHistoryImages.Location = new System.Drawing.Point(7, 64);
			this.c_loadHistoryImages.Name = "c_loadHistoryImages";
			this.c_loadHistoryImages.Size = new System.Drawing.Size(213, 21);
			this.c_loadHistoryImages.TabIndex = 5;
			this.c_loadHistoryImages.Text = "Load History Images on Start";
			this.c_loadHistoryImages.UseVisualStyleBackColor = true;
			// 
			// c_tabControl
			// 
			this.c_tabControl.Controls.Add(this.c_startupTab);
			this.c_tabControl.Controls.Add(this.c_imagesTab);
			this.c_tabControl.Controls.Add(this.tabPage1);
			this.c_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_tabControl.Location = new System.Drawing.Point(0, 0);
			this.c_tabControl.MaximumSize = new System.Drawing.Size(376, 207);
			this.c_tabControl.MinimumSize = new System.Drawing.Size(376, 207);
			this.c_tabControl.Name = "c_tabControl";
			this.c_tabControl.SelectedIndex = 0;
			this.c_tabControl.Size = new System.Drawing.Size(376, 207);
			this.c_tabControl.TabIndex = 11;
			// 
			// c_startupTab
			// 
			this.c_startupTab.Controls.Add(this.c_startWithWindowsCheckBox);
			this.c_startupTab.Controls.Add(this.c_loadHistoryImages);
			this.c_startupTab.Controls.Add(this.c_checkForUpdatesCheckBox);
			this.c_startupTab.Controls.Add(this.c_checkForUpdatesButton);
			this.c_startupTab.Controls.Add(this.c_startUpDelayLabel);
			this.c_startupTab.Controls.Add(this.c_startUpDelayUnitsLabel);
			this.c_startupTab.Controls.Add(this.c_startUpDelayTextBox);
			this.c_startupTab.Location = new System.Drawing.Point(4, 25);
			this.c_startupTab.Name = "c_startupTab";
			this.c_startupTab.Padding = new System.Windows.Forms.Padding(3);
			this.c_startupTab.Size = new System.Drawing.Size(368, 178);
			this.c_startupTab.TabIndex = 0;
			this.c_startupTab.Text = "Start-up";
			this.c_startupTab.UseVisualStyleBackColor = true;
			// 
			// c_imagesTab
			// 
			this.c_imagesTab.Controls.Add(this.c_exportRatingsButton);
			this.c_imagesTab.Controls.Add(this.c_importRatingsButton);
			this.c_imagesTab.Controls.Add(this.c_ignoreHiddenFilesCheckbox);
			this.c_imagesTab.Location = new System.Drawing.Point(4, 25);
			this.c_imagesTab.Name = "c_imagesTab";
			this.c_imagesTab.Padding = new System.Windows.Forms.Padding(3);
			this.c_imagesTab.Size = new System.Drawing.Size(368, 178);
			this.c_imagesTab.TabIndex = 1;
			this.c_imagesTab.Text = "Images";
			this.c_imagesTab.UseVisualStyleBackColor = true;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.c_logLevelCombo);
			this.tabPage1.Controls.Add(this.c_logLevelLabel);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(368, 178);
			this.tabPage1.TabIndex = 2;
			this.tabPage1.Text = "Logging";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// c_importRatingsButton
			// 
			this.c_importRatingsButton.Location = new System.Drawing.Point(7, 50);
			this.c_importRatingsButton.Name = "c_importRatingsButton";
			this.c_importRatingsButton.Size = new System.Drawing.Size(140, 23);
			this.c_importRatingsButton.TabIndex = 5;
			this.c_importRatingsButton.Text = "Import Ratings";
			this.c_importRatingsButton.UseVisualStyleBackColor = true;
			this.c_importRatingsButton.Click += new System.EventHandler(this.ImportRatingsButton_Click);
			// 
			// c_exportRatingsButton
			// 
			this.c_exportRatingsButton.Location = new System.Drawing.Point(153, 50);
			this.c_exportRatingsButton.Name = "c_exportRatingsButton";
			this.c_exportRatingsButton.Size = new System.Drawing.Size(140, 23);
			this.c_exportRatingsButton.TabIndex = 6;
			this.c_exportRatingsButton.Text = "Export Ratings";
			this.c_exportRatingsButton.UseVisualStyleBackColor = true;
			this.c_exportRatingsButton.Click += new System.EventHandler(this.ExportRatingsButton_Click);
			// 
			// SettingsDialog
			// 
			this.AcceptButton = this.c_okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.CancelButton = this.c_cancelButton;
			this.ClientSize = new System.Drawing.Size(376, 244);
			this.Controls.Add(this.c_tabControl);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MinimumSize = new System.Drawing.Size(394, 276);
			this.Name = "SettingsDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.SettingsDialog_Load);
			this.panel1.ResumeLayout(false);
			this.c_tabControl.ResumeLayout(false);
			this.c_startupTab.ResumeLayout(false);
			this.c_startupTab.PerformLayout();
			this.c_imagesTab.ResumeLayout(false);
			this.c_imagesTab.PerformLayout();
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label c_startUpDelayLabel;
		private System.Windows.Forms.TextBox c_startUpDelayTextBox;
		private System.Windows.Forms.Label c_startUpDelayUnitsLabel;
		private System.Windows.Forms.Button c_okButton;
		private System.Windows.Forms.Button c_cancelButton;
		private System.Windows.Forms.CheckBox c_startWithWindowsCheckBox;
		private System.Windows.Forms.CheckBox c_checkForUpdatesCheckBox;
		private System.Windows.Forms.Button c_checkForUpdatesButton;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox c_ignoreHiddenFilesCheckbox;
		private System.Windows.Forms.Label c_logLevelLabel;
		private System.Windows.Forms.ComboBox c_logLevelCombo;
		private System.Windows.Forms.CheckBox c_loadHistoryImages;
		private System.Windows.Forms.TabControl c_tabControl;
		private System.Windows.Forms.TabPage c_startupTab;
		private System.Windows.Forms.TabPage c_imagesTab;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Button c_exportRatingsButton;
		private System.Windows.Forms.Button c_importRatingsButton;
	}
}