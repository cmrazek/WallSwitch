using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WallSwitch
{
	static class DataRowEx
	{
		public static string GetString(this DataRow row, string name, string defaultValue = null)
		{
			var obj = row[name];
			if (obj == null) return defaultValue;
			if (Convert.IsDBNull(obj)) return defaultValue;
			return Convert.ToString(obj);
		}

		public static bool GetBoolean(this DataRow row, string name, bool defaultValue = false)
		{
			var obj = row[name];
			if (obj == null) return defaultValue;
			if (Convert.IsDBNull(obj)) return defaultValue;
			if (obj.GetType() == typeof(bool)) return (bool)obj;
			return Convert.ToInt32(obj) != 0;
		}

		public static int GetInt(this DataRow row, string name, int defaultValue = 0)
		{
			var obj = row[name];
			if (obj == null) return defaultValue;
			if (Convert.IsDBNull(obj)) return defaultValue;
			return Convert.ToInt32(obj);
		}

		public static long GetLong(this DataRow row, string name, long defaultValue = 0)
		{
			var obj = row[name];
			if (obj == null) return defaultValue;
			if (Convert.IsDBNull(obj)) return defaultValue;
			return Convert.ToInt64(obj);
		}

		public static T GetEnum<T>(this DataRow row, string name, T defaultValue = default(T)) where T : struct, IConvertible
		{
			var obj = row[name];
			if (obj == null) return defaultValue;
			if (Convert.IsDBNull(obj)) return defaultValue;
			if (obj.GetType() == typeof(T)) return (T)obj;

			T val;
			if (Enum.TryParse<T>(Convert.ToString(obj), false, out val)) return val;
			return defaultValue;
		}

		public static DateTime GetDateTime(this DataRow row, string name, DateTime? defaultValue = null)
		{
			var obj = row[name];
			if (obj == null || Convert.IsDBNull(obj))
			{
				if (defaultValue.HasValue) return defaultValue.Value;
				return DateTime.MinValue;
			}
			return Convert.ToDateTime(obj);
		}

		public static byte[] GetBytes(this DataRow row, string name, byte[] defaultValue = null)
		{
			var obj = row[name];
			if (obj == null || Convert.IsDBNull(obj)) return defaultValue;
			return (byte[])obj;
		}
	}
}
