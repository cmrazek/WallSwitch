using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WidgetInterface
{
	public struct Screen
	{
		private Rectangle _bounds;
		private Rectangle _workingArea;
		private bool _primary;

		public Screen(Rectangle bounds, Rectangle workingArea, bool primary)
		{
			_bounds = bounds;
			_workingArea = workingArea;
			_primary = primary;
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
