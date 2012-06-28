using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;

namespace WallSwitch
{
	public static class Util
	{
		#region String Conversion
		public static string ParseString(XmlElement xml, string attribute, string def)
		{
			try
			{
				if (!xml.HasAttribute(attribute)) return def;
				return xml.GetAttribute(attribute);
			}
			catch (Exception)
			{
				return def;
			}
		}
		#endregion

		#region Boolean Conversion
		public static bool ParseBool(object obj, bool def)
		{
			try
			{
				return Convert.ToBoolean(obj);
			}
			catch (Exception)
			{
				return def;
			}
		}

		public static bool ParseBool(XmlElement xml, string attribute, bool def)
		{
			try
			{
				if (!xml.HasAttribute(attribute)) return def;

				bool val;
				if (!Boolean.TryParse(xml.GetAttribute(attribute), out val)) return def;
				return val;
			}
			catch (Exception)
			{
				return def;
			}
		}
		#endregion

		#region Int Conversion
		public static int ParseInt(XmlElement xml, string attribute, int def)
		{
			try
			{
				if (!xml.HasAttribute(attribute)) return def;

				int val;
				if (!Int32.TryParse(xml.GetAttribute(attribute), out val)) return def;
				return val;
			}
			catch (Exception)
			{
				return def;
			}
		}
		#endregion

		#region Enum Conversion
		public static T ParseEnum<T>(XmlElement xml, string attribute, T def)
		{
			try
			{
				if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type.");
				if (!xml.HasAttribute(attribute)) return def;

				string attrib = xml.GetAttribute(attribute).ToLower();
				foreach (T i in Enum.GetValues(typeof(T)))
				{
					if (attrib == i.ToString().ToLower()) return i;
				}

				return def;
			}
			catch (Exception)
			{
				return def;
			}
		}
		#endregion

		#region App Data Directory
		public static string AppDataDir
		{
			get
			{
				string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Res.SettingsDir);
				if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
				return dir;
			}
		}
		#endregion
	}

	static class RectangleUtil
	{
		public static RectangleF ScaleRectWidth(this RectangleF rect, float width)
		{
			if (rect.Width > 0.0f && rect.Width != width)
			{
				rect.Height = rect.Height * (width / rect.Width);
				rect.Width = width;
			}
			return rect;
		}

		public static RectangleF ScaleRectHeight(this RectangleF rect, float height)
		{
			if (rect.Height > 0.0f && rect.Height != height)
			{
				rect.Width = rect.Width * (height / rect.Height);
				rect.Height = height;
			}
			return rect;
		}

		public static Point Center(this Rectangle rect)
		{
			return new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
		}
	}

	static class TimeSpanUtil
	{
		public static TimeSpan CalcInterval(int freq, Period period)
		{
			switch (period)
			{
				case Period.Seconds:
					return TimeSpan.FromSeconds(freq);
				case Period.Minutes:
					return TimeSpan.FromMinutes(freq);
				case Period.Hours:
					return TimeSpan.FromHours(freq);
				case Period.Days:
					return TimeSpan.FromDays(freq);
				default:
					return TimeSpan.FromMinutes(freq);
			}
		}
	}

	static class FormUtil
	{
		public static void ShowError(this IWin32Window form, string message)
		{
			Log.Write(LogLevel.Error, "Error: {0}", message);
			MessageBox.Show(String.Format(Res.Error_Content, message), Res.Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public static void ShowError(this IWin32Window form, Exception ex, string message)
		{
			Log.Write(ex, "Error: {0}", message);
			MessageBox.Show(String.Format(Res.Error_ContentEx, message, ex.ToString()), Res.Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public static void ShowError(this IWin32Window form, Exception ex)
		{
			Log.Write(ex, "Error:");
			MessageBox.Show(String.Format(Res.Error_ContentEx, ex.Message, ex.ToString()), Res.Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}

	public static class FileUtil
	{
		public static void ExploreFile(string fileName)
		{
			Process.Start("explorer.exe", string.Format("/select,{0}", fileName));
		}

		public static void ExploreDir(string dirPath)
		{
			Process.Start(dirPath);
		}
	}

	public static class ColorUtil
	{
		public static string ColorToString(Color color)
		{
			return String.Format("{0}, {1}, {2}", color.R, color.G, color.B);
		}

		private static Regex _colorRx = new Regex(@"^\s*(\d+),?\s*(\d+),?\s*(\d+),?\s*$");

		public static Color ParseColor(XmlElement xml, string attribute, Color def)
		{
			try
			{
				if (!xml.HasAttribute(attribute)) return def;

				string attrib = xml.GetAttribute(attribute);
				Match match = _colorRx.Match(attrib);
				if (match.Success)
				{
					return Color.FromArgb(Int32.Parse(match.Groups[1].Value), Int32.Parse(match.Groups[2].Value), Int32.Parse(match.Groups[3].Value));
				}
				return def;
			}
			catch (Exception)
			{
				return def;
			}
		}
	}

	public static class ListViewUtil
	{
		public static void DistributeColumns(this ListView listView)
		{
			var clientWidth = listView.ClientSize.Width;
			if (clientWidth <= 0) return;

			var totalWidth = (from c in listView.Columns.Cast<ColumnHeader>() select Math.Abs(c.Width)).Sum();
			if (totalWidth <= 0) totalWidth = 1;	// To avoid divide-by-zero.

			foreach (ColumnHeader col in listView.Columns)
			{
				var width = Math.Abs(col.Width);
				if (width == 0) width = 1;
				var ratio = (float)width / (float)totalWidth;
				col.Width = (int)(clientWidth * ratio);
			}
		}
	}
}
