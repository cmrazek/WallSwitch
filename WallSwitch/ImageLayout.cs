using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WallSwitch
{
	public class ImageLayout
	{
		public ImageRec ImageRec { get; set; }
		public int StartMonitor { get; set; }
		public int NumMonitors { get; set; }

		public ImageLayout(ImageRec img, int startMonitor, int numMonitors)
		{
			ImageRec = img;
			StartMonitor = startMonitor;
			NumMonitors = numMonitors;
		}

		public void Save(XmlWriter xml)
		{
			xml.WriteAttributeString("StartMonitor", StartMonitor.ToString());
			xml.WriteAttributeString("NumMonitors", NumMonitors.ToString());
			ImageRec.Save(xml);
		}

		public static ImageLayout FromXml(XmlElement element)
		{
			var imageRec = ImageRec.FromXml(element);
			if (imageRec == null) return null;

			string str;
			int startMonitor = 0;
			int numMonitors = 1;

			if (string.IsNullOrEmpty(str = element.GetAttribute("StartMonitor")) || !int.TryParse(str, out startMonitor))
			{
				startMonitor = 0;
			}

			if (string.IsNullOrEmpty(str = element.GetAttribute("NumMonitors")) || !int.TryParse(str, out numMonitors))
			{
				numMonitors = 1;
			}

			return new ImageLayout(imageRec, startMonitor, numMonitors);
		}
	}
}
