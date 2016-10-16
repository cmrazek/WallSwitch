using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace WallSwitch
{
	internal enum ImageLocationType
	{
		File,
		Url
	}

	internal class ImageRec : IComparable<ImageRec>
	{
		#region Member Variables
		private string _location;
		private ImageLocationType _type;
		private Image _image = null;
		private ImageFormat _imageFormat = null;
		private CompressedImage _thumbnail = null;
		private DateTime? _pubDate = null;
		private int _hashCode;
		private int _rating;
		#endregion

		#region Events
		public event EventHandler ThumbnailUpdated;
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
			loc._hashCode = loc._location.ToLower().GetHashCode();
			return loc;
		}

		public static ImageRec FromDirectory(string pathName)
		{
			var loc = new ImageRec();
			loc._location = pathName;
			loc._type = ImageLocationType.File;
			loc._hashCode = loc._location.ToLower().GetHashCode();
			return loc;
		}

		public static ImageRec FromUrl(string url, DateTime pubDate)
		{
			var loc = new ImageRec();
			loc._location = url;
			loc._type = ImageLocationType.Url;
			loc._pubDate = pubDate;
			loc._hashCode = loc._location.ToLower().GetHashCode();
			return loc;
		}

		public static ImageRec FromDataRow(DataRow row)
		{
			var type = row.GetEnum<ImageLocationType>("type");
			var path = row.GetString("path");
			var rating = row.GetInt("rating");

			var loc = new ImageRec();
			loc._location = path;
			loc._type = type;
			loc._hashCode = loc._location.ToLower().GetHashCode();
			loc._rating = rating;

			var bytes = row.GetBytes("thumb");
			if (bytes != null)
			{
				loc._thumbnail = new CompressedImage(bytes);
			}

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
			loc._hashCode = loc._location.ToLower().GetHashCode();
			return loc;
		}

		public void Save(XmlWriter xml)
		{
			xml.WriteAttributeString("Type", _type.ToString());
			if (_pubDate.HasValue) xml.WriteAttributeString("PubDate", _pubDate.Value.ToString("s"));
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

		public int Rating
		{
			get { return _rating; }
			set
			{
				if (value < 0 || value > 5) throw new ArgumentOutOfRangeException();
				_rating = value;
			}
		}

		public override bool Equals(object obj)
		{
			var other = obj as ImageRec;
			if (other == null) return false;

			return string.Equals(_location, other._location, StringComparison.OrdinalIgnoreCase);
		}

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public string GetLocationOnDisk(Database db)
		{
			switch (_type)
			{
				case ImageLocationType.File:
					return File.Exists(_location) ? _location : string.Empty;
				case ImageLocationType.Url:
					if (db != null)
					{
						string fileName;
						if (!ImageCache.TryGetCachedImage(db, _location, out fileName) || !File.Exists(fileName))
						{
							fileName = string.Empty;
						}
						return fileName;
					}
					else
					{
						return null;
					}
			}
			return string.Empty;
		}

		public string GetLocationOnDisk()
		{
			var path = GetLocationOnDisk(null);
			if (string.IsNullOrEmpty(path))
			{
				using (var db = new Database())
				{
					path = GetLocationOnDisk(db);
				}
			}
			return path;
		}

		public ImageFormat ImageFormat
		{
			get { return _imageFormat; }
		}

		public DateTime? PubDate
		{
			get { return _pubDate; }
			set { _pubDate = value; }
		}
		#endregion

		#region Loading
		public bool Retrieve(Database db)
		{
			if (_image != null) return true;

			switch (_type)
			{
				case ImageLocationType.File:
					try
					{
						_imageFormat = ImageFormatDesc.FileNameToImageFormat(_location);
						_image = Image.FromFile(_location);
						MakeThumbnail(db);
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
						if (ImageCache.TryGetCachedImage(db, _location, out fileName))
						{
							try
							{
								Log.Write(LogLevel.Debug, "Loading cached image from '{0}'.", fileName);
								_image = Image.FromFile(fileName);
								MakeThumbnail(db);
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
						_imageFormat = ImageFormatDesc.ContentTypeToImageFormat(response.ContentType);

						var stream = response.GetResponseStream();
						_image = Image.FromStream(stream);
						ImageCache.SaveImage(this, db);
						MakeThumbnail(db);
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

		private void MakeThumbnail(Database db)
		{
			try
			{
				if (_thumbnail != null || _image == null) return;
				lock (_image)
				{
					var imageRect = new RectangleF(new PointF(0.0f, 0.0f), _image.Size);
					if (imageRect.Width > HistoryList.ThumbnailWidth) imageRect = imageRect.ScaleRectWidth(HistoryList.ThumbnailWidth);
					if (imageRect.Height > HistoryList.ThumbnailHeight) imageRect = imageRect.ScaleRectHeight(HistoryList.ThumbnailHeight);

					_thumbnail = new CompressedImage(new Bitmap(_image, (int)imageRect.Width, (int)imageRect.Height));
					ThumbnailUpdated?.Invoke(this, EventArgs.Empty);
				}

				using (var cmd = db.CreateCommand("update img set thumb = @thumb where path = @path"))
				{
					cmd.Parameters.AddWithValue("@thumb", _thumbnail.Data);
					cmd.Parameters.AddWithValue("@path", _location);
					cmd.ExecuteNonQuery();
				}

				using (var cmd = db.CreateCommand("update history set thumb = @thumb where path = @path"))
				{
					cmd.Parameters.AddWithValue("@thumb", _thumbnail.Data);
					cmd.Parameters.AddWithValue("@path", _location);
					cmd.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Error when creating image thumbnail.");
				_thumbnail = null;
			}
		}

		public CompressedImage Thumbnail
		{
			get
			{
				return _thumbnail;
			}
		}
		#endregion

		public int CompareTo(ImageRec other)	// For IComparable
		{
			return string.Compare(_location, other._location, StringComparison.OrdinalIgnoreCase);
		}

	}
}
