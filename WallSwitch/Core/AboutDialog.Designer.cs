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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlIcon
			// 
			this.pnlIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlIcon.BackgroundImage")));
			this.pnlIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.pnlIcon.Location = new System.Drawing.Point(4, 4);
			this.pnlIcon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pnlIcon.Name = "pnlIcon";
			this.tableLayoutPanel1.SetRowSpan(this.pnlIcon, 5);
			this.pnlIcon.Size = new System.Drawing.Size(85, 79);
			this.pnlIcon.TabIndex = 0;
			// 
			// lblAppName
			// 
			this.lblAppName.AutoSize = true;
			this.lblAppName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAppName.Location = new System.Drawing.Point(97, 0);
			this.lblAppName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblAppName.Name = "lblAppName";
			this.lblAppName.Size = new System.Drawing.Size(125, 25);
			this.lblAppName.TabIndex = 1;
			this.lblAppName.Text = "Wall Switch";
			// 
			// lblCopyright
			// 
			this.lblCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCopyright.AutoSize = true;
			this.lblCopyright.Location = new System.Drawing.Point(198, 6);
			this.lblCopyright.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = new System.Drawing.Size(204, 17);
			this.lblCopyright.TabIndex = 2;
			this.lblCopyright.Text = "Copyright © 2012 Chris Mrazek";
			// 
			// lblAppVersion
			// 
			this.lblAppVersion.AutoSize = true;
			this.lblAppVersion.Location = new System.Drawing.Point(97, 25);
			this.lblAppVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblAppVersion.Name = "lblAppVersion";
			this.lblAppVersion.Size = new System.Drawing.Size(96, 17);
			this.lblAppVersion.TabIndex = 3;
			this.lblAppVersion.Text = "<exe version>";
			// 
			// lblUpdateInfo
			// 
			this.lblUpdateInfo.AutoSize = true;
			this.lblUpdateInfo.Location = new System.Drawing.Point(97, 62);
			this.lblUpdateInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblUpdateInfo.Name = "lblUpdateInfo";
			this.lblUpdateInfo.Size = new System.Drawing.Size(155, 17);
			this.lblUpdateInfo.TabIndex = 4;
			this.lblUpdateInfo.Text = "(checking for update...)";
			// 
			// lnkDownloadUpdate
			// 
			this.lnkDownloadUpdate.AutoSize = true;
			this.lnkDownloadUpdate.Location = new System.Drawing.Point(97, 79);
			this.lnkDownloadUpdate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lnkDownloadUpdate.Name = "lnkDownloadUpdate";
			this.lnkDownloadUpdate.Size = new System.Drawing.Size(68, 17);
			this.lnkDownloadUpdate.TabIndex = 5;
			this.lnkDownloadUpdate.TabStop = true;
			this.lnkDownloadUpdate.Text = "download";
			this.lnkDownloadUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDownloadUpdate_LinkClicked);
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.tableLayoutPanel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 201);
			this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(412, 29);
			this.panel1.TabIndex = 6;
			// 
			// lnkWebsite
			// 
			this.lnkWebsite.AutoSize = true;
			this.lnkWebsite.Location = new System.Drawing.Point(10, 6);
			this.lnkWebsite.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lnkWebsite.Name = "lnkWebsite";
			this.lnkWebsite.Size = new System.Drawing.Size(55, 17);
			this.lnkWebsite.TabIndex = 3;
			this.lnkWebsite.TabStop = true;
			this.lnkWebsite.Text = "website";
			this.lnkWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWebsite_LinkClicked);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.pnlIcon, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblAppName, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.lnkDownloadUpdate, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.lblAppVersion, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblUpdateInfo, 1, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(412, 201);
			this.tableLayoutPanel1.TabIndex = 7;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.lblCopyright, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.lnkWebsite, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(412, 29);
			this.tableLayoutPanel2.TabIndex = 4;
			// 
			// AboutDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(412, 230);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About Wall Switch";
			this.Load += new System.EventHandler(this.AboutDialog_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
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
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
	}
}