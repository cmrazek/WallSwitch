using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WallSwitch.WidgetInterface
{
	/// <summary>
	/// A collection of screens, sorted by position from left-to-right top-to-bottom.
	/// </summary>
	public class ScreenList : ICollection<Screen>
	{
		private Screen[] _screens;
		private Point _offset;

		/// <summary>
		/// Generates a new list of screens.
		/// </summary>
		public ScreenList()
		{
			var screens = (from s in System.Windows.Forms.Screen.AllScreens select new Screen(s.Bounds, s.WorkingArea, s.Primary)).ToList();
			screens.Sort((a, b) =>
				{
					if (a.Bounds.Left < b.Bounds.Left) return -1;
					if (a.Bounds.Left > b.Bounds.Left) return 1;

					if (a.Bounds.Top < b.Bounds.Top) return -1;
					if (a.Bounds.Top > b.Bounds.Top) return 1;

					return 0;
				});

			var minX = 0;
			var minY = 0;
			foreach (var screen in screens)
			{
				if (screen.Bounds.Left < minX) minX = screen.Bounds.Left;
				if (screen.Bounds.Top < minY) minY = screen.Bounds.Top;
			}

			if (minX < 0 || minY < 0)
			{
				screens = (from s in screens select s.CloneWithOffsetedBounds(-minX, -minY)).ToList();
			}

			_offset = new Point(-minX, -minY);

			_screens = screens.ToArray();
		}

		/// <summary>
		/// Gets the primary screen.
		/// </summary>
		public Screen Primary
		{
			get { return (from s in _screens where s.Primary select s).FirstOrDefault(); }
		}

		/// <summary>
		/// Gets a screen by an index.
		/// </summary>
		/// <param name="index">The zero-based index of the screen</param>
		/// <returns>The screen object</returns>
		public Screen this[int index]
		{
			get { return _screens[index]; }
		}

		/// <summary>
		/// Gets the number of available screens
		/// </summary>
		public int Count
		{
			get { return _screens.Length; }
		}

		/// <summary>
		/// Gets a flag indicating if this collection is read-only.
		/// Spoiler: it is
		/// </summary>
		public bool IsReadOnly
		{
			get { return true; }
		}

		/// <summary>
		/// Not supported
		/// </summary>
		public void Add(Screen item)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Not supported
		/// </summary>
		public void Clear()
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Tests if this collection includes a screen object
		/// </summary>
		public bool Contains(Screen item)
		{
			return _screens.Contains(item);
		}

		/// <summary>
		/// Copies this collection to an array
		/// </summary>
		public void CopyTo(Screen[] array, int arrayIndex)
		{
			_screens.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Not supported
		/// </summary>
		public bool Remove(Screen item)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Gets an enumerator for the screen list
		/// </summary>
		public IEnumerator<Screen> GetEnumerator()
		{
			return (IEnumerator<Screen>)_screens.Select(x => x).GetEnumerator();
		}

		/// <summary>
		/// Gets an enumerator for the screen list
		/// </summary>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _screens.GetEnumerator();
		}

		/// <summary>
		/// Tests if any screen contains a point.
		/// </summary>
		/// <param name="pt">The point to be tested</param>
		/// <returns>True if any screen contains the point; otherwise false.</returns>
		public bool Contains(Point pt)
		{
			foreach (var screen in _screens)
			{
				if (screen.Bounds.Contains(pt)) return true;
			}
			return false;
		}

		/// <summary>
		/// Gets the distance the wallpaper image is shifted away from the origin.
		/// This compensates for the fact that some screens natively have coordinates with negative values, but are normalized in this list.
		/// </summary>
		public Point Offset
		{
			get { return _offset; }
		}
	}
}
