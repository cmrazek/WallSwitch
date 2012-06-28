using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.IO;

namespace WallSwitch
{
	class ImageFormatDesc
	{
		public string Description { get; set; }
		public ImageFormat Format { get; set; }
		public IEnumerable<string> Extensions { get; set; }
		public IEnumerable<string> ContentTypes { get; set; }

		public static List<ImageFormatDesc> SupportedFormats = InitImageFormats();

		private static List<ImageFormatDesc> InitImageFormats()
		{
			List<ImageFormatDesc> formats = new List<ImageFormatDesc>();

			formats.Add(new ImageFormatDesc
			{
				Format = ImageFormat.Jpeg,
				Extensions = new string[] { ".jpg", ".jpeg" },
				ContentTypes = new string[] { "image/jpeg", "image/pjpeg" }
			});

			formats.Add(new ImageFormatDesc
			{
				Format = ImageFormat.Gif,
				Extensions = new string[] { ".gif" },
				ContentTypes = new string[] { "image/gif" }
			});

			formats.Add(new ImageFormatDesc
			{
				Format = ImageFormat.Png,
				Extensions = new string[] { ".png" },
				ContentTypes = new string[] { "image/png" }
			});

			formats.Add(new ImageFormatDesc
			{
				Format = ImageFormat.Bmp,
				Extensions = new string[] { ".bmp" },
				ContentTypes = new string[0]
			});

			formats.Add(new ImageFormatDesc
			{
				Format = ImageFormat.Tiff,
				Extensions = new string[] { ".tiff" },
				ContentTypes = new string[] { "image/tiff" }
			});

			formats.Add(new ImageFormatDesc
			{
				Format = ImageFormat.Icon,
				Extensions = new string[] { ".ico" },
				ContentTypes = new string[] { "image/vnd.microsoft.icon" }
			});

			formats.Add(new ImageFormatDesc
			{
				Format = ImageFormat.Emf,
				Extensions = new string[] { ".emf" },
				ContentTypes = new string[0]
			});

			formats.Add(new ImageFormatDesc
			{
				Format = ImageFormat.Exif,
				Extensions = new string[] { ".exif" },
				ContentTypes = new string[0]
			});

			formats.Add(new ImageFormatDesc
			{
				Format = ImageFormat.Wmf,
				Extensions = new string[] { ".wmf" },
				ContentTypes = new string[0]
			});

			return formats;
		}

		public static ImageFormat FileNameToImageFormat(string fileName)
		{
			var ext = Path.GetExtension(fileName).ToLower();

			return (from f in SupportedFormats
					where f.Extensions.Any((e) => e.Equals(ext, StringComparison.OrdinalIgnoreCase))
					select f.Format).FirstOrDefault();
		}

		public static string ImageFormatToExtension(ImageFormat fmt)
		{
			var ext = (from f in SupportedFormats
					   where f.Format == fmt
					   select f.Extensions.First()).FirstOrDefault();

			if (ext == null) ext = string.Empty;
			return ext;
		}

		public static ImageFormat ContentTypeToImageFormat(string contentType)
		{
			return (from f in SupportedFormats
					where f.ContentTypes.Any((c) => c.Equals(contentType, StringComparison.OrdinalIgnoreCase))
					select f.Format).FirstOrDefault();
		}

		public static string ImageFileFilter
		{
			get
			{
				// Create list of supported extensions for the filter.
				var sb = new StringBuilder();
				foreach (var fmt in ImageFormatDesc.SupportedFormats)
				{
					foreach (var ext in fmt.Extensions)
					{
						if (sb.Length > 0) sb.Append(";");
						sb.Append("*");
						sb.Append(ext);
					}
				}
				return string.Format(Res.ImageFileFilter, sb.ToString());
			}
		}
	}
}
