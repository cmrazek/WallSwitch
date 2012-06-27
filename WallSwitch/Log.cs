using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
#if DEBUG
using System.Diagnostics;
#endif

namespace WallSwitch
{
	public enum LogLevel
	{
		Debug,
		Info,
		Warning,
		Error
	}

	public class Log
	{
		#region Variables
		private static Thread _thread = null;
		private static StreamWriter _file = null;
		private static bool _enabled = false;
		private static LogLevel _level = LogLevel.Debug;

		/// <summary>
		/// Locked
		/// </summary>
		private static StringBuilder _sb = new StringBuilder();

		/// <summary>
		/// Locked using _killLock
		/// </summary>
		private static bool _kill = false;
		private static object _killLock = new object();

		/// <summary>
		/// Locked
		/// </summary>
		private static Queue<string> _queue = new Queue<string>();
		#endregion

		#region Constants
		private const int k_sleepTime = 250;
		#endregion

		#region Thread Management
		/// <summary>
		/// Opens the log file.
		/// </summary>
		public static void Initialize()
		{
			try
			{
				_thread = new Thread(new ThreadStart(ThreadProc));
				_thread.Name = "Logging Thread";
				_thread.Start();
				_enabled = true;
			}
#if DEBUG
			catch (Exception ex)
			{

				Debug.WriteLine("Error when opening log file:");
				Debug.WriteLine(ex.ToString());

			}
#else
			catch (Exception)
			{ }
#endif
		}

		/// <summary>
		/// Closes the log file.
		/// </summary>
		public static void Close()
		{
			try
			{
				if (_thread != null && _thread.IsAlive)
				{
					lock (_killLock)
					{
						_kill = true;
					}
					_thread.Join();
				}
			}
#if DEBUG
			catch (Exception ex)
			{
				Debug.WriteLine("Error when closing log file:");
				Debug.WriteLine(ex.ToString());
			}
#else
			catch (Exception)
			{ }
#endif
		}

		private static string LogFileName
		{
			get
			{
				string dir = String.Format(Res.SettingsDir, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
				if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
				return dir + Path.DirectorySeparatorChar + Res.LogFile;
			}
		}

		private static void ThreadProc()
		{
			try
			{
				string logFileName = LogFileName;
				_file = new StreamWriter(logFileName, false);
				Write(LogLevel.Info, "Log file opened: {0}", logFileName);

				bool activity;

				while (!CheckKill())
				{
					activity = false;

					lock (_queue)
					{
						while (_queue.Count > 0)
						{
							_file.WriteLine(_queue.Dequeue());
							activity = true;
						}

						if (activity) _file.Flush();
					}

					Thread.Sleep(k_sleepTime);
				}
			}
#if DEBUG
			catch (Exception ex)
			{

				Debug.WriteLine("Exception in logging thread:");
				Debug.WriteLine(ex.ToString());

			}
#else
			catch (Exception)
			{ }
#endif
			finally
			{
				if (_file != null)
				{
					_file.WriteLine("[" + TimeStamp + "] Closing log file.");
					_file.Close();
					_file = null;
				}
			}

		}

		private static bool CheckKill()
		{
			lock (_killLock)
			{
				return _kill;
			}
		}

		/// <summary>
		/// Gets the logging level that will be extracted to the file.
		/// </summary>
		public static LogLevel Level
		{
			get { return _level; }
			set { _level = value; }
		}
		#endregion

		#region Writing
		/// <summary>
		/// Writes an entry to the log file.
		/// The log entry will be prefixed with the current date/time.
		/// </summary>
		/// <param name="line">The text to be written.</param>
		public static void Write(LogLevel level, string line)
		{
			try
			{
				if (!_enabled || level < _level) return;

				string logEntry;
				lock (_sb)
				{
					_sb.Clear();
					_sb.Append("[");
					_sb.Append(TimeStamp);
					_sb.Append("] ");
					_sb.Append(line);

					logEntry = _sb.ToString();
				}

#if DEBUG
				Debug.WriteLine(logEntry);
#endif

				lock (_queue)
				{
					_queue.Enqueue(logEntry);
				}
			}
			catch (Exception)
			{ }
		}

		private static string TimeStamp
		{
			get
			{
				return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
			}
		}

		/// <summary>
		/// Writes an exception to the log file.
		/// The entry is prefixed with the current date/time.
		/// </summary>
		/// <param name="ex">The exception</param>
		/// <param name="comment">A comment to accompany this exception</param>
		public static void Write(Exception ex, string comment)
		{
			try
			{
				if (!_enabled) return;

				string logEntry;
				lock (_sb)
				{
					_sb.Clear();
					_sb.Append("[");
					_sb.Append(TimeStamp);
					_sb.Append("] ");
					_sb.Append(comment);

					logEntry = _sb.ToString();
				}

#if DEBUG
				Debug.WriteLine(logEntry);
				Debug.WriteLine(ex.ToString());
#endif

				lock (_queue)
				{
					_queue.Enqueue(logEntry);
					_queue.Enqueue(ex.ToString());
				}
			}
			catch (Exception)
			{ }
		}

		public static void Write(LogLevel level, string format, object a1)
		{
			if (!_enabled || level < _level) return;
			Write(level, String.Format(format, a1));
		}

		public static void Write(LogLevel level, string format, object a1, object a2)
		{
			if (!_enabled || level < _level) return;
			Write(level, String.Format(format, a1, a2));
		}

		public static void Write(LogLevel level, string format, object a1, object a2, object a3)
		{
			if (!_enabled || level < _level) return;
			Write(level, String.Format(format, a1, a2, a3));
		}

		public static void Write(Exception ex, string format, object a1)
		{
			if (!_enabled) return;
			Write(ex, String.Format(format, a1));
		}

		public static void Write(Exception ex, string format, object a1, object a2)
		{
			if (!_enabled) return;
			Write(ex, String.Format(format, a1, a2));
		}

		public static void Write(Exception ex, string format, object a1, object a2, object a3)
		{
			if (!_enabled) return;
			Write(ex, String.Format(format, a1, a2, a3));
		}
		#endregion


	}
}
