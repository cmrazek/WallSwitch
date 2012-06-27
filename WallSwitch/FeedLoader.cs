using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using System.Xml;

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

				var req = (HttpWebRequest)HttpWebRequest.Create(url);
				var rsp = (HttpWebResponse)req.GetResponse();
				var stream = rsp.GetResponseStream();

				var xmlDoc = new XmlDocument();
				xmlDoc.Load(rsp.GetResponseStream());

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
							_images.Add(ImageRec.FromUrl(url));
						}
					}
				}

				// Scrape the HTML in the description.
				node = item.SelectSingleNode("description");
				if (node != null && node.NodeType == XmlNodeType.Element)
				{
					SearchHtmlForImages(node.InnerText);
				}
			}
		}

		private void LoadAtom(XmlDocument xmlDoc)
		{
			foreach (XmlElement entry in xmlDoc.GetElementsByTagName("entry"))
			{
				foreach (XmlNode node in entry.ChildNodes)
				{
					if (node.NodeType != XmlNodeType.Element) continue;
					if (node.Name == "content")
					{
						XmlElement content = (XmlElement)node;
						var type = content.GetAttribute("type");
						if (string.IsNullOrEmpty(type) || type == "html" || type == "xhtml")
						{
							SearchHtmlForImages(content.InnerText);
						}
						else if (type.StartsWith("image/"))
						{
							var src = content.GetAttribute("src");
							if (!string.IsNullOrWhiteSpace(src))
							{
								_images.Add(ImageRec.FromUrl(src));
							}
						}
					}
				}
			}
		}

		private void SearchHtmlForImages(string html)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(html)) return;

				var htmlDoc = new HtmlDocument();
				htmlDoc.LoadHtml(html);
				SearchHtmlNodeForImages(htmlDoc.DocumentNode);
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Error when searching html for images:\r\n{0}", html);
			}
		}

		private void SearchHtmlNodeForImages(HtmlNode parentNode)
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
						_images.Add(ImageRec.FromUrl(src));
					}
				}
				else if (node.HasChildNodes)
				{
					SearchHtmlNodeForImages(node);
				}
			}
		}

		public IEnumerable<ImageRec> Images
		{
			get { return _images; }
		}

	}
}
