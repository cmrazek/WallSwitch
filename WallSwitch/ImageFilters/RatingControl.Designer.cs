namespace WallSwitch.ImageFilters
{
	partial class RatingControl
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
			// RatingControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "RatingControl";
			this.Size = new System.Drawing.Size(100, 20);
			this.Load += new System.EventHandler(this.RatingControl_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.RatingControl_Paint);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RatingControl_MouseClick);
			this.MouseLeave += new System.EventHandler(this.RatingControl_MouseLeave);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RatingControl_MouseMove);
			this.Resize += new System.EventHandler(this.RatingControl_Resize);
			this.ResumeLayout(false);

		}

		#endregion
	}
}
