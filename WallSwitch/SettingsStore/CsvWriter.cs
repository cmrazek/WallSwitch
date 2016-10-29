using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallSwitch.SettingsStore
{
	class CsvWriter : IDisposable
	{
		private StreamWriter _writer;
		private bool _first;

		public CsvWriter(Stream stream)
		{
			_writer = new StreamWriter(stream);
			_first = true;
		}

		public CsvWriter(string fileName)
		{
			_writer = new StreamWriter(fileName);
			_first = true;
		}

		public void Dispose()
		{
			if (_writer != null)
			{
				_writer.Dispose();
				_writer = null;
			}
		}

		public void Close()
		{
			Dispose();
		}

		public void Write(string text)
		{
			if (_writer == null) throw new InvalidOperationException("Writer is not initialized.");

			var sb = new StringBuilder();
			var quotesRequired = false;

			foreach (var ch in text)
			{
				if (ch == '\"')
				{
					sb.Append("\"\"");
					quotesRequired = true;
				}
				else if (ch == ',')
				{
					sb.Append(',');
					quotesRequired = true;
				}
				else if (ch == '\r' || ch == '\n') { }
				else
				{
					sb.Append(ch);
				}
			}

			if (quotesRequired)
			{
				sb.Append('\"');
				sb.Insert(0, '\"');
			}

			if (_first) _first = false;
			else _writer.Write(",");

			_writer.Write(sb.ToString());
		}

		public void WriteLine()
		{
			if (_writer == null) throw new InvalidOperationException("Writer is not initialized.");

			_writer.WriteLine();
			_first = true;
		}

		public void Write(params string[] cols)
		{
			foreach (var col in cols) Write(col);
		}

		public void WriteLine(string text)
		{
			Write(text);
			WriteLine();
		}

		public void WriteLine(params string[] cols)
		{
			foreach (var col in cols) Write(col);
			WriteLine();
		}
	}
}
