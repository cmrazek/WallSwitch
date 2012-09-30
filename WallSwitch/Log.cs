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
		private static StreamWriter _file = null;
		private static bool _enabled = false;
		private static LogLevel _level = LogLevel.Debug;

		/// <summary>
		/// Locked
		/// </summary>
		private static StringBuilder _sb = new StringBuilder();
		#endregion

		#region Construction
		/// <summary>
		/// Opens the log file.
		/// </summary>
		public static void Initialize()
		{
			try
			{
				string logFileName = LogFileName;
				_file = new StreamWriter(logFileName, false);
				Write(LogLevel.Info, "Log file opened: {0}", logFileName);

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
				if (_file != null)
				{
					_file.WriteLine("[" + TimeStamp + "] Closing log file.");
					_file.Close();
					_file = null;
				}
			}
#if DEBUG
			catch (Exception ex)
			{
				Debug.WriteLine("Error when closing log file:");
				Debug.WriteLine(ex.ToString());
				_file = null;
			}
#else
			catch (Exception)
			{
				_file = null;
			}
#endif
		}

		private static string LogFileName
		{
			get
			{
				return Path.Combine(Util.AppDataDir, Res.LogFile);
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

				lock (_sb)
				{
					_sb.Clear();
					_sb.Append("[");
					_sb.Append(TimeStamp);
					_sb.Append("] ");
					_sb.Append(line);

					var logEntry = _sb.ToString();
					if (_file != null) _file.WriteLine(logEntry);
#if DEBUG
					Debug.WriteLine(logEntry);
#endif
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

				lock (_sb)
				{
					_sb.Clear();
					_sb.Append("[");
					_sb.Append(TimeStamp);
					_sb.Append("] ");
					_sb.Append(comment);

					var logEntry = _sb.ToString();
					if (_file != null)
					{
						_file.WriteLine(logEntry);
						_file.WriteLine(ex.ToString());
					}

#if DEBUG
					Debug.WriteLine(logEntry);
					Debug.WriteLine(ex.ToString());
#endif
				}
			}
			catch (Exception)
			{ }
		}

		public static void Write(LogLevel level, string format, params object[] args)
		{
			if (!_enabled || level < _level) return;
			Write(level, String.Format(format, args));
		}

		public static void Write(Exception ex, string format, params object[] args)
		{
			if (!_enabled) return;
			Write(ex, String.Format(format, args));
		}

		public static void Flush()
		{
			if (_file != null)
			{
				_file.Flush();
			}
		}
		#endregion
	}
}
