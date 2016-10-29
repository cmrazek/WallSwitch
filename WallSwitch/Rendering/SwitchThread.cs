using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using WallSwitch.SettingsStore;
using WallSwitch.WidgetInterface;

namespace WallSwitch
{
	class SwitchThread
	{
		#region Variables
		private Thread _thread = null;
		private List<string> _files = new List<string>();
		private Random _rand = new Random();
		private int _randomGroupCounter = 0;

		private CancellationTokenSource _cancel = new CancellationTokenSource();
		private volatile SwitchDir _switchNow = SwitchDir.None;
		private volatile bool _locked = false;
		private volatile bool _screensaverRunning = false;
		private DateTime _startUpTime = DateTime.MinValue;

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
		public void Start(Database db, Theme theme)
		{
			if (theme == null) throw new ArgumentNullException("Theme is null.");
			_theme = theme;

			_lastSwitch = DateTime.MinValue;
			var str = db.LoadSetting("LastSwitch");
			if (!string.IsNullOrEmpty(str))
			{
				DateTime dt;
				if (DateTime.TryParse(str, out dt))
				{
					_lastSwitch = dt;
					Log.Debug("Last switch time was: {0}", _lastSwitch);
				}
			}

			_thread = new Thread(new ThreadStart(ThreadProc));
			_thread.Name = "Switch Thread";
			_thread.SetApartmentState(ApartmentState.STA);
			_thread.Start();
		}

		public void Kill()
		{
			if (_thread != null)
			{
				if (_thread.IsAlive)
				{
					_cancel.Cancel();
					_thread.Join();
				}

				_thread = null;
			}
		}

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
					if (_theme != value)
					{
						Log.Debug("Theme is switching from '{0}' to '{1}'.",
							_theme != null ? _theme.Name : "(null)", value != null ? value.Name : "(null)");
						_theme = value;
						_randomGroupCounter = 0;
					}
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
				SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;

				while (!_cancel.IsCancellationRequested)
				{
					sw = CheckSwitch();
					if (sw != SwitchDir.None)
					{
						using (var db = new Database())
						{
							try
							{
								DoSwitch(db, sw);
							}
							catch (Exception ex2)
							{
								Log.Write(LogLevel.Error, "Exception when switching wallpaper:\n" + ex2.ToString());
							}

							_lastSwitch = DateTime.Now;
							db.WriteSetting("LastSwitch", _lastSwitch.ToString("s"));
							lock (_themeLock)
							{
								Log.Write(LogLevel.Info, "Next wallpaper switch is in {0} seconds", _theme.Interval.TotalSeconds);
							}
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

		void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
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

		void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
		{
			Log.Write(LogLevel.Debug, "Detected PowerModeChanged event: {0}", e.Mode);
			if (e.Mode == PowerModes.Resume)
			{
				SetStartUpDelay();
			}
		}

		public void SetStartUpDelay()
		{
			_startUpTime = DateTime.Now;
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

			if (_paused) return SwitchDir.None;

			if (_startUpTime != DateTime.MinValue)
			{
				var startUpDelay = Settings.StartUpDelay;
				if (startUpDelay > 0 && _startUpTime.AddSeconds(startUpDelay) > DateTime.Now)
				{
					return SwitchDir.None;
				}
				else
				{
					_startUpTime = DateTime.MinValue;
				}
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

		public void SwitchNow(Database db, SwitchDir dir)
		{
			if (_thread == null || !_thread.IsAlive)
			{
				Log.Write(LogLevel.Warning, "The switch thread was found to be inactive. Restarting...");
				Start(db, _theme);
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
		private SetWallpaper _wallpaperSetter = new SetWallpaper();
		public delegate void SwitchEventHandler(object sender, EventArgs e);
		public event SwitchEventHandler Switching;
		public event SwitchEventHandler Switched;
		private bool _switching = false;
		private volatile bool _paused = false;

		private void DoSwitch(Database db, SwitchDir dir)
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

				_wallpaperSetter.Set(db, _theme, dir, false, ref _randomGroupCounter, _cancel.Token);

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

		public bool Paused
		{
			get { return _paused; }
			set { _paused = value; }
		}

		public SetWallpaper WallpaperSetter
		{
			get { return _wallpaperSetter; }
		}

		public int RandomGroupCounter
		{
			get { return _randomGroupCounter; }
		}
		#endregion
	}
}
