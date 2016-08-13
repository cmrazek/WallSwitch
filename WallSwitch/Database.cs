using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace WallSwitch
{
	class Database
	{

		private static SQLiteConnection _conn;
		private static bool _isNew;

		private static string[] k_initializationScripts = new string[] {
@"create table setting (
name	varchar(100) not null,
value	text
)",
@"create unique index setting_ix_name on setting (name)",
@"create table theme (
name						varchar(100) not null,
active						tinyint,
freq						int not null,
period						varchar(20) not null,
mode						varchar(20) not null,
order_						varchar(20) not null,
image_size					int not null,
back_color_top				varchar(20),
back_color_bottom			varchar(20),
separate_monitors			tinyint,
allow_spanning				tinyint,
max_image_clip				int,
back_opacity				int,
image_fit					varchar(20),
edge_mode					varchar(20),
edge_dist					int,
border_color				varchar(20),
fade_transition				tinyint,
last_wallpaper_file			varchar(300),
max_image_scale				int,
num_collage_images			int,
color_effect_fore			varchar(20),
color_effect_back			varchar(20),
color_effect_back_ratio		int,
drop_shadow					tinyint,
drop_shadow_dist			int,
drop_shadow_feather			tinyint,
drop_shadow_feather_dist	int,
drop_shadow_opacity			int,
background_blur				tinyint,
background_blur_dist		int,
last_image					varchar(300),
activate_on_exit			tinyint,
random_group_count			int,
clear_between_random_groups	tinyint,
hot_key						varchar(100),
history_guid				varchar(40),
latest_guid					varchar(40)
)",
@"create index theme_ix_name on theme (name)",

@"create table location (
theme_id		integer not null,
path			varchar(300) not null,
type			varchar(20) not null,
update_freq		int not null,
update_period	varchar(20) not null,
disabled		tinyint not null,
last_update		datetime
)",
@"create index location_ix_theme on location (theme_id)",

@"create table img (
theme_id		integer not null,
location_id		integer not null,
type			varchar(20) not null,
path			varchar(300) not null,
pub_date		datetime
)",
@"create index img_ix_theme on img (theme_id, path)",
@"create index img_ix_location on img (location_id)",

@"create table widget (
theme_id		integer not null,
name			varchar(200) not null,
bounds_left		int,
bounds_top		int,
bounds_width	int,
bounds_height	int
)",
@"create index widget_ix_theme on widget (theme_id)",

@"create table widget_config (
widget_id		integer not null,
name			varchar(100) not null,
value			text
)",
@"create index widgetconfig_ix_widget on widget_config (widget_id)",

@"create table history (
theme_id		integer not null,
display_date	datetime not null,
guid			varchar(40) not null,
monitors		varchar(20) not null,
type			varchar(20) not null,
path			varchar(300) not null,
pub_date		datetime
)",
@"create index history_ix_theme on history (theme_id)",

@"create table rhistory (
theme_id		integer not null,
display_date	datetime not null,
left			int not null,
top				int not null,
width			int not null,
height			int not null
)",
@"create index rhistory_ix_theme on rhistory (theme_id)",

