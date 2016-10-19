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
			this.lblAppName = new System.Windows.Forms.Label();
			this.lblCopyright = new System.Windows.Forms.Label();
			this.lblAppVersion = new System.Windows.Forms.Label();
			this.lblUpdateInfo = new System.Windows.Forms.Label();
			this.lnkDownloadUpdate = new System.Windows.Forms.LinkLabel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lnkWebsite = new System.Windows.Forms.LinkLabel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlIcon
			// 
			this.pnlIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlIcon.BackgroundImage")));
			this.pnlIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.pnlIcon.Location = new System.Drawing.Point(12, 12);
			this.pnlIcon.Name = "pnlIcon";
			this.pnlIcon.Size = new System.Drawing.Size(64, 64);
			this.pnlIcon.TabIndex = 0;
			// 
			// lblAppName
			// 
			this.lblAppName.AutoSize = true;
			this.lblAppName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAppName.Location = new System.Drawing.Point(82, 12);
			this.lblAppName.Name = "lblAppName";
			this.lblAppName.Size = new System.Drawing.Size(101, 20);
			this.lblAppName.TabIndex = 1;
			this.lblAppName.Text = "Wall Switch";
			// 
			// lblCopyright
			// 
			this.lblCopyright.AutoSize = true;
			this.lblCopyright.Location = new System.Drawing.Point(114, 10);
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = new System.Drawing.Size(154, 13);
			this.lblCopyright.TabIndex = 2;
			this.lblCopyright.Text = "Copyright © 2012 Chris Mrazek";
			// 
			// lblAppVersion
			// 
			this.lblAppVersion.AutoSize = true;
			this.lblAppVersion.Location = new System.Drawing.Point(83, 32);
			this.lblAppVersion.Name = "lblAppVersion";
			this.lblAppVersion.Size = new System.Drawing.Size(73, 13);
			this.lblAppVersion.TabIndex = 3;
			this.lblAppVersion.Text = "<exe version>";
			// 
			// lblUpdateInfo
			// 
			this.lblUpdateInfo.AutoSize = true;
			this.lblUpdateInfo.Location = new System.Drawing.Point(83, 60);
			this.lblUpdateInfo.Name = "lblUpdateInfo";
			this.lblUpdateInfo.Size = new System.Drawing.Size(117, 13);
			this.lblUpdateInfo.TabIndex = 4;
			this.lblUpdateInfo.Text = "(checking for update...)";
			// 
			// lnkDownloadUpdate
			// 
			this.lnkDownloadUpdate.AutoSize = true;
			this.lnkDownloadUpdate.Location = new System.Drawing.Point(83, 77);
			this.lnkDownloadUpdate.Name = "lnkDownloadUpdate";
			this.lnkDownloadUpdate.Size = new System.Drawing.Size(53, 13);
			this.lnkDownloadUpdate.TabIndex = 5;
			this.lnkDownloadUpdate.TabStop = true;
			this.lnkDownloadUpdate.Text = "download";
			this.lnkDownloadUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDownloadUpdate_LinkClicked);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.lnkWebsite);
			this.panel1.Controls.Add(this.lblCopyright);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 108);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(284, 36);
			this.panel1.TabIndex = 6;
			// 
			// lnkWebsite
			// 
			this.lnkWebsite.AutoSize = true;
			this.lnkWebsite.Location = new System.Drawing.Point(12, 10);
			this.lnkWebsite.Name = "lnkWebsite";
			this.lnkWebsite.Size = new System.Drawing.Size(43, 13);
			this.lnkWebsite.TabIndex = 3;
			this.lnkWebsite.TabStop = true;
			this.lnkWebsite.Text = "website";
			this.lnkWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWebsite_LinkClicked);
			// 
			// AboutDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(284, 144);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.lnkDownloadUpdate);
			this.Controls.Add(this.lblUpdateInfo);
			this.Controls.Add(this.lblAppVersion);
			this.Controls.Add(this.lblAppName);
			this.Controls.Add(this.pnlIcon);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About Wall Switch";
			this.Load += new System.EventHandler(this.AboutDialog_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel pnlIcon;
		private System.Windows.Forms.Label lblAppName;
		private System.Windows.Forms.Label lblCopyright;
		private System.Windows.Forms.Label lblAppVersion;
		private System.Windows.Forms.Label lblUpdateInfo;
		private System.Windows.Forms.LinkLabel lnkDownloadUpdate;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.LinkLabel lnkWebsite;
	}
}