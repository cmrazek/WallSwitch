using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WallSwitch
{
	public class WallSwitchService : IWallSwitchService
	{
		public void Activate()
		{
			try
			{
				var wnd = MainWindow.Window;
				if (wnd != null && !wnd.IsDisposed)
				{
					wnd.AppActivate();
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.ToString());
			}
		}
	}
}
