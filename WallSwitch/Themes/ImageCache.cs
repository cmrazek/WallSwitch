using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;

namespace WallSwitch
{
	static class ImageCache
	{
		public static bool TryGetCachedImage(Database db, string location, out string cacheFileName)
		{
			var ret = db.SelectString("select cache_file_name from img_cache where location = @loc", "@loc", location);
			if (!string.IsNullOrEmpty(ret))
			{
				cacheFileName = Path.Combine(GetCacheDir(false), ret);
				return File.Exists(cacheFileName);
			}

			cacheFileName = string.Empty;
			return false;
		}

		public static string SaveImage(ImageRec img, Database db)
		{
			try
			{
				var image = img.Image;
				if (image == null) return null;

				var fmt = img.ImageFormat ?? ImageFormat.Png;
				var fileName = string.Concat(Guid.NewGuid().ToString(), ImageFormatDesc.ImageFormatToExtension(fmt));
				var cachePathName = Path.Combine(GetCacheDir(true), fileName);
				image.Save(cachePathName, fmt);

				if (db.SelectInt("select count(*) from img_cache where location = @loc", "@loc", img.Location) == 0)
				{
					db.Insert("img_cache", new object[]
						{
							"location", img.Location,
							"cache_file_name", fileName,
							"pub_date", img.PubDate
						});
				}

				return cachePathName;
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Error when saving cached image.");
				return null;
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

		public static void ClearExpiredCache(Database db, IEnumerable<string> keepLocations)
		{
			try
			{
				var cacheDir = GetCacheDir(false);
				if (!Directory.Exists(cacheDir)) return;

				var dbFiles = new List<string>();
				foreach (DataRow row in db.SelectDataTable("select rowid, location, cache_file_name from img_cache").Rows)
				{
					var location = row.GetString("location", string.Empty);
					if (!keepLocations.Any(k => string.Equals(k, location, StringComparison.OrdinalIgnoreCase)))
					{
						db.ExecuteNonQuery("delete from img_cache where rowid = @rowid", "@rowid", row.GetLong("rowid"));
					}
					else
					{
						dbFiles.Add(row.GetString("cache_file_name"));
					}
				}

				// Delete items from disk that don't exist in the database
				var diskFiles = (from f in Directory.GetFiles(cacheDir) select Path.GetFileName(f)).ToArray();
				var diskFilesToRemove = new List<string>();
				foreach (var diskFileName in diskFiles)
				{
					if (!dbFiles.Any(x => string.Equals(x, diskFileName, StringComparison.OrdinalIgnoreCase)))
					{
						diskFilesToRemove.Add(diskFileName);
					}
				}

				foreach (var fileName in diskFilesToRemove)
				{
					try
					{
						File.Delete(Path.Combine(cacheDir, fileName));
					}
					catch (Exception ex)
					{
						Log.Write(ex, "Exception when attempting to delete cached image file: {0}", fileName);
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception when clearing expired image cache items.");
			}
		}

	}
}
