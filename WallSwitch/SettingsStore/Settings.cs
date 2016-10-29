using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Win32;
using System.Windows.Forms;

namespace WallSwitch.SettingsStore
{
	class Settings
	{
		#region Constants
		private const bool k_defaultStartWithWindows = false;
		private const bool k_defaultCheckForUpdatesOnStartup = true;
		private const int k_defaultStartUpDelay = 0;
		private const bool k_defaultIgnoreHiddenFiles = true;
#if DEBUG
		private const LogLevel k_defaultLogLevel = LogLevel.Debug;
#else
		private const LogLevel k_defaultLogLevel = LogLevel.Info;
#endif
		private const double k_defaultTransparency = 1.0;
		private const bool k_defaultLoadHistoryImages = true;

		public const string RegistryKey = "Software\\WallSwitch";
		#endregion

		#region Variables
		private static bool _startWithWindows = k_defaultStartWithWindows;
		private static bool _checkForUpdatesOnStartup = k_defaultCheckForUpdatesOnStartup;
		private static int _startUpDelay = k_defaultStartUpDelay;
		private static bool _ignoreHiddenFiles = k_defaultIgnoreHiddenFiles;
		private static LogLevel _logLevel = k_defaultLogLevel;
		private static double _transparency = k_defaultTransparency;
		private static bool _loadHistoryImages = k_defaultLoadHistoryImages;
		#endregion

		/// <summary>
		/// Initializes all values when the program launches.
		/// </summary>
		public static void Initialize()
		{
			try
			{
				Log.Write(LogLevel.Debug, "Initializing settings.");
				GetStartWithWindowsFromRegistry();
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Error when initializing settings.");
			}
		}

		public static void Save(XmlWriter xml)
		{
			xml.WriteElementString("CheckForUpdates", _checkForUpdatesOnStartup.ToString());
			xml.WriteElementString("StartUpDelay", _startUpDelay.ToString());
			if (_ignoreHiddenFiles != k_defaultIgnoreHiddenFiles) xml.WriteElementString("IgnoreHiddenFiles", _ignoreHiddenFiles.ToString());
			xml.WriteElementString("LogLevel", _logLevel.ToString());
			if (_transparency != k_defaultTransparency) xml.WriteElementString("Transparency", _transparency.ToString());
		}

		public static void Save(Database db)
		{
			db.WriteSetting("CheckForUpdates", _checkForUpdatesOnStartup.ToString());
			db.WriteSetting("StartUpDelay", _startUpDelay.ToString());
			db.WriteSetting("IgnoreHiddenFiles", _ignoreHiddenFiles.ToString());
			db.WriteSetting("LogLevel", _logLevel.ToString());
			db.WriteSetting("Transparency", _transparency.ToString());
			db.WriteSetting("LoadHistoryImages", _loadHistoryImages.ToString());
		}

		public static void Load(Database db)
		{
			bool bValue;
			if (bool.TryParse(db.LoadSetting("CheckForUpdates"), out bValue)) _checkForUpdatesOnStartup = bValue;

			int iValue;
			if (int.TryParse(db.LoadSetting("StartUpDelay"), out iValue)) _startUpDelay = iValue;

			if (bool.TryParse(db.LoadSetting("IgnoreHiddenFiles"), out bValue)) _ignoreHiddenFiles = bValue;

			LogLevel logLevel;
			if (Enum.TryParse<LogLevel>(db.LoadSetting("LogLevel"), out logLevel)) _logLevel = logLevel;
			Log.Level = _logLevel;

			double dValue;
			if (double.TryParse(db.LoadSetting("Transparency"), out dValue)) _transparency = dValue;

			if (bool.TryParse(db.LoadSetting("LoadHistoryImages"), out bValue)) _loadHistoryImages = bValue;
		}

		public static void Load(XmlElement xmlSettings)
		{
			var xml = xmlSettings.SelectSingleNode("CheckForUpdates") as XmlElement;
			if (xml != null) _checkForUpdatesOnStartup = Util.ParseBool(xml.InnerText, k_defaultCheckForUpdatesOnStartup);

			int i;
			xml = xmlSettings.SelectSingleNode("StartUpDelay") as XmlElement;
			if (xml != null && int.TryParse(xml.InnerText, out i)) _startUpDelay = i;

			bool bValue;
			xml = xmlSettings.SelectSingleNode("IgnoreHiddenFiles") as XmlElement;
			if (xml != null && bool.TryParse(xml.InnerText, out bValue)) _ignoreHiddenFiles = bValue;

			LogLevel logLevel;
			xml = xmlSettings.SelectSingleNode("LogLevel") as XmlElement;
			if (xml != null && Enum.TryParse<LogLevel>(xml.InnerText, true, out logLevel)) _logLevel = logLevel;
			Log.Level = _logLevel;

			double dValue;
			xml = xmlSettings.SelectSingleNode("Transparency") as XmlElement;
			if (xml != null && double.TryParse(xml.InnerText, out dValue)) _transparency = dValue;
		}

		private static void GetStartWithWindowsFromRegistry()
		{
			RegistryKey key = null;
			try
			{
				_startWithWindows = false;

				string startValue = Application.ExecutablePath + " -winstart";

				key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false);
				string path = Convert.ToString(key.GetValue(Res.AppNameIdent));
				_startWithWindows = !string.IsNullOrEmpty(path) && path.ToLower() == WinStartCommand.ToLower();
			}
			catch(Exception ex)
			{
				Log.Write(ex, "Error when detecting 'start with windows' flag.");
			}
			finally
			{
				if (key != null) key.Close();
			}
		}

		/// <summary>
		/// Gets or sets the flag indicating if this application will start-up with Windows.
		/// </summary>
		public static bool StartWithWindows
		{
			get { return _startWithWindows; }
			set
			{
				RegistryKey key = null;
				try
				{
					if (_startWithWindows != value)
					{
						_startWithWindows = value;

						key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
						if (_startWithWindows)
						{
							Log.Write(LogLevel.Debug, "Enabling 'start with windows'.");
							key.SetValue(Res.AppNameIdent, WinStartCommand);
						}
						else
						{
							Log.Write(LogLevel.Debug, "Disabling 'start with windows'.");
							string[] values = key.GetValueNames();
							foreach (string val in values)
							{
								if (val == Res.AppNameIdent)
								{
									key.DeleteValue(Res.AppNameIdent);
									break;
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					Log.Write(ex, "Error when setting 'start with windows' flag.");
					throw ex;
				}
				finally
				{
					if (key != null) key.Close();
				}
			}
		}

		private static string WinStartCommand
		{
			get
			{
				return "\"" + Application.ExecutablePath + "\" -winstart";
			}
		}

		public static bool CheckForUpdatesOnStartup
		{
			get { return _checkForUpdatesOnStartup; }
			set { _checkForUpdatesOnStartup = value; }
		}

		public static int StartUpDelay
		{
			get { return _startUpDelay; }
			set { _startUpDelay = value; }
		}

		public static bool IgnoreHiddenFiles
		{
			get { return _ignoreHiddenFiles; }
			set { _ignoreHiddenFiles = value; }
		}

		public static LogLevel LogLevel
		{
			get { return _logLevel; }
			set
			{
				_logLevel = value;
				Log.Level = value;
			}
		}

		public static double Transparency
		{
			get { return _transparency; }
			set { _transparency = value; }
		}

		public static bool LoadHistoryImages
		{
			get { return _loadHistoryImages; }
			set { _loadHistoryImages = value; }
		}
	}
}
