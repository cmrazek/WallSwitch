﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace WallSwitch
{
	class SwitchThread
	{
		#region Variables
		private Thread _thread = null;
		private List<string> _files = new List<string>();
		private Random _rand = new Random();

		//private object _controlLock = new object();		TODO: remove
		private volatile bool _kill = false;
		private volatile SwitchDir _switchNow = SwitchDir.None;
		private volatile bool _locked = false;
		private volatile bool _screensaverRunning = false;

		private object _themeLock = new object();
		private Theme _theme = null;
		private DateTime _lastSwitch = DateTime.MinValue;
		#endregion

		#region Constants
		private const int k_sleepTime = 1000;
		#endregion

		#region PInvoke
		[DllImport("Kernel32.dll", SetLastError = true)]
		public static extern uint GetLastError();

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref bool pvParam, uint fWinIni);

		public const uint SPI_GETSCREENSAVERRUNNING = 0x0072;
		#endregion

		#region Construction
		public void Start(Theme theme)
		{
			if (theme == null) throw new ArgumentNullException("Theme is null.");
			_theme = theme;

			_kill = false;

			_thread = new Thread(new ThreadStart(ThreadProc));
			_thread.Name = "Switch Thread";
			_thread.Start();
		}

		public void Kill()
		{
			if (_thread != null)
			{
				if (_thread.IsAlive)
				{
					_kill = true;
					_thread.Join();
				}

				_thread = null;
			}
		}

		// TODO: remove
		//private bool CheckKill()
		//{
		//    bool kill;
		//    lock (_controlLock)
		//    {
		//        kill = _kill;
		//    }
		//    return kill;
		//}

		public bool IsAlive
		{
			get { return _thread != null && _thread.IsAlive; }
		}

		public Theme Theme
		{
			get { lock(_themeLock) { return _theme; } }
			set
			{
				lock(_themeLock)
				{
					_theme = value;
				}
			}
		}
		#endregion

		#region Thread
		private void ThreadProc()
		{
			SwitchDir sw;

			Log.Write(LogLevel.Info, "Switch thread has started.");
			try
			{
				SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;

				while (!_kill)
				{
					sw = CheckSwitch();
					if (sw != SwitchDir.None)
					{
						try
						{
							DoSwitch(sw);
						}
						catch (Exception ex2)
						{
							Log.Write(LogLevel.Error, "Exception when switching wallpaper:\n" + ex2.ToString());
						}

						_lastSwitch = DateTime.Now;
						lock (_themeLock)
						{
							Log.Write(LogLevel.Info, "Next wallpaper switch is in {0} seconds", _theme.Interval.TotalSeconds);
						}
					}

					Thread.Sleep(k_sleepTime);
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception in switch thread.");
			}
			finally
			{
				SystemEvents.SessionSwitch -= SystemEvents_SessionSwitch;
			}

			Log.Write(LogLevel.Info, "Switch thread has ended.");
		}

		void SystemEvents_SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
		{
			switch (e.Reason)
			{
				case SessionSwitchReason.SessionLogon:
					Log.Write(LogLevel.Debug, "Detected session logon event.");
					_locked = false;
					break;

				case SessionSwitchReason.SessionLogoff:
					Log.Write(LogLevel.Debug, "Detected session logoff event.");
					_locked = true;
					break;

				case SessionSwitchReason.SessionLock:
					Log.Write(LogLevel.Debug, "Detected session lock event.");
					_locked = true;
					break;

				case SessionSwitchReason.SessionUnlock:
					Log.Write(LogLevel.Debug, "Detected session unlock event.");
					_locked = false;
					break;
			}
		}

		private SwitchDir CheckSwitch()
		{
			// Check if the user initiated an early switch.
			var switchNow = _switchNow;
			if (switchNow != SwitchDir.None)
			{
				_switchNow = SwitchDir.None;
				return switchNow;
			}

			// If the screensaver was last found to be running, then check now if the user has woken up.
			if (_screensaverRunning)
			{
				if (!ScreenSaverRunning)
				{
					Log.Write(LogLevel.Debug, "Screensaver has stopped running.");
					_screensaverRunning = false;
				}
			}

			// Check if it's time to switch
			if (!_locked && !_screensaverRunning)
			{
				lock (_themeLock)
				{
					DateTime nextSwitch = _lastSwitch + _theme.Interval;
					if (DateTime.Now >= nextSwitch)
					{
						// Check if the screensaver is running; if so, then don't switch now.
						if (ScreenSaverRunning)
						{
							Log.Write(LogLevel.Debug, "Stopping wallpaper updating because the screensaver is running.");
							_screensaverRunning = true;
							return SwitchDir.None;
						}

						return SwitchDir.Next;
					}
				}
			}

			return SwitchDir.None;
		}

		public void SwitchNow(SwitchDir dir)
		{
			if (_thread == null || !_thread.IsAlive)
			{
				Log.Write(LogLevel.Warning, "The switch thread was found to be inactive. Restarting...");
				Start(_theme);
			}

			_switchNow = dir;
		}

		private bool ScreenSaverRunning
		{
			get
			{
				bool ssRunning = false;
				if (!SystemParametersInfo(SPI_GETSCREENSAVERRUNNING, 0, ref ssRunning, 0)) ssRunning = false;
				return ssRunning;
			}
		}
		#endregion

		#region Wallpaper Switch
		SetWallpaper wallpaperSetter = new SetWallpaper();
		public delegate void SwitchEventHandler(object sender, EventArgs e);
		public event SwitchEventHandler Switching;
		public event SwitchEventHandler Switched;
		private bool _switching = false;

		private void DoSwitch(SwitchDir dir)
		{

			if (_theme == null)
			{
				Log.Write(LogLevel.Warning, "Cannot switch wallpaper; there is no current theme.");
				return;
			}
			else
			{
				Log.Write(LogLevel.Info, "Switching wallpaper for theme '{0}'", _theme.Name);
			}

			try
			{
				_switching = true;

				// Fire the Switching event handler.
				SwitchEventHandler ev = Switching;
				if (ev != null) ev(this, new EventArgs());

				// Get the list of images to display next.
				IEnumerable<ImageRec> images = null;
				switch (dir)
				{
					case SwitchDir.Next:
						images = _theme.GetNextImages(wallpaperSetter.NumMonitors);
						break;

					case SwitchDir.Prev:
						images = _theme.GetPrevImages();
						break;

					default:
						images = new ImageRec[0];
						break;
				}

				// Display the images.
				if (images != null)
				{
					foreach (var img in images) Log.Write(LogLevel.Debug, "  Image: {0}", img);
					if (images != null) wallpaperSetter.Set(_theme, images);
				}

				// Now that everything's drawn, it's safe to release that memory.
				foreach (var img in images) img.Release();

				Log.Write(LogLevel.Debug, "Finished switching wallpaper.");
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Error when switching wallpaper.");
			}
			finally
			{
				_switching = false;

				// Fire the Switched event handler.
				SwitchEventHandler ev = Switched;
				if (ev != null) ev(this, new EventArgs());
			}
		}

		public bool IsSwitching
		{
			get { return _switching; }
		}
		#endregion
	}
}
