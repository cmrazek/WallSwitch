using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Win32;
using System.Windows.Forms;

namespace WallSwitch
{
	class Settings
	{
		#region Constants
		private const bool k_defaultStartWithWindows = false;
		private const bool k_defaultCheckForUpdatesOnStartup = true;
		private const int k_defaultStartUpDelay = 0;
		#endregion

		#region Variables
		private static bool _startWithWindows = k_defaultStartWithWindows;
		private static bool _checkForUpdatesOnStartup = k_defaultCheckForUpdatesOnStartup;
		private static int _startUpDelay = k_defaultStartUpDelay;
		#endregion

		#region XML Tag Names
		private const string k_checkForUpdates = "CheckForUpdates";
		private const string k_startUpDelay = "StartUpDelay";
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

		#region Save / Load
		public static void Save(XmlWriter xml)
		{
			xml.WriteElementString(k_checkForUpdates, _checkForUpdatesOnStartup.ToString());
			xml.WriteElementString(k_startUpDelay, _startUpDelay.ToString());
		}

		public static void Load(XmlElement xmlSettings)
		{
			var xml = xmlSettings.SelectSingleNode(k_checkForUpdates) as XmlElement;
			if (xml != null) _checkForUpdatesOnStartup = Util.ParseBool(xml.InnerText, k_defaultCheckForUpdatesOnStartup);

			int i;
			xml = xmlSettings.SelectSingleNode(k_startUpDelay) as XmlElement;
			if (xml != null && int.TryParse(xml.InnerText, out i)) _startUpDelay = i;
		}
		#endregion

		#region Start With Windows
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
		#endregion

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

	}
}