@"create table img_cache (
location			varchar(300) not null,
cache_file_name		varchar(300) not null,
pub_date			datetime
)",
@"create index imgcache_ix_location on img_cache (location)"
	};

		public static void Initialize()
		{
			try
			{
				var fileName = FileName;
				if (!File.Exists(fileName))
				{
					SQLiteConnection.CreateFile(fileName);
					_isNew = true;
				}

				_conn = new SQLiteConnection(string.Format("Data Source={0};Version=3;", fileName));
				_conn.Open();

				if (_isNew)
				{
					RunScripts(k_initializationScripts);
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex);
				Shutdown();
			}
		}

		public static void Shutdown()
		{
			try
			{
				if (_conn != null)
				{
					_conn.Close();
					_conn = null;
				}
			}
			catch (Exception ex)
			{
				_conn = null;
				Log.Write(ex);
			}
		}

		private static void RunScripts(string[] scripts)
		{
			foreach (var script in scripts)
			{
				Log.Info("SQL> {0}", script);

				try
				{
					using (var cmd = _conn.CreateCommand())
					{
						cmd.CommandText = script;
						cmd.CommandType = System.Data.CommandType.Text;
						cmd.ExecuteNonQuery();
					}
				}
				catch (Exception ex)
				{
					Log.Write(ex);
				}
			}
		}

		public static string FileName
		{
			get
			{
				return Path.Combine(Util.AppDataDir, Res.DatabaseFileName);
			}
		}

		public static bool IsNew
		{
			get { return _isNew; }
		}

		public static SQLiteCommand CreateCommand(string sql)
		{
			if (_conn == null) throw new InvalidOperationException("No connection to SQL database.");

			Log.Debug("SQL> {0}", sql);

			var cmd = _conn.CreateCommand();
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = sql;
			return cmd;
		}

		public static void ExecuteNonQuery(string sql, params object[] parameters)
		{
			if (parameters.Length % 2 != 0) throw new ArgumentException("The number of parameters must be of an even length.");

			using (var cmd = CreateCommand(sql))
			{
				for (int i = 0, ii = parameters.Length; i < ii; i += 2)
				{
					cmd.Parameters.AddWithValue((string)parameters[i], parameters[i + 1]);
				}

				cmd.ExecuteNonQuery();
			}
		}

		public static List<int> SelectIntList(string sql, params object[] parameters)
		{
			if (parameters.Length % 2 != 0) throw new ArgumentException("The number of parameters must be of an even length.");

			using (var cmd = CreateCommand(sql))
			{
				for (int i = 0, ii = parameters.Length; i < ii; i += 2)
				{
					cmd.Parameters.AddWithValue((string)parameters[i], parameters[i + 1]);
				}

				var list = new List<int>();

				using (var rdr = cmd.ExecuteReader())
				{
					while (rdr.Read())
					{
						list.Add(Convert.ToInt32(rdr[0]));
					}
				}

				return list;
			}
		}

		public static List<long> SelectLongList(string sql, params object[] parameters)
		{
			if (parameters.Length % 2 != 0) throw new ArgumentException("The number of parameters must be of an even length.");

			using (var cmd = CreateCommand(sql))
			{
				for (int i = 0, ii = parameters.Length; i < ii; i += 2)
				{
					cmd.Parameters.AddWithValue((string)parameters[i], parameters[i + 1]);
				}

				var list = new List<long>();

				using (var rdr = cmd.ExecuteReader())
				{
					while (rdr.Read())
					{
						list.Add(Convert.ToInt64(rdr[0]));
					}
				}

				return list;
			}
		}

		public static List<string> SelectStringList(string sql, params object[] parameters)
		{
			if (parameters.Length % 2 != 0) throw new ArgumentException("The number of parameters must be of an even length.");

			using (var cmd = CreateCommand(sql))
			{
				for (int i = 0, ii = parameters.Length; i < ii; i += 2)
				{
					cmd.Parameters.AddWithValue((string)parameters[i], parameters[i + 1]);
				}

				var list = new List<string>();

				using (var rdr = cmd.ExecuteReader())
				{
					while (rdr.Read())
					{
						list.Add(Convert.ToString(rdr[0]));
					}
				}

				return list;
			}
		}

		public static int SelectInt(string sql, params object[] parameters)
		{
			if (parameters.Length % 2 != 0) throw new ArgumentException("The number of parameters must be of an even length.");

			using (var cmd = CreateCommand(sql))
			{
				for (int i = 0, ii = parameters.Length; i < ii; i += 2)
				{
					cmd.Parameters.AddWithValue((string)parameters[i], parameters[i + 1]);
				}

				return Convert.ToInt32(cmd.ExecuteScalar());
			}
		}

		public static string SelectString(string sql, params object[] parameters)
		{
			if (parameters.Length % 2 != 0) throw new ArgumentException("The number of parameters must be of an even length.");

			using (var cmd = CreateCommand(sql))
			{
				for (int i = 0, ii = parameters.Length; i < ii; i += 2)
				{
					cmd.Parameters.AddWithValue((string)parameters[i], parameters[i + 1]);
				}

				var obj = cmd.ExecuteScalar();
				if (obj == null) return null;
				return Convert.ToString(cmd.ExecuteScalar());
			}
		}

		public static long Insert(string tableName, object[] columnValues)
		{
			if (columnValues.Length % 2 != 0) throw new ArgumentException("The length of columnValues must be even.");

			var sb = new StringBuilder();
			sb.Append("insert into ");
			sb.Append(tableName);
			sb.Append(" (");

			for (int i = 0, ii = columnValues.Length; i < ii; i += 2)
			{
				if (i > 0) sb.Append(", ");
				sb.Append((string)columnValues[i]);
			}

			sb.Append(") values (");

			for (int i = 0, ii = columnValues.Length; i < ii; i += 2)
			{
				if (i > 0) sb.Append(", ");
				sb.Append('@');
				sb.Append((string)columnValues[i]);
			}

			sb.Append("); select last_insert_rowid();");

			using (var cmd = CreateCommand(sb.ToString()))
			{
				for (int i = 0, ii = columnValues.Length; i < ii; i += 2)
				{
					cmd.Parameters.AddWithValue(string.Concat("@", (string)columnValues[i]), columnValues[i + 1]);
//#if DEBUG
//					LogParam((string)columnValues[i], Convert.ToString(columnValues[i + 1]));
//#endif
				}

				return Convert.ToInt64(cmd.ExecuteScalar(System.Data.CommandBehavior.SingleResult));
			}
		}

		public static void Update(string tableName, string whereClause, object[] columnValues, object[] additionalParams = null)
		{
			if (columnValues.Length % 2 != 0) throw new ArgumentException("The length of columnValues must be even.");
			if (additionalParams != null && additionalParams.Length % 2 != 0) throw new ArgumentException("The number of additional parameters must be of an even length.");

			var sb = new StringBuilder();
			sb.Append("update ");
			sb.Append(tableName);
			sb.Append(" set ");

			for (int i = 0, ii = columnValues.Length; i < ii; i += 2)
			{
				if (i > 0) sb.Append(", ");
				sb.Append((string)columnValues[i]);
				sb.Append(" = @");
				sb.Append((string)columnValues[i]);
			}

			if (!string.IsNullOrWhiteSpace(whereClause))
			{
				sb.Append(" where ");
				sb.Append(whereClause);
			}

			using (var cmd = CreateCommand(sb.ToString()))
			{
				for (int i = 0, ii = columnValues.Length; i < ii; i += 2)
				{
					cmd.Parameters.AddWithValue(string.Concat("@", (string)columnValues[i]), columnValues[i + 1]);
//#if DEBUG
//					LogParam((string)columnValues[i], Convert.ToString(columnValues[i + 1]));
//#endif
				}

				if (additionalParams != null)
				{
					for (int i = 0, ii = additionalParams.Length; i < ii; i += 2)
					{
						cmd.Parameters.AddWithValue((string)additionalParams[i], additionalParams[i + 1]);
//#if DEBUG
//						LogParam((string)additionalParams[i], Convert.ToString(additionalParams[i + 1]));
//#endif
					}
				}

				cmd.ExecuteNonQuery();
			}
		}

