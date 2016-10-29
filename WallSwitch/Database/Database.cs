using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using WallSwitch.SettingsStore;

namespace WallSwitch
{
	class Database : IDisposable
	{

		private SQLiteConnection _conn;
		private static bool _isNew;
		private static bool _initCompleted;
		private static string _dbFileName;

		#region Initialization Scripts
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
latest_guid					varchar(40),
filter_xml					text
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
cache_path		varchar(300),
pub_date		datetime,
rating			integer,
thumb			blob,
size			bigint
)",
@"create index img_ix_theme on img (theme_id, path)",
@"create index img_ix_location on img (location_id)",
@"create index img_ix_path on img (path)",
@"create index img_ix_cachepath on img (cache_path)",

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
cache_path		varchar(300),
pub_date		datetime,
rating			integer,
thumb			blob,
size			bigint
)",
@"create index history_ix_theme on history (theme_id)",
@"create index history_ix_path on history (path)",

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
		#endregion

		public Database()
		{
			try
			{
				var fileName = FileName;
				var isNew = false;
				if (!File.Exists(fileName))
				{
					SQLiteConnection.CreateFile(fileName);
					_isNew = isNew = true;
				}

				Log.Debug("Connecting to database");
				_conn = new SQLiteConnection(string.Format("Data Source={0};Version=3;", fileName));
				_conn.Open();

				if (isNew)
				{
					RunNewScripts(k_initializationScripts);
				}
				else
				{
					if (!_initCompleted) RunExistingScripts();
				}

				_initCompleted = true;
			}
			catch (Exception ex)
			{
				Log.Write(ex);
				Dispose();
			}
		}

		public void Dispose()
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

		public void Close()
		{
			Dispose();
		}

		private void RunNewScripts(string[] scripts)
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

		private void RunExistingScripts()
		{
			AddTableColumnIfMissing("theme", "filter_xml", "text");

			AddTableColumnIfMissing("img", "rating", "integer");
			AddTableColumnIfMissing("img", "thumb", "blob");
			AddTableColumnIfMissing("img", "size", "bigint");
			AddTableColumnIfMissing("img", "cache_path", "varchar(300)");
			AddIndexIfMissing("img_ix_path", "create index img_ix_path on img (path)");
			AddIndexIfMissing("img_ix_cachepath", "create index img_ix_cachepath on img (cache_path)");

			AddTableColumnIfMissing("history", "rating", "integer");
			AddTableColumnIfMissing("history", "thumb", "blob");
			AddTableColumnIfMissing("history", "size", "bigint");
			AddTableColumnIfMissing("history", "cache_path", "varchar(300)");
			AddIndexIfMissing("history_ix_path", "create index history_ix_path on history (path)");
		}

		private void AddTableColumnIfMissing(string tableName, string columnName, string dataType)
		{
			using (var cmd = CreateCommand(string.Format("select * from {0} limit 1", tableName)))
			{
				using (var rdr = cmd.ExecuteReader(CommandBehavior.SingleRow))
				{
					for (int i = 0; i < rdr.FieldCount; i++)
					{
						if (rdr.GetName(i) == columnName) return;
					}
				}
			}

			using (var cmd = CreateCommand(string.Format("alter table {0} add column {1} {2}", tableName, columnName, dataType)))
			{
				cmd.ExecuteNonQuery();
			}
		}

		private void AddIndexIfMissing(string indexName, string createStatement)
		{
			using (var cmd = CreateCommand("select count(*) from sqlite_master where type = 'index' and name = @name"))
			{
				cmd.Parameters.AddWithValue("@name", indexName);
				int count = Convert.ToInt32(cmd.ExecuteScalar());
				if (count > 0) return;
			}

			using (var cmd = CreateCommand(createStatement))
			{
				cmd.ExecuteNonQuery();
			}
		}

		public static string FileName
		{
			get
			{
				if (_dbFileName == null)
				{
					using (var key = Registry.CurrentUser.OpenSubKey(Settings.RegistryKey, false))
					{
						if (key != null)
						{
							var obj = key.GetValue("SettingsDatabase", null);
							if (obj != null) _dbFileName = Convert.ToString(obj);
						}
					}

					if (_dbFileName == null)
					{
						using (var key = Registry.LocalMachine.OpenSubKey(Settings.RegistryKey, false))
						{
							if (key != null)
							{
								var obj = key.GetValue("SettingsDatabase", null);
								if (obj != null) _dbFileName = Convert.ToString(obj);
							}
						}
					}

					if (_dbFileName == null)
					{
						_dbFileName = Path.Combine(Util.AppDataDir, Res.DatabaseFileName);
					}
				}
				
				return _dbFileName;
			}
		}

		public static bool IsNew
		{
			get { return _isNew; }
		}

		public SQLiteCommand CreateCommand(string sql, LogLevel logLevel = LogLevel.Debug)
		{
			if (_conn == null) throw new InvalidOperationException("No connection to SQL database.");

			Log.Write(logLevel, "SQL> {0}", sql);

			var cmd = _conn.CreateCommand();
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = sql;
			return cmd;
		}

		public void ExecuteNonQuery(string sql, params object[] parameters)
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

		public List<int> SelectIntList(string sql, params object[] parameters)
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

		public List<long> SelectLongList(string sql, params object[] parameters)
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

		public List<string> SelectStringList(string sql, params object[] parameters)
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

		public int SelectInt(string sql, params object[] parameters)
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

		public string SelectString(string sql, params object[] parameters)
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

		public long Insert(string tableName, object[] columnValues)
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
				}

				return Convert.ToInt64(cmd.ExecuteScalar(System.Data.CommandBehavior.SingleResult));
			}
		}

		public void Update(string tableName, string whereClause, object[] columnValues, object[] additionalParams = null)
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
				}

				if (additionalParams != null)
				{
					for (int i = 0, ii = additionalParams.Length; i < ii; i += 2)
					{
						cmd.Parameters.AddWithValue((string)additionalParams[i], additionalParams[i + 1]);
					}
				}

				cmd.ExecuteNonQuery();
			}
		}

		public void WriteSetting(string name, string value)
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

		public string LoadSetting(string name, string defaultValue = null)
		{
			var ret = SelectString("select value from setting where name = @name", "@name", name);
			if (ret == null) return defaultValue;
			return ret;
		}

		public Dictionary<string, object> SelectOne(string sql, params object[] parameters)
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

		public List<Dictionary<string, object>> SelectList(string sql, params object[] parameters)
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

		public DataTable SelectDataTable(string sql, params object[] parameters)
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

		public SQLiteTransaction BeginTransaction()
		{
			return _conn.BeginTransaction();
		}
	}
}
