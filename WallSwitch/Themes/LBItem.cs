using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallSwitch.Themes
{
	class LBItem
	{
		private LocationBrowser _lb;
		private ImageRec _img;
		private string _relativeLocation;
		private Rectangle[] _starRects = new Rectangle[5];
		private int _mouseOverRating;
		private int _index;

		public LBItem(LocationBrowser lb, ImageRec img, Location loc, int index)
		{
			if (lb == null) throw new ArgumentNullException(nameof(lb));
			if (img == null) throw new ArgumentNullException(nameof(img));
			if (loc == null) throw new ArgumentNullException(nameof(loc));
			if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

			_lb = lb;
			_img = img;
			_index = index;

			_relativeLocation = img.Location;
			if (_relativeLocation.StartsWith(loc.Path, StringComparison.OrdinalIgnoreCase))
			{
				var remove = loc.Path.Length;
				if (remove < _relativeLocation.Length && _relativeLocation[remove] == '\\') remove++;
				_relativeLocation = _relativeLocation.Substring(remove);
			}

			_img.RatingUpdated += Image_RatingUpdated;
		}

		public void OnBrowserClosed()
		{
			_img.RatingUpdated -= Image_RatingUpdated;
			_img = null;
		}

		public ImageRec ImageRec
		{
			get { return _img; }
		}

		public int Index
		{
			get { return _index; }
			set { _index = value; }
		}

		public string RelativeLocation
		{
			get { return _relativeLocation; }
		}

		public Rectangle[] StarRects
		{
			get { return _starRects; }
		}

		public int MouseOverRating
		{
			get { return _mouseOverRating; }
			set
			{
				if (value < 0 || value > 5) throw new ArgumentOutOfRangeException();

				if (_mouseOverRating != value)
				{
					_mouseOverRating = value;
					_lb.InvalidateItem(_index);
				}
			}
		}

		private void Image_RatingUpdated(object sender, EventArgs e)
		{
			try
			{
				if (_index >= 0)
				{
					_lb.InvalidateItem(_index);
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex);
			}
		}
	}
}
