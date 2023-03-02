using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using WallSwitch.Core;
using System.Linq;

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
		private static List<LogEntry> _cache = new List<LogEntry>();

		/// <summary>
		/// Locked
		/// </summary>
		private static StringBuilder _sb = new StringBuilder();
		#endregion

		#region Constants
		public const string DateFormat = "yyyy/MM/dd HH:mm:ss.fff";
		private const int PurgeDays = 14;
		public const int MaxCache = 300;
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
                _enabled = true;
                Write(LogLevel.Info, "Log file opened: {0}", logFileName);
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
					_file.WriteLine("[" + DateTime.Now.ToString(DateFormat) + "] Closing log file.");
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
		public static event EventHandler<LogEntryEventArgs> LogEntryAdded;
		public struct LogEntryEventArgs
		{
			public LogEntry Entry { get; private set; }

			public LogEntryEventArgs(LogEntry entry)
			{
				Entry = entry ?? throw new ArgumentNullException(nameof(entry));
			}
		}

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

				var timeStamp = DateTime.Now;

				lock (_sb)
				{
					_sb.Clear();
					_sb.Append("[");
					_sb.Append(timeStamp.ToString(DateFormat));
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
					var entry = new LogEntry(timeStamp, level, line);
                    AddToCache(entry);
                    LogEntryAdded?.Invoke(null, new LogEntryEventArgs(entry));
                }

			}
			catch (Exception)
			{ }
		}

		/// <summary>
		/// Writes an exception to the log file.
		/// The entry is prefixed with the current date/time.
		/// </summary>
		/// <param name="ex">The exception</param>
		/// <param name="comment">A comment to accompany this exception</param>
		public static void Write(Exception ex, string comment)
		{
			if (!_enabled) return;
			Write(LogLevel.Error, String.Concat(comment, Environment.NewLine, ex));
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

		public static void Error(Exception ex, string message)
		{
			Write(LogLevel.Error, string.Concat(message, " - ", ex));
		}

		public static void Error(Exception ex, string format, params object[] args)
		{
			Write(LogLevel.Error, string.Concat(string.Format(format, args), " - ", ex));
		}

		public static void Flush()
		{
			if (_file != null)
			{
				_file.Flush();
			}
		}
		#endregion

		#region Cache
		private static void AddToCache(LogEntry entry)
		{
			lock (_cache)
			{
				_cache.Add(entry);
				while (_cache.Count > MaxCache) _cache.RemoveAt(0);
			}
		}

		public static IEnumerable<LogEntry> GetLogs(LogLevel? levelFilter)
		{
			lock (_cache)
			{
				foreach (var entry in _cache)
				{
					if (levelFilter.HasValue && entry.Severity < levelFilter.Value) continue;
					yield return entry;
				}
			}
		}

		public static void Clear()
		{
			lock (_cache)
			{
				_cache.Clear();
			}
		}

		public static void RemoveEntry(LogEntry entry)
		{
			lock (_cache)
			{
				_cache.Remove(entry);
			}
		}

		public static int GetErrorCount()
		{
			lock (_cache)
			{
				return _cache.Count(x => x.Severity == LogLevel.Error);
			}
		}
		#endregion
	}
}
