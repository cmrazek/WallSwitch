using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WallSwitch.SettingsStore;

namespace WallSwitch
{
	static class Program
	{
		private const string k_guid = "57FF779B-63F3-430A-B420-AD436F2D2AEB";

		private static SwitchThread _switchThread;
		private static MainWindow _mainWindow;
		private static OsVersion? _osVersion;

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
						WidgetManager.SearchForWidgets();

						Application.EnableVisualStyles();
						Application.SetCompatibleTextRenderingDefault(false);

						_mainWindow = new MainWindow(winStart);
						Application.Run(_mainWindow);
					}
					finally
					{
						Log.Debug("Main thread is terminating.");

						// Terminal the switch thread
						try
						{
							Log.Debug("Shutting down switch thread.");
							if (_switchThread != null && _switchThread.IsAlive)
							{
								_switchThread.Kill();
								_switchThread = null;
							}
						}
						catch (Exception ex)
						{
							Log.Write(ex);
						}

						// Close the log file
						try
						{
							Log.Close();
						}
						catch (Exception ex)
						{
							Log.Write(ex);	// Probably futile
						}
					}
				}
				else if (!winStart)
				{
					NativeMethods.PostMessage((IntPtr)NativeMethods.HWND_BROADCAST,
						NativeMethods.WM_SHOWME, IntPtr.Zero, IntPtr.Zero);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Error when starting WallSwitch", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public static SwitchThread SwitchThread
		{
			get { return _switchThread; }
			set { _switchThread = value; }
		}

		public static OsVersion OsVersion
		{
			get
			{
				if (!_osVersion.HasValue)
				{
					var ver = Environment.OSVersion.Version;

					if (ver.Major < 6)
					{
						_osVersion = OsVersion.Legacy;
					}
					else if (ver.Major == 6)
					{
						if (ver.Minor == 0) _osVersion = OsVersion.Vista;
						else if (ver.Minor == 1) _osVersion = OsVersion.Windows7;
						else if (ver.Minor == 2) _osVersion = OsVersion.Windows8;
						else if (ver.Minor == 3) _osVersion = OsVersion.Windows81;
						else _osVersion = OsVersion.Future;
					}
					else
					{
						_osVersion = OsVersion.Future;
					}

					Log.Debug("OS Version: {0} ({1})", ver, _osVersion.Value);
				}

				return _osVersion.Value;
			}
		}
	}

	enum OsVersion
	{
		Legacy,
		Vista,
		Windows7,
		Windows8,
		Windows81,
		Future
	}
}
