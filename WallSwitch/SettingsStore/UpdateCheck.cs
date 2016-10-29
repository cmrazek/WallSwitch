using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;
using System.Xml;

namespace WallSwitch.SettingsStore
{
	public class UpdateCheckEventArgs : EventArgs
	{
		public string UpdateUrl { get; set; }
		public Version ExeVersion { get; set; }
		public Version WebVersion { get; set; }

		public void OpenUpdateUrl()
		{
			if (!string.IsNullOrEmpty(UpdateUrl)) UpdateChecker.OpenUpdateUrl(UpdateUrl);
		}
	}

	class UpdateChecker
	{
		private Version _exeVersion = null;
		private Version _webVersion = null;
		private string _updateUrl = "";

		public event EventHandler<UpdateCheckEventArgs> UpdateAvailable;
		public event EventHandler<UpdateCheckEventArgs> NoUpdateAvailable;
		public event EventHandler<UpdateCheckEventArgs> UpdateCheckFailed;

		public void Check()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(DoCheck));
		}

		private void DoCheck(object obj)
		{
			try
			{
				Log.Write(LogLevel.Info, "Checking for updates...");

				_exeVersion = AssemblyVersion;
				Log.Write(LogLevel.Info, "Current version: {0}", _exeVersion.ToAppFormat());

				_webVersion = GetLatestVersion();
				if (_webVersion == null)
				{
					Log.Write(LogLevel.Info, "No version could be found from the web.");
					return;
				}
				Log.Write(LogLevel.Info, "Web version: {0}", _webVersion.ToAppFormat());
				Log.Write(LogLevel.Debug, "Update URL: {0}", _updateUrl);

				if (_exeVersion < _webVersion)
				{
					Log.Write(LogLevel.Info, "A new version is available.");
					var ev = UpdateAvailable;
					if (ev != null) ev(this, new UpdateCheckEventArgs
					{
						UpdateUrl = _updateUrl,
						ExeVersion = _exeVersion,
						WebVersion = _webVersion
					});
				}
				else
				{
					Log.Write(LogLevel.Info, "WallSwitch is up to date.");
					var ev = NoUpdateAvailable;
					if (ev != null) ev(this, new UpdateCheckEventArgs
					{
						ExeVersion = _exeVersion,
						WebVersion = _webVersion,
						UpdateUrl = _updateUrl
					});
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Error when checking for updates.");

				var ev = UpdateCheckFailed;
				if (ev != null) ev(this, new UpdateCheckEventArgs());
			}
		}

		public static Version AssemblyVersion
		{
			get { return Assembly.GetExecutingAssembly().GetName().Version; }
		}

		private Version GetLatestVersion()
		{
			var xmlDoc = GetFeedXml();
			if (xmlDoc == null) return null;

			var rx = new Regex(@"^.*release.*:\s+WallSwitch\s+(\d+\.\d+\.*\d*)", RegexOptions.IgnoreCase);

			var titleNode = (from n in xmlDoc.SelectNodes("/rss/channel/item/title").Cast<XmlNode>()
			                 where rx.IsMatch(n.InnerText)
			                 select n).FirstOrDefault();
			if (titleNode == null) return null;	// No version information?

			var linkNode = titleNode.ParentNode.SelectSingleNode("link");
			if (linkNode == null) return null;
			_updateUrl = linkNode.InnerText;

			return new Version(rx.Match(titleNode.InnerText).Groups[1].Value);
		}

		private XmlDocument GetFeedXml()
		{
			var request = (HttpWebRequest)HttpWebRequest.Create(Res.UpdateRssUrl);
			request.Method = "GET";

			var response = request.GetResponse();
			using (var sr = new StreamReader(response.GetResponseStream()))
			{
				var xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(sr.ReadToEnd());
				return xmlDoc;
			}
		}

		public string Url
		{
			get { return _updateUrl; }
		}

		public static void OpenUpdateUrl(string url)
		{
			try
			{
				var uri = new Uri(url);
				if (uri.IsFile) throw new InvalidOperationException("Invalid update URL.");
				Process.Start(uri.ToString());
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception when attempting to open the update page.");
			}
		}
	}
}
