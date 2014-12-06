using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WallSwitch.Data;

namespace WallSwitch
{
	internal static class DataEx
	{
		public static Point ToData(this System.Drawing.Size size)
		{
			return new Point { X = size.Width, Y = size.Height };
		}

		public static Point ToData(this System.Drawing.Point pt)
		{
			return new Point { X = pt.X, Y = pt.Y };
		}

		public static System.Drawing.Point ToPoint(this Point pt)
		{
			return new System.Drawing.Point(pt.X, pt.Y);
		}

		public static Rectangle ToData(this System.Drawing.Rectangle rect)
		{
			return new Rectangle { Left = rect.Left, Top = rect.Top, Width = rect.Width, Height = rect.Height };
		}

		public static System.Drawing.Rectangle ToRectangle(this Rectangle rect)
		{
			return new System.Drawing.Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);
		}

		public static IEnumerable<Screen> ToData(this WidgetInterface.ScreenList screens)
		{
			foreach (var screen in screens)
			{
				yield return new Screen
				{
					Primary = screen.Primary,
					Bounds = screen.Bounds.ToData(),
					WorkingArea = screen.WorkingArea.ToData()
				};
			}
		}
	}
}
