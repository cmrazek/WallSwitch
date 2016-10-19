using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using HtmlAgilityPack;

#if DEBUG
using System.IO;
#endif

namespace WallSwitch
{
	class FeedLoader
	{
		private List<ImageRec> _images = new List<ImageRec>();

		public bool LoadUrl(string url)
		{
			try
			{
				Log.Write(LogLevel.Info, "Downloading feed: {0}", url);

				_images.Clear();

				var uri = new Uri(url);
				var req = (HttpWebRequest)HttpWebRequest.Create(uri);
				req.Timeout = 10000;
				var rsp = (HttpWebResponse)req.GetResponse();
				var stream = rsp.GetResponseStream();

				var xmlDoc = new XmlDocument();
				xmlDoc.Load(rsp.GetResponseStream());

#if DEBUG
				var outDir = Path.Combine(Util.AppDataDir, "FeedLog");
				if (!Directory.Exists(outDir)) Directory.CreateDirectory(outDir);

				string fileName;
				var fileNameIndex = 0;
				do
				{
					fileName = Path.Combine(outDir, string.Format("{0} {1}", uri.Host, DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss")));
					if (fileNameIndex > 0) fileName += string.Format(" ({0})", fileNameIndex);
					fileNameIndex++;
					fileName += ".xml";
				}
				while (File.Exists(fileName));

				xmlDoc.Save(fileName);
#endif

				var rootElement = xmlDoc.DocumentElement;
				if (rootElement.Name == "rss")
				{
					Log.Write(LogLevel.Debug, "This is an RSS feed.");
					LoadRss(xmlDoc);
					return true;
				}

				if (rootElement.Name == "feed")
				{
					Log.Write(LogLevel.Debug, "This is an ATOM feed.");
					LoadAtom(xmlDoc);
					return true;
				}

				Log.Write(LogLevel.Warning, "Unable to determine the type of feed.");
				return false;
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Error when loading feed '{0}'.", url);
				return false;
			}
		}

		private void LoadRss(XmlDocument xmlDoc)
		{
			foreach (XmlElement item in xmlDoc.GetElementsByTagName("item"))
			{
				var pubDate = GetPubDateForItem(item);

				// Check for an image element.
				var node = item.SelectSingleNode("image");
				if (node != null && node.NodeType == XmlNodeType.Element)
				{
					node = node.SelectSingleNode("url");
					if (node != null && node.NodeType == XmlNodeType.Element)
					{
						var url = node.InnerText;
						if (!string.IsNullOrWhiteSpace(url))
						{
							Log.Write(LogLevel.Debug, "Found image in item image element '{0}'.", url);
							_images.Add(ImageRec.FromUrl(url, pubDate));
						}
					}
				}

				// Scrape the HTML in the description.
				node = item.SelectSingleNode("description");
				if (node != null && node.NodeType == XmlNodeType.Element)
				{
					SearchHtmlForImages(node.InnerText, pubDate);
				}
			}
		}

		private void LoadAtom(XmlDocument xmlDoc)
		{
			foreach (XmlElement entry in xmlDoc.GetElementsByTagName("entry"))
			{
				var pubDate = GetPubDateForItem(entry);

				foreach (XmlNode node in entry.ChildNodes)
				{
					if (node.NodeType != XmlNodeType.Element) continue;
					if (node.Name == "content")
					{
						XmlElement content = (XmlElement)node;
						var type = content.GetAttribute("type");
						if (string.IsNullOrEmpty(type) || type == "html" || type == "xhtml")
						{
							SearchHtmlForImages(content.InnerText, pubDate);
						}
						else if (type.StartsWith("image/"))
						{
							var src = content.GetAttribute("src");
							if (!string.IsNullOrWhiteSpace(src))
							{
								_images.Add(ImageRec.FromUrl(src, pubDate));
							}
						}
					}
				}
			}
		}

		private DateTime GetPubDateForItem(XmlElement item)
		{
			DateTime pubDate;

			var node = item.SelectSingleNode("pubDate");
			if (node != null && node.NodeType == XmlNodeType.Element)
			{
				if (DateTime.TryParse(node.InnerText, out pubDate))
				{
					Log.Write(LogLevel.Debug, "Item has pubDate element: {0}", pubDate.ToString());
					return pubDate;
				}
				else
				{
					Log.Write(LogLevel.Debug, "Couldn't parse pubDate element text: {0}", node.InnerText);
				}
			}
			else
			{
				Log.Write(LogLevel.Debug, "Item has no pubDate element.");
			}

			node = item.SelectSingleNode("updated");
			if (node != null && node.NodeType == XmlNodeType.Element)
			{
				if (DateTime.TryParse(node.InnerText, out pubDate))
				{
					Log.Write(LogLevel.Debug, "Item has updated element: {0}", pubDate.ToString());
					return pubDate;
				}
				else
				{
					Log.Write(LogLevel.Debug, "Couldn't parse updated element text: {0}", node.InnerText);
				}
			}
			else
			{
				Log.Write(LogLevel.Debug, "Item has no updated element.");
			}

			Log.Write(LogLevel.Debug, "Resorting to current date as the pub date.");
			return DateTime.Now;
		}

		private void SearchHtmlForImages(string html, DateTime pubDate)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(html)) return;

				var htmlDoc = new HtmlDocument();
				htmlDoc.LoadHtml(html);
				SearchHtmlNodeForImages(htmlDoc.DocumentNode, pubDate);
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Error when searching html for images:\r\n{0}", html);
			}
		}

		private void SearchHtmlNodeForImages(HtmlNode parentNode, DateTime pubDate)
		{
			foreach (var node in (from n in parentNode.ChildNodes
								  where n.NodeType == HtmlNodeType.Element
								  select n))
			{
				if (node.Name == "img")
				{
					var src = node.GetAttributeValue("src", "");
					if (!string.IsNullOrWhiteSpace(src))
					{
						Log.Write(LogLevel.Debug, "Found image in item HTML '{0}'.", src);
						_images.Add(ImageRec.FromUrl(src, pubDate));
					}
				}
				else if (node.HasChildNodes)
				{
					SearchHtmlNodeForImages(node, pubDate);
				}
			}
		}

		public IEnumerable<ImageRec> Images
		{
			get { return _images; }
		}

	}
}