//#if DEBUG
//		private static void LogParam(string name, object value)
//		{
//			if (value == null)
//			{
//				Log.Debug("  @{0}: (null)", name);
//			}
//			else
//			{
//				Log.Debug("  @{0}: {1}", name, Convert.ToString(value));
//			}
//		}
//#endif

		public static void WriteSetting(string name, string value)
		{
			if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");

			var rowid = 0;

			using (var cmd = CreateCommand("select rowid, value from setting where name = @name"))
			{
				cmd.Parameters.AddWithValue("@name", name);
				using (var rdr = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow))
				{
					if (rdr.Read())
					{
						if (rdr.GetString(1) == value) return;
						rowid = rdr.GetInt32(0);
					}
				}
			}

			if (rowid == 0)
			{
				using (var cmd = CreateCommand("insert into setting (name, value) values (@name, @value)"))
				{
					cmd.Parameters.AddWithValue("@name", name);
					cmd.Parameters.AddWithValue("@value", value);
					cmd.ExecuteNonQuery();
				}
			}
			else
			{
				using (var cmd = CreateCommand("update setting set name = @name, value = @value where rowid = @rowid"))
				{
					cmd.Parameters.AddWithValue("@name", name);
					cmd.Parameters.AddWithValue("@value", value);
					cmd.Parameters.AddWithValue("@rowid", rowid);
					cmd.ExecuteNonQuery();
				}
			}
		}

		public static string LoadSetting(string name, string defaultValue = null)
		{
			var ret = SelectString("select value from setting where name = @name", "@name", name);
			if (ret == null) return defaultValue;
			return ret;
		}

		public static Dictionary<string, object> SelectOne(string sql, params object[] parameters)
		{
			if (parameters.Length % 2 != 0) throw new ArgumentException("The number of parameters must be of an even length.");

			using (var cmd = CreateCommand(sql))
			{
				for (int i = 0, ii = parameters.Length; i < ii; i += 2)
				{
					cmd.Parameters.AddWithValue((string)parameters[i], parameters[i + 1]);
				}

				using (var rdr = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow))
				{
					if (rdr.Read())
					{
						var rec = new Dictionary<string, object>();

						for (int f = 0, ff = rdr.FieldCount; f < ff; f++)
						{
							rec[rdr.GetName(f)] = rdr[f];
						}

						return rec;
					}
				}
			}

			return null;
		}

		public static List<Dictionary<string, object>> SelectList(string sql, params object[] parameters)
		{
			if (parameters.Length % 2 != 0) throw new ArgumentException("The number of parameters must be of an even length.");

			var list = new List<Dictionary<string, object>>();

			using (var cmd = CreateCommand(sql))
			{
				for (int i = 0, ii = parameters.Length; i < ii; i += 2)
				{
					cmd.Parameters.AddWithValue((string)parameters[i], parameters[i + 1]);
				}

				using (var rdr = cmd.ExecuteReader())
				{
					while (rdr.Read())
					{
						var row = new Dictionary<string, object>();
						for (int f = 0, ff = rdr.FieldCount; f < ff; f++) row[rdr.GetName(f)] = rdr[f];
						list.Add(row);
					}
				}
			}

			return list;
		}

		public static DataTable SelectDataTable(string sql, params object[] parameters)
		{
			if (parameters.Length % 2 != 0) throw new ArgumentException("The number of parameters must be of an even length.");

			using (var cmd = CreateCommand(sql))
			{
				for (int i = 0, ii = parameters.Length; i < ii; i += 2)
				{
					cmd.Parameters.AddWithValue((string)parameters[i], parameters[i + 1]);
				}

				using (var ad = new SQLiteDataAdapter(cmd))
				{
					var table = new DataTable();
					ad.Fill(table);
					return table;
				}
			}
		}
	}
}
