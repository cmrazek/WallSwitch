using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using System.Xml;

namespace WallSwitch
{
	static class ImageCache
	{
		// TODO: remove
		//private class CacheEntry
		//{
		//	public string location;
		//	public string cacheFileName;
		//	public DateTime? pubDate = null;
		//}
		//private static List<CacheEntry> _cache = new List<CacheEntry>();

		public static bool TryGetCachedImage(string location, out string cacheFileName)
		{
			var ret = Database.SelectString("select cache_file_name from img_cache where location = @loc", "@loc", location);
			if (!string.IsNullOrEmpty(ret))
			{
				cacheFileName = Path.Combine(GetCacheDir(false), ret);
				return File.Exists(ret);
			}

			cacheFileName = string.Empty;
			return false;

			// TODO: remove
			//var entry = (from c in _cache
			//			 where c.location.Equals(location, StringComparison.OrdinalIgnoreCase)
			//			 select c).FirstOrDefault();
			//if (entry == null)
			//{
			//	cacheFileName = string.Empty;
			//	return false;
			//}

			//cacheFileName = Path.Combine(GetCacheDir(false), entry.cacheFileName);
			//return File.Exists(cacheFileName);
		}

		public static void SaveImage(ImageRec img)
		{
			try
			{
				var image = img.Image;
				if (image == null) return;

				var fmt = img.ImageFormat ?? ImageFormat.Png;
				var fileName = string.Concat(Guid.NewGuid().ToString(), ImageFormatDesc.ImageFormatToExtension(fmt));

				image.Save(Path.Combine(GetCacheDir(true), fileName), fmt);

				if (Database.SelectInt("select count(*) from img_cache where location = @loc", "@loc", img.Location) == 0)
				{
					Database.Insert("img_cache", new object[]
						{
							"location", img.Location,
							"cache_file_name", fileName,
							"pub_date", img.PubDate
						});
				}

				// TODO: remove
				//var entry = (from c in _cache
				//			 where c.location.Equals(img.Location, StringComparison.OrdinalIgnoreCase)
				//			 select c).FirstOrDefault();
				//if (entry != null)
				//{
				//	entry.cacheFileName = fileName;
				//	entry.pubDate = img.PubDate;
				//}
				//else
				//{
				//	_cache.Add(new CacheEntry
				//	{
				//		location = img.Location,
				//		cacheFileName = fileName,
				//		pubDate = img.PubDate
				//	});
				//}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Error when saving cached image.");
			}
		}

		private static string GetCacheDir(bool createIfMissing)
		{
			var dir = Path.Combine(Util.AppDataDir, Res.ImageCacheDir);

			if (createIfMissing && !Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}

			return dir;
		}

		// TODO: remove
		//public static void SaveSettings()
		//{
		//	if (_cache.Count == 0) return;

		//	xml.WriteStartElement("ImageCache");

		//	foreach (var entry in _cache)
		//	{
		//		xml.WriteStartElement("CacheEntry");
		//		xml.WriteAttributeString("File", entry.location);
		//		xml.WriteAttributeString("Cache", entry.cacheFileName);
		//		if (entry.pubDate.HasValue) xml.WriteAttributeString("PubDate", entry.pubDate.Value.ToString("s"));
		//		xml.WriteEndElement();	// CacheEntry
		//	}

		//	xml.WriteEndElement();	// ImageCache
		//}

		//public static void LoadSettings(XmlElement settingsElement)
		//{
		//	var imageCache = settingsElement.SelectSingleNode("ImageCache");
		//	if (imageCache == null) return;

		//	foreach (XmlElement cacheEntry in imageCache.SelectNodes("CacheEntry"))
		//	{
		//		var location = cacheEntry.GetAttribute("File");
		//		var cacheFileName = cacheEntry.GetAttribute("Cache");

		//		DateTime? pubDate = null;
		//		DateTime dt;
		//		var str = cacheEntry.GetAttribute("PubDate");
		//		if (!string.IsNullOrWhiteSpace(str) && DateTime.TryParse(str, out dt)) pubDate = dt;

		//		if (!string.IsNullOrWhiteSpace(location) && !string.IsNullOrWhiteSpace(cacheFileName))
		//		{
		//			_cache.Add(new CacheEntry
		//			{
		//				location = location,
		//				cacheFileName = cacheFileName,
		//				pubDate = pubDate
		//			});
		//		}
		//	}
		//}

		public static void ClearExpiredCache(IEnumerable<string> keepLocations)
		{
			var removeList = new List<long>();

			using (var cmd = Database.CreateCommand("select rowid, location from img_cache"))
			{
				using (var rdr = cmd.ExecuteReader())
				{
					while (rdr.Read())
					{
						var location = (string)rdr[1];
						if (!keepLocations.Any(l => l.Equals(location, StringComparison.OrdinalIgnoreCase)))
						{
							removeList.Add((long)rdr[0]);
						}
					}
				}
			}

			if (removeList.Any())
			{
				using (var cmd = Database.CreateCommand("delete from img_cache where rowid = @rowid"))
				{
					foreach (var rowid in removeList)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.AddWithValue("@rowid", rowid);
						cmd.ExecuteNonQuery();
					}
				}
			}

			//// Clean out old entries in internal cache.
			//var removeList = new List<CacheEntry>();
			//foreach (var entry in _cache)
			//{
			//	if (!(from l in keepLocations
			//		  where l.Equals(entry.location, StringComparison.OrdinalIgnoreCase)
			//		  select l).Any())
			//	{
			//		removeList.Add(entry);
			//	}
			//}

			//foreach (var entry in removeList)
			//{
			//	Log.Write(LogLevel.Debug, "Cleaning internal cache item: {0} - {1}", entry.location, entry.cacheFileName);
			//	_cache.Remove(entry);
			//}

			// Clean out old files in cache dir.
			var cacheDir = GetCacheDir(false);
			if (Directory.Exists(cacheDir))
			{
				foreach (var file in Directory.GetFiles(cacheDir))
				{
					var fileName = Path.GetFileName(file);

					if (Database.SelectInt("select count(*) from img_cache where location = @loc", "@loc", "fileName") == 0)
					{
						try
						{
							Log.Write(LogLevel.Debug, "Cleaning disk cache file '{0}'", fileName);
							File.Delete(file);
						}
						catch (Exception ex)
						{
							Log.Write(ex, "Failed to delete old file from cache dir '{0}'.", fileName);
						}
					}

					// TODO: remove
					//if (!(from c in _cache
					//	  where c.cacheFileName.Equals(fileName, StringComparison.OrdinalIgnoreCase)
					//	  select c).Any())
					//{
					//	try
					//	{
					//		Log.Write(LogLevel.Debug, "Cleaning disk cache file '{0}'", fileName);
					//		File.Delete(file);
					//	}
					//	catch (Exception ex)
					//	{
					//		Log.Write(ex, "Failed to delete old file from cache dir '{0}'.", fileName);
					//	}
					//}
				}
			}
		}

	}
}
