using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WallSwitch
{
	public class VStackPanel : Panel
	{
		private bool _grow = true;

		public VStackPanel()
		{
			this.Resize += VStackPanel_Resize;
			this.ControlAdded += VStackPanel_ControlAdded;
			this.ControlRemoved += VStackPanel_ControlRemoved;
		}

		public bool Grow
		{
			get { return _grow; }
			set { _grow = value; }
		}

		private void VStackPanel_Resize(object sender, EventArgs e)
		{
			//UpdateControlLayout(false);
		}

		private void VStackPanel_ControlAdded(object sender, ControlEventArgs e)
		{
			UpdateControlLayout(true);
		}

		private void VStackPanel_ControlRemoved(object sender, ControlEventArgs e)
		{
			UpdateControlLayout(true);
		}

		private void UpdateControlLayout(bool resizeIfChanged)
		{
			int y = 0;
			int width = ClientSize.Width;

			foreach (Control ctrl in Controls)
			{
				ctrl.Location = new Point(0, y);
				ctrl.Width = width;
				y += ctrl.Height;
			}

			if (resizeIfChanged)
			{
				if (_grow && Height != y)
				{
					Height = y;
				}
			}
		}
	}
}
