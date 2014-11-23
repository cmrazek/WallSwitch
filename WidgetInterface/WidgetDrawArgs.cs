using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WallSwitch.WidgetInterface
{
	/// <summary>
	/// Arguments for rendering a widget.
	/// </summary>
	public class WidgetDrawArgs
	{
		/// <summary>
		/// The configuration items for this widget.
		/// </summary>
		public WidgetConfig Config { get; private set; }

		/// <summary>
		/// The widget's bounds.
		/// </summary>
		public Rectangle Bounds { get; private set; }

		/// <summary>
		/// The graphics object for the destination image that the widget may use to draw with.
		/// </summary>
		public Graphics Graphics { get; private set; }

		/// <summary>
		/// The image that the widget is being drawn to.
		/// When drawing the sample image, this value will be null.
		/// </summary>
		public Image Image { get; private set; }

		/// <summary>
		/// True if the rendering to the screen layout in the WallSwitch window.
		/// False if rendering to the wallpaper before it is being displayed.
		/// </summary>
		public bool Sample { get; private set; }

		/// <summary>
		/// Creates a new draw arguments object.
		/// </summary>
		/// <param name="config">The configuration item collection</param>
		/// <param name="bounds">The widget's bounds</param>
		/// <param name="g">The graphics object for the destination image</param>
		/// <param name="image">The destination image</param>
		/// <param name="sample">Flag indicating if drawing the sample image</param>
		public WidgetDrawArgs(WidgetConfig config, Rectangle bounds, Graphics g, Image image, bool sample)
		{
			Config = config;
			Graphics = g;
			Image = image;
			Bounds = bounds;
			Sample = sample;
		}
	}
}
