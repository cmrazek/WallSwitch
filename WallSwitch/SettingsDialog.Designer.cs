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
			this.lblStartUpDelay = new System.Windows.Forms.Label();
			this.txtStartUpDelay = new System.Windows.Forms.TextBox();
			this.lblStartUpDelayUnit = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.chkStartWithWindows = new System.Windows.Forms.CheckBox();
			this.chkCheckForUpdates = new System.Windows.Forms.CheckBox();
			this.btnCheckForUpdates = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblStartUpDelay
			// 
			this.lblStartUpDelay.AutoSize = true;
			this.lblStartUpDelay.Location = new System.Drawing.Point(10, 71);
			this.lblStartUpDelay.Name = "lblStartUpDelay";
			this.lblStartUpDelay.Size = new System.Drawing.Size(121, 13);
			this.lblStartUpDelay.TabIndex = 0;
			this.lblStartUpDelay.Text = "Start-up/Resume Delay:";
			// 
			// txtStartUpDelay
			// 
			this.txtStartUpDelay.Location = new System.Drawing.Point(137, 68);
			this.txtStartUpDelay.Name = "txtStartUpDelay";
			this.txtStartUpDelay.Size = new System.Drawing.Size(60, 20);
			this.txtStartUpDelay.TabIndex = 1;
			// 
			// lblStartUpDelayUnit
			// 
			this.lblStartUpDelayUnit.AutoSize = true;
			this.lblStartUpDelayUnit.Location = new System.Drawing.Point(203, 71);
			this.lblStartUpDelayUnit.Name = "lblStartUpDelayUnit";
			this.lblStartUpDelayUnit.Size = new System.Drawing.Size(53, 13);
			this.lblStartUpDelayUnit.TabIndex = 2;
			this.lblStartUpDelayUnit.Text = "(seconds)";
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(128, 4);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "&OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(206, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// chkStartWithWindows
			// 
			this.chkStartWithWindows.AutoSize = true;
			this.chkStartWithWindows.Location = new System.Drawing.Point(13, 12);
			this.chkStartWithWindows.Name = "chkStartWithWindows";
			this.chkStartWithWindows.Size = new System.Drawing.Size(120, 17);
			this.chkStartWithWindows.TabIndex = 5;
			this.chkStartWithWindows.Text = "Start With Windows";
			this.chkStartWithWindows.UseVisualStyleBackColor = true;
			// 
			// chkCheckForUpdates
			// 
			this.chkCheckForUpdates.AutoSize = true;
			this.chkCheckForUpdates.Location = new System.Drawing.Point(13, 35);
			this.chkCheckForUpdates.Name = "chkCheckForUpdates";
			this.chkCheckForUpdates.Size = new System.Drawing.Size(172, 17);
			this.chkCheckForUpdates.TabIndex = 6;
			this.chkCheckForUpdates.Text = "Check for Updates on Start Up";
			this.chkCheckForUpdates.UseVisualStyleBackColor = true;
			// 
			// btnCheckForUpdates
			// 
			this.btnCheckForUpdates.Location = new System.Drawing.Point(206, 31);
			this.btnCheckForUpdates.Name = "btnCheckForUpdates";
			this.btnCheckForUpdates.Size = new System.Drawing.Size(75, 23);
			this.btnCheckForUpdates.TabIndex = 7;
			this.btnCheckForUpdates.Text = "Check Now";
			this.btnCheckForUpdates.UseVisualStyleBackColor = true;
			this.btnCheckForUpdates.Click += new System.EventHandler(this.btnCheckForUpdates_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 102);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(284, 30);
			this.panel1.TabIndex = 8;
			// 
			// SettingsDialog
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(284, 132);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnCheckForUpdates);
			this.Controls.Add(this.chkCheckForUpdates);
			this.Controls.Add(this.chkStartWithWindows);
			this.Controls.Add(this.lblStartUpDelayUnit);
			this.Controls.Add(this.txtStartUpDelay);
			this.Controls.Add(this.lblStartUpDelay);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximumSize = new System.Drawing.Size(300, 170);
			this.MinimumSize = new System.Drawing.Size(300, 170);
			this.Name = "SettingsDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.SettingsDialog_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblStartUpDelay;
		private System.Windows.Forms.TextBox txtStartUpDelay;
		private System.Windows.Forms.Label lblStartUpDelayUnit;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox chkStartWithWindows;
		private System.Windows.Forms.CheckBox chkCheckForUpdates;
		private System.Windows.Forms.Button btnCheckForUpdates;
		private System.Windows.Forms.Panel panel1;
	}
}