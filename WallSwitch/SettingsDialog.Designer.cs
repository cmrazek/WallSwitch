namespace WallSwitch
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
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// c_startUpDelayLabel
			// 
			this.c_startUpDelayLabel.AutoSize = true;
			this.c_startUpDelayLabel.Location = new System.Drawing.Point(10, 84);
			this.c_startUpDelayLabel.Name = "c_startUpDelayLabel";
			this.c_startUpDelayLabel.Size = new System.Drawing.Size(121, 13);
			this.c_startUpDelayLabel.TabIndex = 5;
			this.c_startUpDelayLabel.Text = "Start-up/Resume Delay:";
			// 
			// c_startUpDelayTextBox
			// 
			this.c_startUpDelayTextBox.Location = new System.Drawing.Point(137, 81);
			this.c_startUpDelayTextBox.Name = "c_startUpDelayTextBox";
			this.c_startUpDelayTextBox.Size = new System.Drawing.Size(60, 20);
			this.c_startUpDelayTextBox.TabIndex = 6;
			// 
			// c_startUpDelayUnitsLabel
			// 
			this.c_startUpDelayUnitsLabel.AutoSize = true;
			this.c_startUpDelayUnitsLabel.Location = new System.Drawing.Point(203, 84);
			this.c_startUpDelayUnitsLabel.Name = "c_startUpDelayUnitsLabel";
			this.c_startUpDelayUnitsLabel.Size = new System.Drawing.Size(53, 13);
			this.c_startUpDelayUnitsLabel.TabIndex = 7;
			this.c_startUpDelayUnitsLabel.Text = "(seconds)";
			// 
			// c_okButton
			// 
			this.c_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.c_okButton.Location = new System.Drawing.Point(128, 4);
			this.c_okButton.Name = "c_okButton";
			this.c_okButton.Size = new System.Drawing.Size(75, 23);
			this.c_okButton.TabIndex = 0;
			this.c_okButton.Text = "&OK";
			this.c_okButton.UseVisualStyleBackColor = true;
			this.c_okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// c_cancelButton
			// 
			this.c_cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.c_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.c_cancelButton.Location = new System.Drawing.Point(206, 4);
			this.c_cancelButton.Name = "c_cancelButton";
			this.c_cancelButton.Size = new System.Drawing.Size(75, 23);
			this.c_cancelButton.TabIndex = 1;
			this.c_cancelButton.Text = "&Cancel";
			this.c_cancelButton.UseVisualStyleBackColor = true;
			this.c_cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// c_startWithWindowsCheckBox
			// 
			this.c_startWithWindowsCheckBox.AutoSize = true;
			this.c_startWithWindowsCheckBox.Location = new System.Drawing.Point(13, 12);
			this.c_startWithWindowsCheckBox.Name = "c_startWithWindowsCheckBox";
			this.c_startWithWindowsCheckBox.Size = new System.Drawing.Size(120, 17);
			this.c_startWithWindowsCheckBox.TabIndex = 1;
			this.c_startWithWindowsCheckBox.Text = "Start With Windows";
			this.c_startWithWindowsCheckBox.UseVisualStyleBackColor = true;
			// 
			// c_checkForUpdatesCheckBox
			// 
			this.c_checkForUpdatesCheckBox.AutoSize = true;
			this.c_checkForUpdatesCheckBox.Location = new System.Drawing.Point(13, 35);
			this.c_checkForUpdatesCheckBox.Name = "c_checkForUpdatesCheckBox";
			this.c_checkForUpdatesCheckBox.Size = new System.Drawing.Size(172, 17);
			this.c_checkForUpdatesCheckBox.TabIndex = 2;
			this.c_checkForUpdatesCheckBox.Text = "Check for Updates on Start Up";
			this.c_checkForUpdatesCheckBox.UseVisualStyleBackColor = true;
			// 
			// c_checkForUpdatesButton
			// 
			this.c_checkForUpdatesButton.Location = new System.Drawing.Point(206, 31);
			this.c_checkForUpdatesButton.Name = "c_checkForUpdatesButton";
			this.c_checkForUpdatesButton.Size = new System.Drawing.Size(75, 23);
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
			this.panel1.Location = new System.Drawing.Point(0, 140);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(284, 30);
			this.panel1.TabIndex = 0;
			// 
			// c_ignoreHiddenFilesCheckbox
			// 
			this.c_ignoreHiddenFilesCheckbox.AutoSize = true;
			this.c_ignoreHiddenFilesCheckbox.Location = new System.Drawing.Point(13, 58);
			this.c_ignoreHiddenFilesCheckbox.Name = "c_ignoreHiddenFilesCheckbox";
			this.c_ignoreHiddenFilesCheckbox.Size = new System.Drawing.Size(175, 17);
			this.c_ignoreHiddenFilesCheckbox.TabIndex = 4;
			this.c_ignoreHiddenFilesCheckbox.Text = "Ignore Hidden Files and Folders";
			this.c_ignoreHiddenFilesCheckbox.UseVisualStyleBackColor = true;
			// 
			// c_logLevelLabel
			// 
			this.c_logLevelLabel.AutoSize = true;
			this.c_logLevelLabel.Location = new System.Drawing.Point(12, 110);
			this.c_logLevelLabel.Name = "c_logLevelLabel";
			this.c_logLevelLabel.Size = new System.Drawing.Size(71, 13);
			this.c_logLevelLabel.TabIndex = 8;
			this.c_logLevelLabel.Text = "Loging Level:";
			// 
			// c_logLevelCombo
			// 
			this.c_logLevelCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.c_logLevelCombo.FormattingEnabled = true;
			this.c_logLevelCombo.Location = new System.Drawing.Point(89, 107);
			this.c_logLevelCombo.Name = "c_logLevelCombo";
			this.c_logLevelCombo.Size = new System.Drawing.Size(108, 21);
			this.c_logLevelCombo.TabIndex = 9;
			// 
			// SettingsDialog
			// 
			this.AcceptButton = this.c_okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.CancelButton = this.c_cancelButton;
			this.ClientSize = new System.Drawing.Size(284, 170);
			this.Controls.Add(this.c_logLevelCombo);
			this.Controls.Add(this.c_logLevelLabel);
			this.Controls.Add(this.c_ignoreHiddenFilesCheckbox);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.c_checkForUpdatesButton);
			this.Controls.Add(this.c_checkForUpdatesCheckBox);
			this.Controls.Add(this.c_startWithWindowsCheckBox);
			this.Controls.Add(this.c_startUpDelayUnitsLabel);
			this.Controls.Add(this.c_startUpDelayTextBox);
			this.Controls.Add(this.c_startUpDelayLabel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximumSize = new System.Drawing.Size(300, 208);
			this.MinimumSize = new System.Drawing.Size(300, 208);
			this.Name = "SettingsDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.SettingsDialog_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

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
	}
}