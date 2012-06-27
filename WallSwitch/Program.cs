using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace WallSwitch
{
	static class Program
	{
		private const string k_guid = "{57FF779B-63F3-430A-B420-AD436F2D2AEB}";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
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

				bool createdNew = false;
				using (Semaphore sem = new Semaphore(0, 1, "Global\\" + k_guid, out createdNew))
				{
					if (createdNew)
					{
						ThreadPool.QueueUserWorkItem((WaitCallback)(state => SemProc(sem)));
						Application.Run(new MainWindow(args));
					}
					sem.Release();
				}
			}
			finally
			{
				Log.Close();
			}
		}

		static void SemProc(Semaphore s)
		{
			try
			{
				if (s != null)
				{
					while (s.WaitOne())
					{
						MainWindow window = MainWindow.Window;
						if (window != null) window.AppActivate();
					}
				}
			}
			catch (Exception)
			{ }
		}
	}
}
