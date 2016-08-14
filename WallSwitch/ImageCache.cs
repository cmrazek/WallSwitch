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
				}
			}
		}

	}
}
