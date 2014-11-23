using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WidgetInterface
{
	public class ScreenList : ICollection<Screen>
	{
		private Screen[] _screens;

		public ScreenList()
		{
			_screens = (from s in System.Windows.Forms.Screen.AllScreens select new Screen(s.Bounds, s.WorkingArea, s.Primary)).ToArray();
		}

		public Screen Primary
		{
			get { return (from s in _screens where s.Primary select s).FirstOrDefault(); }
		}

		public Screen this[int index]
		{
			get { return _screens[index]; }
		}

		public int Count
		{
			get { return _screens.Length; }
		}

		public bool IsReadOnly
		{
			get { return true; }
		}

		public void Add(Screen item)
		{
			throw new NotSupportedException();
		}

		public void Clear()
		{
			throw new NotSupportedException();
		}

		public bool Contains(Screen item)
		{
			return _screens.Contains(item);
		}

		public void CopyTo(Screen[] array, int arrayIndex)
		{
			_screens.CopyTo(array, arrayIndex);
		}

		public bool Remove(Screen item)
		{
			throw new NotSupportedException();
		}

		public IEnumerator<Screen> GetEnumerator()
		{
			return (IEnumerator<Screen>)_screens.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _screens.GetEnumerator();
		}

		public bool Contains(Point pt)
		{
			foreach (var screen in _screens)
			{
				if (screen.Bounds.Contains(pt)) return true;
			}
			return false;
		}
	}
}
