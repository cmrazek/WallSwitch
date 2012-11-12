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
		void RenderScreen(ImageRec file, Rectangle thisScreenRect);
		void RenderBlankScreen(Rectangle screenRect);
	}
}
