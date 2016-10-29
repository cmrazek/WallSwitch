using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallSwitch.SettingsStore
{
	class CsvReader : IDisposable
	{
		private StreamReader _reader;
		private bool _endOfFile;
		private List<string> _cols = new List<string>();
		private string[] _headers;

		public CsvReader(Stream stream)
		{
			_reader = new StreamReader(stream);
		}

		public CsvReader(string fileName)
		{
			_reader = new StreamReader(fileName);
		}

		public void Dispose()
		{
			if (_reader != null)
			{
				_reader.Close();
				_reader = null;
			}
		}

		public void Close()
		{
			Dispose();
		}

		public bool EndOfFile
		{
			get { return _endOfFile; }
		}

		public bool ReadLine()
		{
			_cols.Clear();

			var line = _reader.ReadLine();
			if (line == null)
			{
				_endOfFile = true;
				return false;
			}

			var sb = new StringBuilder();
			var insideQuotes = false;
			var afterComma = false;

			for (int pos = 0, len = line.Length; pos < len; pos++)
			{
				var ch = line[pos];

				if (ch == ',' && !insideQuotes)
				{
					_cols.Add(sb.ToString().Trim());
					sb.Clear();
					afterComma = true;
				}
				else if (ch == '\"')
				{
					if (afterComma)
					{
						insideQuotes = true;
					}
					else if (pos + 1 < len && line[pos + 1] == '\"')
					{
						sb.Append('\"');
					}
					else
					{
						insideQuotes = false;
					}
					afterComma = false;
				}
				else if (char.IsWhiteSpace(ch))
				{
					if (!afterComma) sb.Append(ch);
				}
				else
				{
					sb.Append(ch);
					afterComma = false;
				}
			}

			_cols.Add(sb.ToString().Trim());
			return true;
		}

		public bool ReadHeaders()
		{
			if (!ReadLine()) return false;
			_headers = _cols.ToArray();
			return true;
		}

		public int Count
		{
			get { return _cols.Count; }
		}

		public string this[int index]
		{
			get { return _cols[index]; }
		}

		public string this[string headerName]
		{
			get
			{
				var index = GetHeaderIndex(headerName);
				if (index < 0 || index >= _cols.Count) return null;
				return _cols[index];
			}
		}

		public int GetHeaderIndex(string headerName)
		{
			if (_headers == null) return -1;

			for (int i = 0, ii = _headers.Length; i < ii; i++)
			{
				if (string.Equals(_headers[i], headerName, StringComparison.OrdinalIgnoreCase)) return i;
			}

			return -1;
		}
	}
}
