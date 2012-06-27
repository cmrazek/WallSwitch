namespace WallSwitch
{
	partial class AboutDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
			this.pnlIcon = new System.Windows.Forms.Panel();
			this.lblAppVersion = new System.Windows.Forms.Label();
			this.lblCopyright = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pnlIcon
			// 
			this.pnlIcon.BackgroundImage = global::WallSwitch.Res.WallSwitch;
			this.pnlIcon.Location = new System.Drawing.Point(12, 12);
			this.pnlIcon.Name = "pnlIcon";
			this.pnlIcon.Size = new System.Drawing.Size(64, 64);
			this.pnlIcon.TabIndex = 0;
			// 
			// lblAppVersion
			// 
			this.lblAppVersion.AutoSize = true;
			this.lblAppVersion.Location = new System.Drawing.Point(82, 12);
			this.lblAppVersion.Name = "lblAppVersion";
			this.lblAppVersion.Size = new System.Drawing.Size(63, 13);
			this.lblAppVersion.TabIndex = 1;
			this.lblAppVersion.Text = "Wall Switch";
			// 
			// lblCopyright
			// 
			this.lblCopyright.AutoSize = true;
			this.lblCopyright.Location = new System.Drawing.Point(82, 61);
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = new System.Drawing.Size(158, 13);
			this.lblCopyright.TabIndex = 2;
			this.lblCopyright.Text = "Copyright (C) 2011 Chris Mrazek";
			// 
			// AboutDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(249, 90);
			this.Controls.Add(this.lblCopyright);
			this.Controls.Add(this.lblAppVersion);
			this.Controls.Add(this.pnlIcon);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(265, 128);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(265, 128);
			this.Name = "AboutDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About Wall Switch";
			this.Load += new System.EventHandler(this.AboutDialog_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel pnlIcon;
		private System.Windows.Forms.Label lblAppVersion;
		private System.Windows.Forms.Label lblCopyright;
	}
}