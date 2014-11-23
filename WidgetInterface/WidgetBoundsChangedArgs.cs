using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WallSwitch.WidgetInterface
{
	/// <summary>
	/// Used to pass data when the user is changing the size of the widget.
	/// </summary>
	public class WidgetBoundsChangedArgs
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
		/// A list of available monitors
		/// </summary>
		public ScreenList Screens { get; private set; }

		/// <summary>
		/// Gets a flag indicating if the user has finished dragging the widget, and these bounds are now finalized.
		/// </summary>
		public bool Final { get; private set; }

		/// <summary>
		/// Creates size change args.
		/// </summary>
		/// <param name="config">Widget configuration</param>
		/// <param name="bounds">The updated bounds for the widget.</param>
		/// <param name="oldBounds">The previous bounds for the widget.</param>
		/// <param name="screens">A list of available monitors.</param>
		/// <param name="final">A flag indicating if the user has finished dragging the widget, and these bounds are now finalized.</param>
		public WidgetBoundsChangedArgs(WidgetConfig config, Rectangle bounds, Rectangle oldBounds, ScreenList screens, bool final)
		{
			Config = config;
			Bounds = bounds;
			OldBounds = oldBounds;
			Screens = screens;
			Final = final;
		}
	}
}
