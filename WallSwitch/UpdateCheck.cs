using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;
using System.Xml;

namespace WallSwitch
{
	public class UpdateCheckEventArgs : EventArgs
	{
		public string UpdateUrl { get; set; }
		public Version ExeVersion { get; set; }
		public Version WebVersion { get; set; }
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
				_exeVersion = Assembly.GetExecutingAssembly().GetName().Version;

				_webVersion = GetLatestVersion();
				if (_webVersion == null) return;

				if (_exeVersion < _webVersion)
				{
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
					var ev = NoUpdateAvailable;
					if (ev != null) ev(this, new UpdateCheckEventArgs
					{
						ExeVersion = _exeVersion,
						WebVersion = _webVersion
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

		private Version GetLatestVersion()
		{
			var xmlDoc = GetFeedXml();
			if (xmlDoc == null) return null;

			var rx = new Regex(@"^Released:\s+WallSwitch\s+(\d+\.\d+\.*\d*)");

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
	}
}
