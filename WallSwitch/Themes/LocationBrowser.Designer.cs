namespace WallSwitch.Themes
{
	partial class LocationBrowser
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocationBrowser));
			this.c_contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.c_openContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.c_exploreContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.c_deleteContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.c_statusBar = new System.Windows.Forms.StatusStrip();
			this.c_statusMessage = new System.Windows.Forms.ToolStripStatusLabel();
			this.c_statusCounts = new System.Windows.Forms.ToolStripStatusLabel();
			this.c_vScroll = new System.Windows.Forms.VScrollBar();
			this.c_contextMenu.SuspendLayout();
			this.c_statusBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// c_contextMenu
			// 
			this.c_contextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.c_contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_openContextMenuItem,
            this.c_exploreContextMenuItem,
            this.toolStripMenuItem1,
            this.c_deleteContextMenuItem});
			this.c_contextMenu.Name = "c_listContextMenu";
			this.c_contextMenu.Size = new System.Drawing.Size(182, 116);
			// 
			// c_openContextMenuItem
			// 
			this.c_openContextMenuItem.Name = "c_openContextMenuItem";
			this.c_openContextMenuItem.Size = new System.Drawing.Size(181, 26);
			this.c_openContextMenuItem.Text = "&Open";
			this.c_openContextMenuItem.Click += new System.EventHandler(this.OpenContextMenuItem_Click);
			// 
			// c_exploreContextMenuItem
			// 
			this.c_exploreContextMenuItem.Name = "c_exploreContextMenuItem";
			this.c_exploreContextMenuItem.Size = new System.Drawing.Size(181, 26);
			this.c_exploreContextMenuItem.Text = "&Explore";
			this.c_exploreContextMenuItem.Click += new System.EventHandler(this.ExploreContextMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(178, 6);
			// 
			// c_deleteContextMenuItem
			// 
			this.c_deleteContextMenuItem.Name = "c_deleteContextMenuItem";
			this.c_deleteContextMenuItem.Size = new System.Drawing.Size(181, 26);
			this.c_deleteContextMenuItem.Text = "&Delete";
			this.c_deleteContextMenuItem.Click += new System.EventHandler(this.DeleteContextMenuItem_Click);
			// 
			// c_statusBar
			// 
			this.c_statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.c_statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_statusMessage,
            this.c_statusCounts});
			this.c_statusBar.Location = new System.Drawing.Point(0, 486);
			this.c_statusBar.Name = "c_statusBar";
			this.c_statusBar.Size = new System.Drawing.Size(741, 22);
			this.c_statusBar.TabIndex = 1;
			// 
			// c_statusMessage
			// 
			this.c_statusMessage.Name = "c_statusMessage";
			this.c_statusMessage.Size = new System.Drawing.Size(726, 17);
			this.c_statusMessage.Spring = true;
			this.c_statusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// c_statusCounts
			// 
			this.c_statusCounts.Name = "c_statusCounts";
			this.c_statusCounts.Size = new System.Drawing.Size(0, 17);
			// 
			// c_vScroll
			// 
			this.c_vScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.c_vScroll.Location = new System.Drawing.Point(720, 0);
			this.c_vScroll.Name = "c_vScroll";
			this.c_vScroll.Size = new System.Drawing.Size(21, 486);
			this.c_vScroll.TabIndex = 2;
			this.c_vScroll.ValueChanged += new System.EventHandler(this.VScroll_ValueChanged);
			// 
			// LocationBrowser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(741, 508);
			this.ContextMenuStrip = this.c_contextMenu;
			this.Controls.Add(this.c_vScroll);
			this.Controls.Add(this.c_statusBar);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LocationBrowser";
			this.Text = "LocationBrowser";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LocationBrowser_FormClosed);
			this.Load += new System.EventHandler(this.LocationBrowser_Load);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LocationBrowser_MouseClick);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LocationBrowser_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LocationBrowser_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LocationBrowser_MouseUp);
			this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.LocationBrowser_PreviewKeyDown);
			this.Resize += new System.EventHandler(this.LocationBrowser_Resize);
			this.c_contextMenu.ResumeLayout(false);
			this.c_statusBar.ResumeLayout(false);
			this.c_statusBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ContextMenuStrip c_contextMenu;
		private System.Windows.Forms.ToolStripMenuItem c_openContextMenuItem;
		private System.Windows.Forms.ToolStripMenuItem c_exploreContextMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem c_deleteContextMenuItem;
		private System.Windows.Forms.StatusStrip c_statusBar;
		private System.Windows.Forms.ToolStripStatusLabel c_statusMessage;
		private System.Windows.Forms.ToolStripStatusLabel c_statusCounts;
		private System.Windows.Forms.VScrollBar c_vScroll;
	}
}