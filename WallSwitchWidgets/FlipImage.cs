using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using WallSwitch.WidgetInterface;

namespace WallSwitch.Widgets
{
	[DisplayName("Flip Image")]
	public class FlipImage : IWidget
	{
		private FlipImageProperties _props = new FlipImageProperties();

		public string Name
		{
			get { return "Flip Image"; }
		}

		public void Load(WidgetConfig config)
		{
			bool value;
			WidgetConfigItem item;

			if ((item = config.TryGetItem("Horizontal")) != null)
			{
				if (bool.TryParse(item.Value, out value)) _props.Horizontal = value;
			}

			if ((item = config.TryGetItem("Vertical")) != null)
			{
				if (bool.TryParse(item.Value, out value)) _props.Vertical = value;
			}
		}

		public void Save(WidgetConfig config)
		{
			config["Horizontal"] = _props.Horizontal.ToString();
			config["Vertical"] = _props.Vertical.ToString();
		}

		public Rectangle GetPreferredBounds(ScreenList screens)
		{
			return screens.Last().Bounds;
		}

		public void Draw(WidgetDrawArgs args)
		{
			if (args.Sample) return;
			if (!_props.Horizontal && !_props.Vertical) return;

			using (var img = new Bitmap(args.Bounds.Width, args.Bounds.Height, args.Graphics))
			{
				var imgGraphics = Graphics.FromImage(img);
				imgGraphics.DrawImage(args.Image, new Rectangle(0, 0, args.Bounds.Width, args.Bounds.Height), args.Bounds, GraphicsUnit.Pixel);

				if (_props.Horizontal)
				{
					if (_props.Vertical) img.RotateFlip(RotateFlipType.RotateNoneFlipXY);
					else img.RotateFlip(RotateFlipType.RotateNoneFlipX);
				}
				else if (_props.Vertical)
				{
					img.RotateFlip(RotateFlipType.RotateNoneFlipY);
				}

				args.Graphics.DrawImage(img, args.Bounds);
			}
		}

		public bool IsFixedSize
		{
			get { return true; }
		}

		public void OnBoundsChanged(WidgetBoundsChangedArgs args)
		{
			if (!args.Final) return;

			// Snap the bounds to the size of a full monitor.
			var center = new Point(args.Bounds.Left + args.Bounds.Right / 2, args.Bounds.Top + args.Bounds.Bottom / 2);
			var screen = (from s in args.Screens orderby PointDist(center, RectCenter(s.Bounds)) select s).FirstOrDefault();
			if (!screen.Bounds.IsEmpty)
			{
				args.Bounds = screen.Bounds;
			}
		}

		private Point RectCenter(Rectangle rect)
		{
			return new Point(rect.Left + rect.Right / 2, rect.Top + rect.Bottom / 2);
		}

		private double PointDist(Point a, Point b)
		{
			double x = b.X - a.X;
			double y = b.Y - a.Y;
			return Math.Sqrt(x * x + y * y);
		}

		public object Properties
		{
			get { return _props; }
		}

		public class FlipImageProperties
		{
			public bool Horizontal { get; set; }
			public bool Vertical { get; set; }

			public FlipImageProperties()
			{
				Horizontal = true;
				Vertical = false;
			}
		}
	}
}
