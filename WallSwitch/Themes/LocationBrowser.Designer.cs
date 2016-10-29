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
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.c_sortPathContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.c_sortRatingContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.c_sortSizeContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.c_statusBar = new System.Windows.Forms.StatusStrip();
			this.c_statusMessage = new System.Windows.Forms.ToolStripStatusLabel();
			this.c_statusCounts = new System.Windows.Forms.ToolStripStatusLabel();
			this.c_vScroll = new System.Windows.Forms.VScrollBar();
			this.c_filterPanel = new System.Windows.Forms.Panel();
			this.c_clearFilterButton = new System.Windows.Forms.Button();
			this.c_filterTextBox = new System.Windows.Forms.TextBox();
			this.c_filterTimer = new System.Windows.Forms.Timer(this.components);
			this.c_contextMenu.SuspendLayout();
			this.c_statusBar.SuspendLayout();
			this.c_filterPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// c_contextMenu
			// 
			this.c_contextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.c_contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_openContextMenuItem,
            this.c_exploreContextMenuItem,
            this.toolStripMenuItem1,
            this.c_deleteContextMenuItem,
            this.toolStripMenuItem2,
            this.sortToolStripMenuItem});
			this.c_contextMenu.Name = "c_listContextMenu";
			this.c_contextMenu.Size = new System.Drawing.Size(135, 120);
			// 
			// c_openContextMenuItem
			// 
			this.c_openContextMenuItem.Name = "c_openContextMenuItem";
			this.c_openContextMenuItem.Size = new System.Drawing.Size(134, 26);
			this.c_openContextMenuItem.Text = "&Open";
			this.c_openContextMenuItem.Click += new System.EventHandler(this.OpenContextMenuItem_Click);
			// 
			// c_exploreContextMenuItem
			// 
			this.c_exploreContextMenuItem.Name = "c_exploreContextMenuItem";
			this.c_exploreContextMenuItem.Size = new System.Drawing.Size(134, 26);
			this.c_exploreContextMenuItem.Text = "&Explore";
			this.c_exploreContextMenuItem.Click += new System.EventHandler(this.ExploreContextMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(131, 6);
			// 
			// c_deleteContextMenuItem
			// 
			this.c_deleteContextMenuItem.Name = "c_deleteContextMenuItem";
			this.c_deleteContextMenuItem.Size = new System.Drawing.Size(134, 26);
			this.c_deleteContextMenuItem.Text = "&Delete";
			this.c_deleteContextMenuItem.Click += new System.EventHandler(this.DeleteContextMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(131, 6);
			// 
			// sortToolStripMenuItem
			// 
			this.sortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_sortPathContextMenuItem,
            this.c_sortRatingContextMenuItem,
            this.c_sortSizeContextMenuItem});
			this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
			this.sortToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
			this.sortToolStripMenuItem.Text = "&Sort";
			// 
			// c_sortPathContextMenuItem
			// 
			this.c_sortPathContextMenuItem.Name = "c_sortPathContextMenuItem";
			this.c_sortPathContextMenuItem.Size = new System.Drawing.Size(127, 26);
			this.c_sortPathContextMenuItem.Text = "&Path";
			this.c_sortPathContextMenuItem.Click += new System.EventHandler(this.SortPathContextMenuItem_Click);
			// 
			// c_sortRatingContextMenuItem
			// 
			this.c_sortRatingContextMenuItem.Name = "c_sortRatingContextMenuItem";
			this.c_sortRatingContextMenuItem.Size = new System.Drawing.Size(127, 26);
			this.c_sortRatingContextMenuItem.Text = "&Rating";
			this.c_sortRatingContextMenuItem.Click += new System.EventHandler(this.SortRatingContextMenuItem_Click);
			// 
			// c_sortSizeContextMenuItem
			// 
			this.c_sortSizeContextMenuItem.Name = "c_sortSizeContextMenuItem";
			this.c_sortSizeContextMenuItem.Size = new System.Drawing.Size(127, 26);
			this.c_sortSizeContextMenuItem.Text = "&Size";
			this.c_sortSizeContextMenuItem.Click += new System.EventHandler(this.SortSizeContextMenuItem_Click);
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
			// c_filterPanel
			// 
			this.c_filterPanel.Controls.Add(this.c_clearFilterButton);
			this.c_filterPanel.Controls.Add(this.c_filterTextBox);
			this.c_filterPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.c_filterPanel.Location = new System.Drawing.Point(0, 0);
			this.c_filterPanel.Name = "c_filterPanel";
			this.c_filterPanel.Size = new System.Drawing.Size(720, 29);
			this.c_filterPanel.TabIndex = 3;
			// 
			// c_clearFilterButton
			// 
			this.c_clearFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.c_clearFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("c_clearFilterButton.Image")));
			this.c_clearFilterButton.Location = new System.Drawing.Point(694, 3);
			this.c_clearFilterButton.Name = "c_clearFilterButton";
			this.c_clearFilterButton.Size = new System.Drawing.Size(23, 24);
			this.c_clearFilterButton.TabIndex = 1;
			this.c_clearFilterButton.UseVisualStyleBackColor = true;
			this.c_clearFilterButton.Click += new System.EventHandler(this.ClearFilterButton_Click);
			// 
			// c_filterTextBox
			// 
			this.c_filterTextBox.AcceptsReturn = true;
			this.c_filterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.c_filterTextBox.Location = new System.Drawing.Point(3, 4);
			this.c_filterTextBox.Name = "c_filterTextBox";
			this.c_filterTextBox.Size = new System.Drawing.Size(689, 22);
			this.c_filterTextBox.TabIndex = 0;
			this.c_filterTextBox.TextChanged += new System.EventHandler(this.FilterTextBox_TextChanged);
			this.c_filterTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c_filterTextBox_KeyDown);
			this.c_filterTextBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.c_filterTextBox_PreviewKeyDown);
			// 
			// c_filterTimer
			// 
			this.c_filterTimer.Interval = 1000;
			this.c_filterTimer.Tick += new System.EventHandler(this.FilterTimer_Tick);
			// 
			// LocationBrowser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(741, 508);
			this.ContextMenuStrip = this.c_contextMenu;
			this.Controls.Add(this.c_filterPanel);
			this.Controls.Add(this.c_vScroll);
			this.Controls.Add(this.c_statusBar);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LocationBrowser";
			this.Text = "LocationBrowser";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LocationBrowser_FormClosed);
			this.Load += new System.EventHandler(this.LocationBrowser_Load);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LocationBrowser_MouseClick);
			this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LocationBrowser_MouseDoubleClick);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LocationBrowser_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LocationBrowser_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LocationBrowser_MouseUp);
			this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.LocationBrowser_PreviewKeyDown);
			this.Resize += new System.EventHandler(this.LocationBrowser_Resize);
			this.c_contextMenu.ResumeLayout(false);
			this.c_statusBar.ResumeLayout(false);
			this.c_statusBar.PerformLayout();
			this.c_filterPanel.ResumeLayout(false);
			this.c_filterPanel.PerformLayout();
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
		private System.Windows.Forms.Panel c_filterPanel;
		private System.Windows.Forms.Button c_clearFilterButton;
		private System.Windows.Forms.TextBox c_filterTextBox;
		private System.Windows.Forms.Timer c_filterTimer;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem sortToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem c_sortPathContextMenuItem;
		private System.Windows.Forms.ToolStripMenuItem c_sortRatingContextMenuItem;
		private System.Windows.Forms.ToolStripMenuItem c_sortSizeContextMenuItem;
	}
}