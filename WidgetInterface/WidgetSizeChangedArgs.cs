using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WidgetInterface
{
	/// <summary>
	/// Used to pass data when the user is changing the size of the widget.
	/// </summary>
	public class WidgetSizeChangedArgs
	{
		/// <summary>
		/// Gets the configuration for this widget.
		/// </summary>
		public WidgetConfig Config { get; private set; }

		/// <summary>
		/// Gets or sets the new bounds of the widget.
		/// The widget may choose to adjust the bounds to meet its needs.
		/// </summary>
		public Rectangle Bounds { get; set; }

		/// <summary>
		/// Gets the previous bounds for the widget.
		/// This can be used for comparison, if needed.
		/// </summary>
		public Rectangle OldBounds { get; private set; }

		/// <summary>
		/// Creates size change args.
		/// </summary>
		/// <param name="config">Widget configuration</param>
		/// <param name="bounds">The updated bounds for the widget.</param>
		/// <param name="oldBounds">The previous bounds for the widget.</param>
		public WidgetSizeChangedArgs(WidgetConfig config, Rectangle bounds, Rectangle oldBounds)
		{
			Config = config;
			Bounds = bounds;
			OldBounds = oldBounds;
		}
	}
}
