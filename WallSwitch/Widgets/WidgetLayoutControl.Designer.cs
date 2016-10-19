namespace WallSwitch
{
	partial class WidgetLayoutControl
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
			this.SuspendLayout();
			// 
			// WidgetLayoutControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.DoubleBuffered = true;
			this.Name = "WidgetLayoutControl";
			this.Load += new System.EventHandler(this.WidgetLayoutControl_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.WidgetLayoutControl_Paint);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WidgetLayoutControl_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WidgetLayoutControl_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WidgetLayoutControl_MouseUp);
			this.Resize += new System.EventHandler(this.WidgetLayoutControl_Resize);
			this.ResumeLayout(false);

		}

		#endregion
	}
}
