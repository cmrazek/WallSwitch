﻿using System;
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
		private class CacheEntry
		{
			public string location;
			public string cacheFileName;
		}
		private static List<CacheEntry> _cache = new List<CacheEntry>();

		public static bool TryGetCachedImage(string location, out string cacheFileName)
		{
			var entry = (from c in _cache
						 where c.location.Equals(location, StringComparison.OrdinalIgnoreCase)
						 select c).FirstOrDefault();
			if (entry == null)
			{
				cacheFileName = string.Empty;
				return false;
			}

			cacheFileName = Path.Combine(GetCacheDir(false), entry.cacheFileName);
			return File.Exists(cacheFileName);
		}

		public static void SaveImage(ImageRec loc)
		{
			try
			{
				var image = loc.Image;
				if (image == null) return;

				var fmt = loc.ImageFormat ?? ImageFormat.Png;
				var fileName = string.Concat(Guid.NewGuid().ToString(), ImageFormatDesc.ImageFormatToExtension(fmt));

				image.Save(Path.Combine(GetCacheDir(true), fileName), fmt);

				var entry = (from c in _cache
							 where c.location.Equals(loc.Location, StringComparison.OrdinalIgnoreCase)
							 select c).FirstOrDefault();
				if (entry != null)
				{
					entry.cacheFileName = fileName;
				}
				else
				{
					_cache.Add(new CacheEntry
					{
						location = loc.Location,
						cacheFileName = fileName
					});
				}
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

		public static void SaveSettings(XmlWriter xml)
		{
			if (_cache.Count == 0) return;

			xml.WriteStartElement("ImageCache");

			foreach (var entry in _cache)
			{
				xml.WriteStartElement("CacheEntry");
				xml.WriteAttributeString("File", entry.location);
				xml.WriteAttributeString("Cache", entry.cacheFileName);
				xml.WriteEndElement();	// CacheEntry
			}

			xml.WriteEndElement();	// ImageCache
		}

		public static void LoadSettings(XmlElement settingsElement)
		{
			var imageCache = settingsElement.SelectSingleNode("ImageCache");
			if (imageCache == null) return;

			foreach (XmlElement cacheEntry in imageCache.SelectNodes("CacheEntry"))
			{
				var location = cacheEntry.GetAttribute("File");
				var cacheFileName = cacheEntry.GetAttribute("Cache");

				if (!string.IsNullOrWhiteSpace(location) && !string.IsNullOrWhiteSpace(cacheFileName))
				{
					_cache.Add(new CacheEntry
					{
						location = location,
						cacheFileName = cacheFileName
					});
				}
			}
		}

		public static void ClearExpiredCache(IEnumerable<string> keepLocations)
		{
			// Clean out old entries in internal cache.
			var removeList = new List<CacheEntry>();
			foreach (var entry in _cache)
			{
				if (!(from l in keepLocations
					  where l.Equals(entry.location, StringComparison.OrdinalIgnoreCase)
					  select l).Any())
				{
					removeList.Add(entry);
				}
			}

			foreach (var entry in removeList)
			{
				Log.Write(LogLevel.Debug, "Cleaning internal cache item: {0} - {1}", entry.location, entry.cacheFileName);
				_cache.Remove(entry);
			}

			// Clean out old files in cache dir.
			var cacheDir = GetCacheDir(false);
			if (Directory.Exists(cacheDir))
			{
				foreach (var file in Directory.GetFiles(cacheDir))
				{
					var fileName = Path.GetFileName(file);

					if (!(from c in _cache
						  where c.cacheFileName.Equals(fileName, StringComparison.OrdinalIgnoreCase)
						  select c).Any())
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
				}
			}
		}

	}
}
