using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;
using System.IO;
using System.Xml;
using System.Drawing.Imaging;

namespace WallSwitch
{
	enum ImageLocationType
	{
		File,
		Url
	}

	class ImageRec
	{
		#region Member Variables
		private string _location;
		private ImageLocationType _type;
		private Image _image = null;
		private ImageFormat _imageFormat = null;
		#endregion

		#region Construction
		private ImageRec()
		{
		}

		public static ImageRec FromFile(string fileName)
		{
			var loc = new ImageRec();
			loc._location = fileName;
			loc._type = ImageLocationType.File;
			return loc;
		}

		public static ImageRec FromDirectory(string pathName)
		{
			var loc = new ImageRec();
			loc._location = pathName;
			loc._type = ImageLocationType.File;
			return loc;
		}

		public static ImageRec FromUrl(string url)
		{
			var loc = new ImageRec();
			loc._location = url;
			loc._type = ImageLocationType.Url;
			return loc;
		}

		public static ImageRec FromXml(XmlElement element)
		{
			ImageLocationType type;
			if (!Enum.TryParse<ImageLocationType>(element.GetAttribute("Type"), out type)) return null;

			string path = element.InnerText;
			if (string.IsNullOrWhiteSpace(path)) return null;

			var loc = new ImageRec();
			loc._location = path;
			loc._type = type;
			return loc;
		}

		public void Save(XmlWriter xml)
		{
			xml.WriteAttributeString("Type", _type.ToString());
			xml.WriteString(_location);
		}

		public override string ToString()
		{
			return _location;
		}

		public string Location
		{
			get { return _location; }
		}

		public ImageLocationType Type
		{
			get { return _type; }
		}

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != typeof(ImageRec)) return false;
			return _location.Equals((obj as ImageRec)._location);
		}

		public override int GetHashCode()
		{
			return _location.GetHashCode();
		}

		public string LocationOnDisk
		{
			get
			{
				switch (_type)
				{
					case ImageLocationType.File:
						return _location;
					case ImageLocationType.Url:
						{
							string fileName;
							if (!ImageCache.TryGetCachedImage(_location, out fileName) || !File.Exists(fileName))
							{
								fileName = string.Empty;
							}
							return fileName;
						}
				}
				return string.Empty;
			}
		}
		#endregion

		#region Loading
		public bool Retrieve()
		{
			if (_image != null) return true;

			switch (_type)
			{
				case ImageLocationType.File:
					try
					{
						_imageFormat = FileNameToImageFormat(_location);
						_image = Image.FromFile(_location);
						return true;
					}
					catch (Exception ex)
					{
						Log.Write(ex, "Failed to load image from file '{0}'.", _location);
						_image = null;
						_imageFormat = null;
						return false;
					}

				case ImageLocationType.Url:
					try
					{
						string fileName;
						if (ImageCache.TryGetCachedImage(_location, out fileName))
						{
							try
							{
								Log.Write(LogLevel.Debug, "Loading cached image from '{0}'.", fileName);
								_image = Image.FromFile(fileName);
								return true;
							}
							catch (Exception ex)
							{
								Log.Write(ex, "Error when loading cached image from '{0}'.", fileName);
							}
						}

						Log.Write(LogLevel.Debug, "Downloading image '{0}'.", _location);
						var request = (HttpWebRequest)HttpWebRequest.Create(_location);
						var response = (HttpWebResponse)request.GetResponse();

						Log.Write(LogLevel.Debug, "Content Type is '{0}'.", response.ContentType);
						_imageFormat = ContentTypeToImageFormat(response.ContentType);

						var stream = response.GetResponseStream();
						_image = Image.FromStream(stream);
						ImageCache.SaveImage(this);

						return true;
					}
					catch (Exception ex)
					{
						Log.Write(ex, "Failed to load image from url '{0}'.", _location);
						_image = null;
						_imageFormat = null;
						return false;
					}
			}

			return false;
		}

		public void Release()
		{
			if (_image != null)
			{
				_image.Dispose();
				_image = null;
			}
		}

		public bool IsPresent
		{
			get { return _image != null; }
		}

		public Image Image
		{
			get { return _image; }
		}
		#endregion

		#region Formats
		public static ImageFormat FileNameToImageFormat(string fileName)
		{
			switch (Path.GetExtension(fileName).ToLower())
			{
				case ".bmp":
					return ImageFormat.Bmp;
				case ".emf":
					return ImageFormat.Emf;
				case ".exif":
					return ImageFormat.Exif;
				case ".gif":
					return ImageFormat.Gif;
				case ".ico":
					return ImageFormat.Icon;
				case ".jpg":
				case ".jpeg":
					return ImageFormat.Jpeg;
				case ".png":
					return ImageFormat.Png;
				case ".tiff":
					return ImageFormat.Tiff;
				case ".wmf":
					return ImageFormat.Wmf;
				default:
					return null;
			}
		}

		public static string ImageFormatToExtension(ImageFormat fmt)
		{
			if (fmt.Guid == ImageFormat.Bmp.Guid) return ".bmp";
			if (fmt.Guid == ImageFormat.Emf.Guid) return ".emf";
			if (fmt.Guid == ImageFormat.Exif.Guid) return ".exif";
			if (fmt.Guid == ImageFormat.Gif.Guid) return ".gif";
			if (fmt.Guid == ImageFormat.Icon.Guid) return ".ico";
			if (fmt.Guid == ImageFormat.Jpeg.Guid) return ".jpg";
			if (fmt.Guid == ImageFormat.Png.Guid) return ".png";
			if (fmt.Guid == ImageFormat.Tiff.Guid) return ".tiff";
			if (fmt.Guid == ImageFormat.Wmf.Guid) return ".wmf";
			return "";
		}

		private ImageFormat ContentTypeToImageFormat(string contentType)
		{
			switch (contentType.Trim().ToLower())
			{
				case "image/gif":
					return ImageFormat.Gif;
				case "image/jpeg":
				case "image/pjpeg":
					return ImageFormat.Jpeg;
				case "image/png":
					return ImageFormat.Png;
				case "image/tiff":
					return ImageFormat.Tiff;
				case "image/vnd.microsoft.icon":
					return ImageFormat.Icon;
				default:
					Log.Write(LogLevel.Warning, "Unable to determine image format from content type '{0}'.", contentType);
					return null;
			}
		}

		public ImageFormat ImageFormat
		{
			get { return _imageFormat; }
		}
		#endregion

	}
}
