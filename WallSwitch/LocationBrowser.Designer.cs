namespace WallSwitch
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
			this.c_list = new System.Windows.Forms.ListView();
			this.col_thumb = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.col_path = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.col_rating = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.col_size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.c_listContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.c_openContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.c_exploreContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.c_deleteContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.c_listContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// c_list
			// 
			this.c_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_thumb,
            this.col_path,
            this.col_rating,
            this.col_size});
			this.c_list.ContextMenuStrip = this.c_listContextMenu;
			this.c_list.Dock = System.Windows.Forms.DockStyle.Fill;
			this.c_list.FullRowSelect = true;
			this.c_list.Location = new System.Drawing.Point(0, 0);
			this.c_list.MultiSelect = false;
			this.c_list.Name = "c_list";
			this.c_list.OwnerDraw = true;
			this.c_list.Size = new System.Drawing.Size(741, 508);
			this.c_list.TabIndex = 0;
			this.c_list.UseCompatibleStateImageBehavior = false;
			this.c_list.View = System.Windows.Forms.View.Details;
			this.c_list.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.List_DrawColumnHeader);
			this.c_list.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.List_DrawItem);
			this.c_list.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.List_DrawSubItem);
			this.c_list.ItemActivate += new System.EventHandler(this.List_ItemActivate);
			this.c_list.SelectedIndexChanged += new System.EventHandler(this.c_list_SelectedIndexChanged);
			this.c_list.MouseClick += new System.Windows.Forms.MouseEventHandler(this.c_list_MouseClick);
			this.c_list.MouseLeave += new System.EventHandler(this.List_MouseLeave);
			this.c_list.MouseMove += new System.Windows.Forms.MouseEventHandler(this.List_MouseMove);
			// 
			// col_thumb
			// 
			this.col_thumb.Tag = "thumb";
			this.col_thumb.Text = "Thumbnail";
			this.col_thumb.Width = 124;
			// 
			// col_path
			// 
			this.col_path.Tag = "path";
			this.col_path.Text = "Path";
			this.col_path.Width = 400;
			// 
			// col_rating
			// 
			this.col_rating.Tag = "rating";
			this.col_rating.Text = "Rating";
			this.col_rating.Width = 100;
			// 
			// col_size
			// 
			this.col_size.Tag = "size";
			this.col_size.Text = "Size";
			this.col_size.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.col_size.Width = 80;
			// 
			// c_listContextMenu
			// 
			this.c_listContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.c_listContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_openContextMenuItem,
            this.c_exploreContextMenuItem,
            this.toolStripMenuItem1,
            this.c_deleteContextMenuItem});
			this.c_listContextMenu.Name = "c_listContextMenu";
			this.c_listContextMenu.Size = new System.Drawing.Size(135, 88);
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
			// LocationBrowser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(741, 508);
			this.Controls.Add(this.c_list);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LocationBrowser";
			this.Text = "LocationBrowser";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LocationBrowser_FormClosed);
			this.Load += new System.EventHandler(this.LocationBrowser_Load);
			this.c_listContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView c_list;
		private System.Windows.Forms.ColumnHeader col_thumb;
		private System.Windows.Forms.ColumnHeader col_path;
		private System.Windows.Forms.ColumnHeader col_rating;
		private System.Windows.Forms.ColumnHeader col_size;
		private System.Windows.Forms.ContextMenuStrip c_listContextMenu;
		private System.Windows.Forms.ToolStripMenuItem c_openContextMenuItem;
		private System.Windows.Forms.ToolStripMenuItem c_exploreContextMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem c_deleteContextMenuItem;
	}
}