using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WallSwitch
{
	class ImageListView : ListView
	{
		private class ILVData
		{
			public Bitmap icon;
			public ImageLocationType locationType;
			public string location;
		}

		public ImageListView()
		{
			OwnerDraw = true;
		}

		protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
		{
			e.DrawDefault = true;
			//base.OnDrawColumnHeader(e);
		}

		protected override void OnDrawItem(DrawListViewItemEventArgs e)
		{
			//base.OnDrawItem(e);

			var data = (ILVData)e.Item.Tag;
			if (data.icon != null)
			{
				e.Graphics.DrawImage(data.icon, e.Bounds);

				e.DrawFocusRectangle();
			}
		}

		protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
		{
			base.OnDrawSubItem(e);
		}

		public void AddHistory(ImageRec img)
		{
			Bitmap bmp = null;
			if (img != null) bmp = new Bitmap(img.Image, 32, 32);

			ListViewItem lvi = new ListViewItem("");
			lvi.Tag = new ILVData
			{
				icon = bmp,
				locationType = img.Type,
				location = img.Location
			};
			Items.Insert(0, lvi);
		}

	}
}
