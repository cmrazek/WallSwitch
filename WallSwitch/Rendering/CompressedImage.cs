using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace WallSwitch
{
	class CompressedImage
	{
		private byte[] _data;
		private Size? _size;

		public CompressedImage(Image img)
		{
			using (var memStream = new MemoryStream())
			{
				img.Save(memStream, ImageFormat.Jpeg);
				var length = (int)memStream.Length;
				_data = new byte[length];
				memStream.Seek(0, SeekOrigin.Begin);
				memStream.Read(_data, 0, length);
			}

			_size = img.Size;
		}

		public CompressedImage(byte[] bytes)
		{
			_data = bytes;
		}

		public Image GetImage()
		{
			using (var memStream = new MemoryStream(_data))
			{
				return Image.FromStream(memStream);
			}
		}

		public byte[] Data
		{
			get { return _data; }
		}

		public Size Size
		{
			get
			{
				if (!_size.HasValue)
				{
					using (var img = GetImage())
					{
						_size = img.Size;
					}
				}
				return _size.Value;
			}
		}
	}
}
