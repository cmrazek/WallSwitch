using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WallSwitch.WidgetInterface
{
	/// <summary>
	/// Contains information for a single screen.
	/// </summary>
	public struct Screen
	{
		private Rectangle _bounds;
		private Rectangle _workingArea;
		private bool _primary;

		/// <summary>
		/// Creates a new Screen object.
		/// </summary>
		/// <param name="bounds">The bounds of the screen</param>
		/// <param name="workingArea">The bounds of the working area</param>
		/// <param name="primary">A flag indicating if this is the primary screen</param>
		public Screen(Rectangle bounds, Rectangle workingArea, bool primary)
		{
			_bounds = bounds;
			_workingArea = workingArea;
			_primary = primary;
		}

		internal Screen CloneWithOffsetedBounds(int offsetX, int offsetY)
		{
			var bounds = _bounds;
			bounds.Offset(offsetX, offsetY);

			var workingArea = _workingArea;
			workingArea.Offset(offsetX, offsetY);

			return new Screen(bounds, workingArea, _primary);
		}

		/// <summary>
		/// Gets the rectangle that encompasses this screen.
		/// </summary>
		public Rectangle Bounds
		{
			get { return _bounds; }
		}

		/// <summary>
		/// Gets the working area of the screen (excludes taskbars, docked windows and docked toolbars)
		/// </summary>
		public Rectangle WorkingArea
		{
			get { return _workingArea; }
		}

		/// <summary>
		/// Gets a flag indicating if this screen is the primary.
		/// </summary>
		public bool Primary
		{
			get { return _primary; }
		}
	}
}
