using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using WallSwitch.WidgetInterface;

namespace WallSwitch.Widgets
{
	[DisplayName("Calendar")]
	public class CalendarWidget : IWidget
	{
		private Rectangle _lastBounds;
		private Font _daysFont;
		private Brush _backBrush;
		private Pen _borderPen;
		private Font _headerFont;
		private Brush _currentDayBackBrush;

		public Rectangle GetPreferredBounds(ScreenList screens)
		{
			var rect = screens.Primary.Bounds;
			return new Rectangle(rect.Right - 600, 100, 500, 500);
		}

		public void Draw(WidgetDrawArgs args)
		{
			var g = args.Graphics;
			var bounds = args.Bounds;

			var headerHeight = (int)(bounds.Height / 7.0f);
			var headerRect = new Rectangle(bounds.Left, bounds.Top, bounds.Width, headerHeight);
			var daysRect = new Rectangle(bounds.Left, bounds.Top + headerHeight, bounds.Width, bounds.Height - headerHeight);

			if (bounds != _lastBounds)
			{
				_lastBounds = bounds;

				_backBrush = CreateRadialGradientBrush(bounds, Color.FromArgb(0xff, 0, 0, 0), Color.FromArgb(0x40, 0, 0, 0));
				_currentDayBackBrush = CreateRadialGradientBrush(bounds, Color.FromArgb(0xff, 255, 140, 0), Color.FromArgb(0x40, 255, 140, 0));
				_borderPen = new Pen(Color.Gray);
				_daysFont = new Font(FontFamily.GenericSansSerif, ((float)daysRect.Height / 12.0f), FontStyle.Bold, GraphicsUnit.Pixel);
				_headerFont = new Font(FontFamily.GenericSansSerif, headerRect.Height / 2.0f, FontStyle.Bold, GraphicsUnit.Pixel);
			}

			DrawHeader(g, headerRect);
			DrawCalendarDays(g, daysRect);
		}

		private void DrawHeader(Graphics g, Rectangle headerRect)
		{
			g.FillRectangle(_backBrush, headerRect);

			DrawTextCentered(DateTime.Now.ToString("MMMM yyyy"), headerRect, g, _headerFont, Brushes.White);

			g.DrawRectangle(_borderPen, headerRect);
		}

		private void DrawCalendarDays(Graphics g, Rectangle daysRect)
		{
			var now = DateTime.Now;
			var startDay = new DateTime(now.Year, now.Month, 1, 12, 0, 0);
			var oneDay = TimeSpan.FromTicks(TimeSpan.TicksPerDay);

			while (startDay.DayOfWeek != DayOfWeek.Sunday)
			{
				startDay = startDay.Subtract(oneDay);
			}

			var drawDay = startDay;

			var cols = new int[7 + 1];
			for (int c = 0; c <= 7; c++) cols[c] = (int)((float)c / 7.0f * (float)daysRect.Width) + daysRect.Left;

			var rows = new int[6 + 1];
			for (int r = 0; r <= 6; r++) rows[r] = (int)((float)r / 6.0f * (float)daysRect.Height) + daysRect.Top;

			for (int weekNum = 0; weekNum < 6; weekNum++)
			{
				for (int dayNum = 0; dayNum < 7; dayNum++)
				{
					var rect = new Rectangle(cols[dayNum], rows[weekNum], cols[dayNum + 1] - cols[dayNum], rows[weekNum + 1] - rows[weekNum]);

					var fontBrush = drawDay.Month == now.Month ? Brushes.White : Brushes.Gray;

					if (drawDay.Month == now.Month)
					{
						if (drawDay.Day == now.Day) g.FillRectangle(_currentDayBackBrush, rect);
						else g.FillRectangle(_backBrush, rect);
					}

					DrawTextCentered(drawDay.Day.ToString(), rect, g, _daysFont, fontBrush);

					if (drawDay.Month == now.Month) g.DrawRectangle(_borderPen, rect);

					drawDay = drawDay.Add(oneDay);
				}

				if (drawDay.Month != now.Month) break;
			}
		}

		private void DrawTextCentered(string text, Rectangle rect, Graphics g, Font font, Brush brush)
		{
			var size = g.MeasureString(text, font);
			var pt = new PointF(rect.Left + (rect.Width - size.Width) * .5f, rect.Top + (rect.Height - size.Height) * .5f);
			g.DrawString(text, font, brush, pt);
		}

		public void Load(WidgetConfig settings)
		{
		}

		public void Save(WidgetConfig settings)
		{
		}

		public PathGradientBrush CreateRadialGradientBrush(Rectangle rect, Color centerColor, Color outsideColor)
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

		public bool IsFixedSize
		{
			get { return false; }
		}

		public void OnBoundsChanged(WidgetBoundsChangedArgs args)
		{
			// The size must remain a perfect square.

			if (args.Bounds.Width != args.Bounds.Height)
			{
				if (args.Bounds.Width != args.OldBounds.Width) args.Bounds = new Rectangle(args.Bounds.Location, new Size(args.Bounds.Width, args.Bounds.Width));
				else args.Bounds = new Rectangle(args.Bounds.Location, new Size(args.Bounds.Height, args.Bounds.Height));
			}
		}

		public object Properties
		{
			get { return null; }
		}
	}
}
