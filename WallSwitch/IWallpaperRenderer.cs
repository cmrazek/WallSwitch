using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WallSwitch
{
	public interface IWallpaperRenderer : IDisposable
	{
		Image WallpaperImage { get; }
		bool InitFrame(Rectangle[] screenRects, Theme theme, Bitmap lastImage);
		void RenderImageOnScreen(ImageRec file, Rectangle thisScreenRect, bool firstImage);
		void RenderBlankScreen(Rectangle screenRect);
	}
}
