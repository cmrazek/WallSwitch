using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;

namespace WallSwitch
{
	class ImageLayout
	{
		public ImageRec ImageRec { get; set; }
		public int[] Monitors { get; set; }

		public ImageLayout(ImageRec img, int[] monitors)
		{
			ImageRec = img;
			Monitors = monitors;
		}

		public void Save(XmlWriter xml)
		{
			var sb = new StringBuilder();
			foreach (var monitor in Monitors)
			{
				if (sb.Length > 0) sb.Append(",");
				sb.Append(monitor);
			}
			xml.WriteAttributeString("Monitors", sb.ToString());

			ImageRec.Save(xml);
		}

		public string GetMonitorsSaveString()
		{
			var sb = new StringBuilder();
			foreach (var monitor in Monitors)
			{
				if (sb.Length > 0) sb.Append(",");
				sb.Append(monitor);
			}
			return sb.ToString();
		}

		public static ImageLayout FromDataRow(DataRow row)
		{
			var imageRec = ImageRec.FromDataRow(row);
			if (imageRec == null) return null;

			var monitors = new List<int>();
			foreach (var monStr in row.GetString("monitors").Split(','))
			{
				int monitor;
				if (int.TryParse(monStr.Trim(), out monitor))
				{
					monitors.Add(monitor);
				}
			}
			if (monitors.Count == 0) monitors.Add(0);

			return new ImageLayout(imageRec, monitors.ToArray());
		}

		public static ImageLayout FromXml(XmlElement element)
		{
			var imageRec = ImageRec.FromXml(element);
			if (imageRec == null) return null;

			string str;
			List<int> monitors = new List<int>();

			str = element.GetAttribute("Monitors");
			if (!string.IsNullOrEmpty(str))
			{
				foreach (var monStr in str.Split(','))
				{
					int monitor;
					if (int.TryParse(monStr.Trim(), out monitor))
					{
						monitors.Add(monitor);
					}
				}
			}
			if (monitors.Count == 0) monitors.Add(0);

			return new ImageLayout(imageRec, monitors.ToArray());
		}
	}
}
