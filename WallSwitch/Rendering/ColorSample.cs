using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WallSwitch
{
	public partial class ColorSample : UserControl
	{
		public delegate void ColorChangedEventHandler(object sender, ColorChangedEventArgs e);
		public event ColorChangedEventHandler ColorChanged;
		public class ColorChangedEventArgs : EventArgs
		{
			public Color OldColor;
			public Color NewColor;
		}

		public ColorSample()
		{
			InitializeComponent();
		}

		public Color Color
		{
			get { return BackColor; }
			set { BackColor = value; }
		}

		private void ColorSample_Click(object sender, EventArgs e)
		{
			ColorDialog dlg = new ColorDialog();
			dlg.Color = BackColor;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if (BackColor.R != dlg.Color.R ||
					BackColor.G != dlg.Color.G ||
					BackColor.B != dlg.Color.B)
				{
					BackColor = Color.FromArgb(255, dlg.Color.R, dlg.Color.G, dlg.Color.B);

					ColorChangedEventArgs args = new ColorChangedEventArgs();
					args.OldColor = BackColor;
					args.NewColor = dlg.Color;

					ColorChangedEventHandler ev = ColorChanged;
					if (ev != null) ev(this, args);
				}
			}
		}
	}
}
