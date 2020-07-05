using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml;
using Newtonsoft.Json.Linq;

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

		private static HttpClient _httpClient;

		public event EventHandler<UpdateCheckEventArgs> UpdateAvailable;
		public event EventHandler<UpdateCheckEventArgs> NoUpdateAvailable;
		public event EventHandler<UpdateCheckEventArgs> UpdateCheckFailed;

		public async Task CheckAsync()
		{
			try
			{
				Log.Write(LogLevel.Info, "Checking for updates...");

				_exeVersion = AssemblyVersion;
				Log.Write(LogLevel.Info, "Current version: {0}", _exeVersion.ToAppFormat());

				_webVersion = await GetLatestVersionAsync();
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

		private async Task<Version> GetLatestVersionAsync()
		{
			if (_httpClient == null) _httpClient = new HttpClient();

			var rq = new HttpRequestMessage(HttpMethod.Get, Res.UpdateCheckLatestReleaseUrl);
			rq.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
			rq.Headers.TryAddWithoutValidation("User-Agent", Res.UpdateCheckUserAgent);

			var rs = await _httpClient.SendAsync(rq);
			if (!rs.IsSuccessStatusCode)
			{
				Log.Warning("Server returned status code {0} {1} when checking for updates.", (int)rs.StatusCode, rs.StatusCode);
				return new Version(0, 0);
			}

			var rspBody = await rs.Content.ReadAsStringAsync();
			var rspJson = JToken.Parse(rspBody);

			var rspObject = rspJson as JObject;
			if (rspObject != null)
			{
				var tagNameProp = rspObject["tag_name"];
				if (tagNameProp != null)
				{
					var versionString = tagNameProp.Value<string>();
					if (versionString.StartsWith("v")) versionString = versionString.Substring(1);
					return Version.Parse(versionString);
				}
			}

			return new Version(0, 0);
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
