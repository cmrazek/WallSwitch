using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;

namespace WallSwitch
{
	class SwitchThread
	{
		#region Variables
		Thread _thread = null;
		List<string> _files = new List<string>();
		Random _rand = new Random();

		object _controlLock = new object();
		bool _kill = false;
		SwitchDir _switchNow = SwitchDir.None;
		
		object _themeLock = new object();
		Theme _theme = null;
		DateTime _lastSwitch = DateTime.MinValue;
		#endregion

		#region Constants
		private const int k_sleepTime = 250;
		#endregion

		#region Construction
		public void Start(Theme theme)
		{
			if (theme == null) throw new ArgumentNullException("Theme is null.");
			_theme = theme;

			lock (_controlLock)
			{
				_kill = false;
			}

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
					lock (_controlLock)
					{
						_kill = true;
					}
					_thread.Join();
				}

				_thread = null;
			}
		}

		private bool CheckKill()
		{
			bool kill;
			lock (_controlLock)
			{
				kill = _kill;
			}
			return kill;
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
				while (!CheckKill())
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

			Log.Write(LogLevel.Info, "Switch thread has ended.");
		}

		private SwitchDir CheckSwitch()
		{
			lock (_controlLock)
			{
				if (_switchNow != SwitchDir.None)
				{
					SwitchDir ret = _switchNow;
					_switchNow = SwitchDir.None;
					return ret;
				}
			}

			lock (_themeLock)
			{
				DateTime nextSwitch = _lastSwitch + _theme.Interval;
				if (DateTime.Now >= nextSwitch) return SwitchDir.Next;
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

			lock (_controlLock)
			{
				_switchNow = dir;
			}
		}
		#endregion

		#region Wallpaper Switch
		SetWallpaper sw = new SetWallpaper();
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
				lock (_controlLock)
				{
					_switching = true;
				}

				// Fire the Switching event handler.
				SwitchEventHandler ev = Switching;
				if (ev != null) ev(this, new EventArgs());

				// Get the list of images to display next.
				string[] images = null;
				switch (dir)
				{
					case SwitchDir.Next:
						images = _theme.GetNextImages(sw.NumMonitors);
						break;

					case SwitchDir.Prev:
						images = _theme.GetPrevImages();
						break;

					default:
						images = new string[0];
						break;
				}

				// Display the images.
				if (images != null)
				{
					foreach (string img in images) Log.Write(LogLevel.Debug, "  Image: " + img);
					if (images != null) sw.Set(_theme, images);
				}

				Log.Write(LogLevel.Debug, "Finished switching wallpaper.");
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Error when switching wallpaper.");
			}
			finally
			{
				lock (_controlLock)
				{
					_switching = false;
				}

				// Fire the Switched event handler.
				SwitchEventHandler ev = Switched;
				if (ev != null) ev(this, new EventArgs());
			}
		}

		public bool IsSwitching
		{
			get { lock (_controlLock) { return _switching; } }
		}
		#endregion
	}
}
