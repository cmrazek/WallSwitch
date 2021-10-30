using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WallSwitch
{
	static class ImageLoading
	{
		private static int ExifOrientationProperty = 0x112;

		public static Image LoadFromFile(string fileName)
		{
			if (Path.GetExtension(fileName).Equals(".webp", StringComparison.OrdinalIgnoreCase))
			{
				return LoadWebpFile(fileName);
			}
			else
			{
				using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					var img = Image.FromStream(stream);

					if (img.PropertyIdList.Contains(ExifOrientationProperty))
					{
						var prop = img.GetPropertyItem(ExifOrientationProperty);
						var value = BitConverter.ToUInt16(prop.Value, 0);
						var rotate = RotateFlipType.RotateNoneFlipNone;

						if (value == 3 || value == 4) rotate = RotateFlipType.Rotate180FlipNone;
						else if (value == 5 || value == 6) rotate = RotateFlipType.Rotate90FlipNone;
						else if (value == 7 || value == 8) rotate = RotateFlipType.Rotate270FlipNone;

						if (value == 2 || value == 4 || value == 5 || value == 7) rotate |= RotateFlipType.RotateNoneFlipX;

						if (rotate != RotateFlipType.RotateNoneFlipNone)
						{
							img.RotateFlip(rotate);
						}
					}

					return img;
				}
			}
		}

		private static Image LoadWebpFile(string pathName)
		{
			var fileBytes = File.ReadAllBytes(pathName);
			var fileLen = fileBytes.Length;
			var filePtr = Marshal.AllocHGlobal(fileLen);
			try
			{
				Marshal.Copy(fileBytes, 0, filePtr, fileBytes.Length);

				var packedSize = WallSwitchImgProc.webpGetSize(filePtr, (uint)fileLen);
				if (packedSize == 0)
				{
					// This might not be a webp file, but just has the wrong extension. Try loading conventionally.
					try
					{
						using (var stream = new MemoryStream(fileBytes))
						{
							return Image.FromStream(stream);
						}
					}
					catch (Exception ex)
					{
						Log.Error(ex, "Webp file could not be loaded as a webp or conventional image: {0}", pathName);
						throw new WebpLoadException($"Invalid webp image: {pathName}");
					}
				}

				var width = (int)(packedSize >> 16);
				var height = (int)(packedSize & 0xffff);
				var bmp = new Bitmap(width, height);

				var bmpData = bmp.LockBits(new Rectangle(0, 0, width, height),
					System.Drawing.Imaging.ImageLockMode.ReadWrite,
					System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				try
				{
					if (WallSwitchImgProc.webpLoadARGB(filePtr, (uint)fileLen, bmpData.Scan0,
						(uint)(bmpData.Stride * bmpData.Height), (uint)bmpData.Stride) != 0)
					{
						throw new WebpLoadException($"Failed to decode webp image: {pathName}");
					}
				}
				finally
				{
					bmp.UnlockBits(bmpData);
				}

				return bmp;
			}
			finally
			{
				Marshal.FreeHGlobal(filePtr);
			}
		}
	}

	class WebpLoadException : Exception
	{
		public WebpLoadException(string message) : base(message) { }
	}
}
