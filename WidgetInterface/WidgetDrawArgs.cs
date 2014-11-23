using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WidgetInterface
{
	public class WidgetDrawArgs
	{
		public WidgetConfig Config { get; private set; }
		public Rectangle Bounds { get; private set; }
		public Graphics Graphics { get; private set; }
		public bool Sample { get; private set; }

		public WidgetDrawArgs(WidgetConfig config, Rectangle bounds, Graphics g, bool sample)
		{
			Config = config;
			Graphics = g;
			Bounds = bounds;
			Sample = sample;
		}
	}
}
