using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace WallSwitch
{
	static class Program
	{
		private const string k_guid = "57FF779B-63F3-430A-B420-AD436F2D2AEB";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				var winStart = false;
				foreach (var arg in args)
				{
					if (arg.Equals("-winstart", StringComparison.InvariantCultureIgnoreCase))
					{
						winStart = true;
					}
				}

				bool createdNew;
				var mutex = new Mutex(true, k_guid, out createdNew);
				if (createdNew)
				{
					Log.Initialize();
					try
					{
#if DEBUG
						Log.Level = LogLevel.Debug;
#else
						Log.Level = LogLevel.Info;
#endif

						Settings.Initialize();

						Application.EnableVisualStyles();
						Application.SetCompatibleTextRenderingDefault(false);
						Application.Run(new MainWindow(winStart));
					}
					finally
					{
						Log.Close();
					}
				}
				else if (!winStart)
				{
					MainWindow.AppActivateExternal();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Error when starting WallSwitch", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
