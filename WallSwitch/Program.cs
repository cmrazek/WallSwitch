using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace WallSwitch
{
	static class Program
	{
		private const string k_guid = "57FF779B-63F3-430A-B420-AD436F2D2AEB";

		private static WallSwitchServiceManager _serviceMgr;
		private static SwitchThread _switchThread;
		private static MainWindow _mainWindow;

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
						Log.WriteDebug("Main thread is terminating.");

						// Shutdown the WCF service
						try
						{
							Log.WriteDebug("Shutting down WCF service.");
							if (_serviceMgr != null)
							{
								_serviceMgr.Dispose();
								_serviceMgr = null;
							}
						}
						catch (Exception ex)
						{
							Log.Write(ex);
						}

						// Terminal the switch thread
						try
						{
							Log.WriteDebug("Shutting down switch thread.");
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
					MainWindow.AppActivateExternal();
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

		public static WallSwitchServiceManager ServiceManager
		{
			get { return _serviceMgr; }
			set { _serviceMgr = value; }
		}
	}
}
