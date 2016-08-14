using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace WallSwitch
{
	static class SQLiteDataReaderEx
	{
		public static string GetString(this SQLiteDataReader rdr, string fieldName, string defaultValue = null)
		{
			var ret = rdr.GetString(rdr.GetOrdinal(fieldName));
			if (ret == null) return defaultValue;
			if (Convert.IsDBNull(ret)) return defaultValue;
			return ret;
		}

		public static bool GetBoolean(this SQLiteDataReader rdr, string fieldName, bool defaultValue = false)
		{
			var obj = rdr[rdr.GetOrdinal(fieldName)];
			if (obj == null) return defaultValue;
			if (Convert.IsDBNull(obj)) return defaultValue;
			var ret = Convert.ToInt32(obj);
			return ret != 0 ? true : false;
		}

		public static int GetInt32(this SQLiteDataReader rdr, string fieldName, int defaultValue = 0)
		{
			var obj = rdr[rdr.GetOrdinal(fieldName)];
			if (obj == null) return defaultValue;
			if (Convert.IsDBNull(obj)) return defaultValue;
			return Convert.ToInt32(obj);
		}

		public static T GetEnum<T>(this SQLiteDataReader rdr, string fieldName, T defaultValue = default(T)) where T : struct, IConvertible
		{
			var obj = rdr[rdr.GetOrdinal(fieldName)];
			if (obj == null) return defaultValue;
			if (Convert.IsDBNull(obj)) return defaultValue;

			T val;
			if (Enum.TryParse<T>(Convert.ToString(obj), false, out val)) return val;
			return defaultValue;
		}
	}
}
