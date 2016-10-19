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

		#region Constants
		public const string DateFormat = "yyyy/MM/dd HH:mm:ss.fff";
		private const int PurgeDays = 14;
		#endregion

		#region Construction
		/// <summary>
		/// Opens the log file.
		/// </summary>
		public static void Initialize()
		{
			try
			{
				PurgeOldLogFiles();

				string logFileName = LogFileName;
				_file = new StreamWriter(logFileName, false);
				Write(LogLevel.Info, "Log file opened: {0}", logFileName);

				_enabled = true;
			}
#if DEBUG
			catch (Exception ex)
			{
                System.Diagnostics.Debug.WriteLine("Error when opening log file:");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
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
                System.Diagnostics.Debug.WriteLine("Error when closing log file:");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
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
				return Path.Combine(Util.AppDataDir, string.Format(Res.LogFile, DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss")));
			}
		}

		/// <summary>
		/// Gets the logging level that will be extracted to the file.
		/// </summary>
		public static LogLevel Level
		{
			get { return _level; }
			set
			{
				if (_level != value)
				{
					_level = value;
					Write(LogLevel.Info, "Logging level has been changed to '{0}'.", value);
				}
			}
		}

		private static void PurgeOldLogFiles()
		{
			try 
			{	        
				var minPurgeDate = DateTime.Now.AddDays(PurgeDays * -1);

				foreach (var fileName in Directory.GetFiles(Util.AppDataDir, "*.log"))
				{
					if (File.GetLastWriteTime(fileName) <= minPurgeDate)
					{
						try 
						{
							Write(LogLevel.Info, "Purging old log file: {0}", fileName);
							File.Delete(fileName);
						}
						catch (Exception ex)
						{
							Write(ex, "Exception when deleting old log file: {0}", fileName);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Write(ex, "Exception when purging old log files.");
			}
			
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
					_sb.Append("] (");
					_sb.Append(level.ToString());
					_sb.Append(") ");
					_sb.Append(line);

					var logEntry = _sb.ToString();
					if (_file != null)
					{
						_file.WriteLine(logEntry);
						_file.Flush();
					}
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(logEntry);
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
				return DateTime.Now.ToString(DateFormat);
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
					_sb.Append("] (Error) ");
					_sb.Append(comment);

					var logEntry = _sb.ToString();
					if (_file != null)
					{
						_file.WriteLine(logEntry);
						_file.WriteLine(ex.ToString());
						//_file.Flush();
					}

#if DEBUG
                    System.Diagnostics.Debug.WriteLine(logEntry);
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
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

		public static void Write(Exception ex)
		{
			if (!_enabled) return;
			Write(ex, "");
		}

		public static void Debug(string message)
		{
			Write(LogLevel.Debug, message);
		}

		public static void Debug(string format, params object[] args)
		{
			Write(LogLevel.Debug, format, args);
		}

        public static void Info(string message)
		{
			Write(LogLevel.Info, message);
		}

		public static void Info(string format, params object[] args)
		{
			Write(LogLevel.Info, format, args);
		}

        public static void Warning(string message)
        {
            Write(LogLevel.Warning, message);
        }

        public static void Warning(string format, params object[] args)
		{
			Write(LogLevel.Warning, format, args);
		}

		public static void Warning(Exception ex)
		{
			Write(LogLevel.Warning, ex.ToString());
		}

        public static void Error(string message)
        {
            Write(LogLevel.Error, message);
        }

        public static void Error(string format, params object[] args)
		{
			Write(LogLevel.Error, format, args);
		}

		public static void Error(Exception ex)
		{
			Write(LogLevel.Error, ex.ToString());
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
