using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallSwitch.Widgets
{
	internal static class Util
	{
		public static string ToDataSizeString(this long val)
		{
			if (val < 1024) return val.ToString("F01") + " B";

			double size = val / 1024;
			if (size < 1024) return size.ToString("F01") + " KB";

			size /= 1024;
			if (size < 1024) return size.ToString("F01") + " MB";

			size /= 1024;
			return size.ToString("F01") + " GB";
		}
	}
}
