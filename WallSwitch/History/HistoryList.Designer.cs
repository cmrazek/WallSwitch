namespace WallSwitch
{
	partial class HistoryList
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.vScroll = new System.Windows.Forms.VScrollBar();
			this.SuspendLayout();
			// 
			// vScroll
			// 
			this.vScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.vScroll.Location = new System.Drawing.Point(133, 0);
			this.vScroll.Name = "vScroll";
			this.vScroll.Size = new System.Drawing.Size(17, 150);
			this.vScroll.TabIndex = 0;
			this.vScroll.ValueChanged += new System.EventHandler(this.vScroll_ValueChanged);
			// 
			// HistoryList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.vScroll);
			this.DoubleBuffered = true;
			this.Name = "HistoryList";
			this.Load += new System.EventHandler(this.HistoryList_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.HistoryList_Paint);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HistoryList_KeyDown);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.HistoryList_MouseClick);
			this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.HistoryList_MouseDoubleClick);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HistoryList_MouseDown);
			this.MouseHover += new System.EventHandler(this.HistoryList_MouseHover);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HistoryList_MouseMove);
			this.Resize += new System.EventHandler(this.HistoryList_Resize);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.VScrollBar vScroll;
	}
}
