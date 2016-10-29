using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

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

		public static RectangleF RestrictSize(this RectangleF rect, SizeF maxSize)
		{
			if (rect.Width > maxSize.Width) rect.Width = maxSize.Width;
			if (rect.Height > maxSize.Height) rect.Height = maxSize.Height;
			return rect;
		}

		public static RectangleF CenterInside(this RectangleF rect, RectangleF container)
		{
			return new RectangleF(container.Left + (container.Width - rect.Width) * .5f,
				container.Top + (container.Height - rect.Height) * .5f,
				rect.Width, rect.Height);
		}

		public static bool CompletelyInside(this RectangleF rect, RectangleF container)
		{
			return rect.Left >= container.Left && rect.Top >= container.Top &&
				rect.Right <= container.Right && rect.Bottom <= container.Bottom;
		}

		public static float IntersectArea(this RectangleF self, RectangleF rect)
		{
			if (!self.IntersectsWith(rect)) return 0.0f;
			var inter = RectangleF.Intersect(self, rect);
			return inter.Width * inter.Height;
		}

		public static int IntersectArea(this Rectangle self, Rectangle rect)
		{
			if (!self.IntersectsWith(rect)) return 0;
			var inter = Rectangle.Intersect(self, rect);
			return inter.Width * inter.Height;
		}

		public static int Area(this Rectangle self)
		{
			return self.Width * self.Height;
		}

		public static Rectangle ToRectangle(this RectangleF self)
		{
			return new Rectangle((int)Math.Round(self.Left), (int)Math.Round(self.Top), (int)Math.Round(self.Width), (int)Math.Round(self.Height));
		}

		public static Point TopLeft(this Rectangle rect)
		{
			return new Point(rect.Left, rect.Top);
		}

		public static Point TopRight(this Rectangle rect)
		{
			return new Point(rect.Right, rect.Top);
		}

		public static Point BottomLeft(this Rectangle rect)
		{
			return new Point(rect.Left, rect.Bottom);
		}

		public static Point BottomRight(this Rectangle rect)
		{
			return new Point(rect.Right, rect.Bottom);
		}

		public static PointF TopLeft(this RectangleF rect)
		{
			return new PointF(rect.Left, rect.Top);
		}

		public static PointF TopRight(this RectangleF rect)
		{
			return new PointF(rect.Right, rect.Top);
		}

		public static PointF BottomLeft(this RectangleF rect)
		{
			return new PointF(rect.Left, rect.Bottom);
		}

		public static PointF BottomRight(this RectangleF rect)
		{
			return new PointF(rect.Right, rect.Bottom);
		}

		public static Rectangle GetEnvelope(this IEnumerable<Rectangle> rects)
		{
			int left = -1, top = -1, right = -1, bottom = -1;

			foreach (var rect in rects)
			{
				if (left == -1)
				{
					left = rect.Left;
					top = rect.Top;
					right = rect.Right;
					bottom = rect.Bottom;
				}
				else
				{
					if (rect.Left < left) left = rect.Left;
					if (rect.Top < top) top = rect.Top;
					if (rect.Right > right) right = rect.Right;
					if (rect.Bottom > bottom) bottom = rect.Bottom;
				}
			}

			if (left == -1)
			{
				return Rectangle.Empty;
			}
			else
			{
				return new Rectangle(left, top, right - left, bottom - top);
			}
		}

		public static Rectangle OffsetInline(this Rectangle rect, int x, int y)
		{
			return new Rectangle(rect.Left + x, rect.Top + y, rect.Width, rect.Height);
		}

		public static Point OffsetInline(this Point pt, int x, int y)
		{
			return new Point(pt.X + x, pt.Y + y);
		}
	}

	static class PointUtil
	{
		public static bool IsCloseTo(this Point pt1, Point pt2, int dist)
		{
			return Math.Abs(pt1.X - pt2.X) <= dist && Math.Abs(pt1.Y - pt2.Y) <= dist;
		}

		public static Point Subtract(this Point pt1, Point pt2)
		{
			return new Point(pt1.X - pt2.X, pt1.Y - pt2.Y);
		}

		public static Point Subtract(this Point pt, int x, int y)
		{
			return new Point(pt.X - x, pt.Y - y);
		}

		public static Point Add(this Point pt1, Point pt2)
		{
			return new Point(pt1.X + pt2.X, pt1.Y + pt2.Y);
		}

		public static Point Add(this Point pt, int x, int y)
		{
			return new Point(pt.X + x, pt.Y + y);
		}
	}

	static class PointFUtil
	{
		public static PointF Add(this PointF pt, float x, float y)
		{
			return new PointF(pt.X + x, pt.Y + y);
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

		public static string IntervalDisplayString(int freq, Period period)
		{
			switch (period)
			{
				case Period.Seconds:
					if (freq == 1) return string.Concat(freq, " ", Res.Second);
					return string.Concat(freq, " ", Res.Seconds);
				case Period.Minutes:
					if (freq == 1) return string.Concat(freq, " ", Res.Minute);
					return string.Concat(freq, " ", Res.Minutes);
				case Period.Hours:
					if (freq == 1) return string.Concat(freq, " ", Res.Hour);
					return string.Concat(freq, " ", Res.Hours);
				case Period.Days:
					if (freq == 1) return string.Concat(freq, " ", Res.Day);
					return string.Concat(freq, " ", Res.Days);
				default:
					return string.Concat(freq, " ", period);
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

		public static void ShowError(this IWin32Window form, string format, params object[] args)
		{
			form.ShowError(string.Format(format, args));
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

		public static void InsertAfter(this Control.ControlCollection controls, Control after, Control newCtrl)
		{
			var newIndex = controls.Count;
			controls.Add(newCtrl);

			if (after != null)
			{
				var index = 0;
				foreach (var ctrl in controls)
				{
					if (ctrl == after) break;
					index++;
				}
				if (index < newIndex) controls.SetChildIndex(newCtrl, index + 1);
			}
		}
	}

	public static class FileUtil
	{
		[DllImport("WallSwitchImgProc.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		private static extern void ShowFileInExplorer(string fileName);

		public static void ExploreFile(string fileName)
		{
			ShowFileInExplorer(fileName);
		}

		public static void ExploreDir(string dirPath)
		{
			Process.Start(dirPath);
		}

		private const int FO_DELETE = 3;
		private const int FOF_ALLOWUNDO = 0x40;
		private const int FOF_NOCONFIRMATION = 0x0010;

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
		public struct SHFILEOPSTRUCT
		{
			public IntPtr hwnd;
			[MarshalAs(UnmanagedType.U4)]
			public int wFunc;
			public string pFrom;
			public string pTo;
			public short fFlags;
			[MarshalAs(UnmanagedType.Bool)]
			public bool fAnyOperationsAborted;
			public IntPtr hNameMappings;
			public string lpszProgressTitle;
		}

		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);

		public static void RecycleFile(string fileName)
		{
			SHFILEOPSTRUCT fileop = new SHFILEOPSTRUCT();
			fileop.wFunc = FO_DELETE;
			fileop.pFrom = fileName + '\0' + '\0';
			fileop.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION;

			SHFileOperation(ref fileop);
		}

		public static long? GetFileSize(string fileName)
		{
			try
			{
				var fileInfo = new FileInfo(fileName);
				return fileInfo.Length;
			}
			catch (Exception ex)
			{
				Log.Error(ex);
				return null;
			}
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
				return ParseColor(xml.GetAttribute(attribute), def);
			}
			catch (Exception)
			{
				return def;
			}
		}

		public static Color ParseColor(string str, Color defaultValue)
		{
			Match match = _colorRx.Match(str);
			if (match.Success)
			{
				return Color.FromArgb(Int32.Parse(match.Groups[1].Value), Int32.Parse(match.Groups[2].Value), Int32.Parse(match.Groups[3].Value));
			}
			return defaultValue;
		}
	}

	public static class ListViewUtil
	{
		public static void DistributeColumns(this ListView listView)
		{
			var clientWidth = listView.ClientSize.Width;
			if (clientWidth <= 0) return;

			var totalWidth = (from c in listView.Columns.Cast<ColumnHeader>() select Math.Abs(c.Width)).Sum();
			if (totalWidth <= 0) totalWidth = 1;    // To avoid divide-by-zero.

			foreach (ColumnHeader col in listView.Columns)
			{
				var width = Math.Abs(col.Width);
				if (width == 0) width = 1;
				var ratio = (float)width / (float)totalWidth;
				col.Width = (int)(clientWidth * ratio);
			}
		}

		public static int GetItemIndex(this ListView lv, ListViewItem lvi)
		{
			var index = 0;
			foreach (var item in lv.Items)
			{
				if (item == lvi) return index;
				index++;
			}
			return -1;
		}

		public static Rectangle? GetItemRect(this ListView lv, ListViewItem lvi)
		{
			var index = lv.GetItemIndex(lvi);
			if (index >= 0) return lv.GetItemRect(index);
			return null;
		}

		public static void InvalidateItem(this ListView lv, ListViewItem lvi)
		{
			var rect = lv.GetItemRect(lvi);
			if (rect.HasValue && rect.Value.IntersectsWith(lv.ClientRectangle)) lv.Invalidate(rect.Value);
		}
	}

	public static class ComboBoxUtil
	{
		public static void InitForEnum<T>(this ComboBox comboBox, T initialValue) where T : struct, IConvertible
		{
			comboBox.Items.Clear();
			TagString selectItem = null;

			foreach (T val in Enum.GetValues(typeof(T)))
			{
				var item = new TagString(EnumUtil.GetEnumDesc<T>(val), val);
				if (val.Equals(initialValue)) selectItem = item;
				comboBox.Items.Add(item);
			}

			if (selectItem != null) comboBox.SelectedItem = selectItem;
		}

		public static T GetEnumValue<T>(this ComboBox comboBox) where T : struct, IConvertible
		{
			var item = comboBox.SelectedItem;
			if (item == null || item.GetType() != typeof(TagString)) return default(T);

			TagString ts = item as TagString;
			if (ts.Tag == null || ts.Tag.GetType() != typeof(T)) return default(T);

			return (T)ts.Tag;
		}

		public static void SetEnumValue<T>(this ComboBox comboBox, T value) where T : struct, IConvertible
		{
			foreach (var item in comboBox.Items)
			{
				if (item != null && item.GetType() == typeof(TagString))
				{
					TagString ts = item as TagString;
					if (ts.Tag != null && ts.Tag.GetType() == typeof(T) && value.Equals(ts.Tag))
					{
						comboBox.SelectedItem = item;
						return;
					}
				}
			}
		}
	}

	public static class EnumUtil
	{
		public static string GetEnumDesc<T>(T value) where T : struct, IConvertible
		{
			DescriptionAttribute descAttrib = (from d in typeof(T).GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false).Cast<DescriptionAttribute>()
											   select d as DescriptionAttribute).FirstOrDefault();

			if (descAttrib != null) return descAttrib.Description;
			return value.ToString();
		}
	}

	public static class RandomUtil
	{
		public static Random FromGuid(Guid guid)
		{
			var bytes = guid.ToByteArray();
			uint seed = BytesToUint(bytes[0], bytes[1], bytes[2], bytes[3]) ^
				BytesToUint(bytes[4], bytes[5], bytes[6], bytes[7]) ^
				BytesToUint(bytes[8], bytes[9], bytes[10], bytes[11]) ^
				BytesToUint(bytes[12], bytes[13], bytes[14], bytes[15]);
			seed &= 0x7fffffff;
			return new Random((int)seed);
		}

		public static Random FromGuidAndTime(Guid guid)
		{
			var bytes = guid.ToByteArray();
			uint seed = BytesToUint(bytes[0], bytes[1], bytes[2], bytes[3]) ^
				BytesToUint(bytes[4], bytes[5], bytes[6], bytes[7]) ^
				BytesToUint(bytes[8], bytes[9], bytes[10], bytes[11]) ^
				BytesToUint(bytes[12], bytes[13], bytes[14], bytes[15]);
			seed ^= (uint)(DateTime.Now.Ticks & 0xffffffff);
			seed &= 0x7fffffff;
			return new Random((int)seed);
		}

		private static uint BytesToUint(byte b0, byte b1, byte b2, byte b3)
		{
			return (uint)b0 | ((uint)b1 << 8) | ((uint)b2 << 16) | ((uint)b3 << 24);
		}
	}

	public static class IntUtil
	{
		public static int Clamp(this int value, int minValue, int maxValue)
		{
			if (value < minValue) return minValue;
			if (value > maxValue) return maxValue;
			return value;
		}
	}

	public static class LongUtil
	{
		public static string ToSizeString(this long value)
		{
			if (value < 1024) return string.Format("{0} B", value);

			double val = value / 1024.0;
			if (val < 1024.0) return string.Format("{0:N1} KB", val);

			val /= 1024;
			if (val < 1024.0) return string.Format("{0:N1} MB", val);

			val /= 1024;
			if (val < 1024.0) return string.Format("{0:N1} GB", val);

			val /= 1024;
			return string.Format("{0:N1} TB", val);
		}
	}

	static class GraphicsUtil
	{
		public static void DrawImageWithColorMatrix(this Graphics g, Image img, RectangleF imgRect, RectangleF srcRect, ColorMatrix colorMatrix)
		{
			if (img == null || colorMatrix == null) return;

			ImageAttributes imgAttribs = new ImageAttributes();
			imgAttribs.SetColorMatrix(colorMatrix);

			g.DrawImage(img,
				new Rectangle((int)Math.Round(imgRect.X), (int)Math.Round(imgRect.Y), (int)Math.Round(imgRect.Width), (int)Math.Round(imgRect.Height)),
				srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height,
				GraphicsUnit.Pixel, imgAttribs);
		}

		public static void DrawImageWithColorMatrix(this Graphics g, Image img, RectangleF imgRect, ColorMatrix colorMatrix)
		{
			if (img == null || colorMatrix == null) return;
			DrawImageWithColorMatrix(g, img, imgRect, new RectangleF(0, 0, img.Width, img.Height), colorMatrix);
		}

		public static PathGradientBrush CreateRadialGradientBrush(Rectangle rect, Color centerColor, Color outsideColor)
		{
			PathGradientBrush pgb;
			using (var path = new GraphicsPath())
			{
				var ellipseBounds = rect;
				ellipseBounds.Inflate((int)Math.Round(ellipseBounds.Width * .414f * .5f), (int)Math.Round(ellipseBounds.Height * .414f * .5f));
				path.AddEllipse(ellipseBounds);

				pgb = new PathGradientBrush(path);
				pgb.CenterPoint = new PointF(rect.Left + rect.Width * .5f, rect.Top + rect.Height * .5f);
				pgb.CenterColor = centerColor;
				pgb.SurroundColors = new[] { outsideColor };
				pgb.FocusScales = new PointF(0, 0);
				pgb.WrapMode = WrapMode.Clamp;
			}

			return pgb;
		}

		public static void DrawImage(this Graphics g, CompressedImage img, Rectangle rect)
		{
			using (var i = img.GetImage())
			{
				g.DrawImage(i, rect);
			}
		}

		public static void DrawImage(this Graphics g, CompressedImage img, RectangleF rect)
		{
			using (var i = img.GetImage())
			{
				g.DrawImage(i, rect);
			}
		}

		public static PointF DpiPoint(this Graphics g)
		{
			return new PointF(g.DpiX, g.DpiY);
		}
	}

	public static class VersionUtil
	{
		public static string ToAppFormat(this Version ver)
		{
			var sb = new StringBuilder();

			sb.Append(ver.Major);
			sb.Append(".");
			sb.Append(ver.Minor);
			if (ver.Build > 0)
			{
				sb.Append(".");
				sb.Append(ver.Build);
			}
			if (ver.Revision > 0)
			{
				sb.Append(".");
				sb.Append(ver.Revision);
			}

			return sb.ToString();
		}
	}

	public static class SizeUtil
	{
		public static float GetAspect(this Size size)
		{
			if (size.Height <= 0 || size.Width <= 0) return 0.0f;
			return (float)size.Width / (float)size.Height;
		}
	}

	public static class SizeFUtil
	{
		public static SizeF Scale(this SizeF size, float scale)
		{
			return new SizeF(size.Width * scale, size.Height * scale);
		}
	}

	public static class ImageUtil
	{
		public static Image LoadFromFile(string fileName)
		{
			using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				return Image.FromStream(stream);
			}
		}
	}
}
