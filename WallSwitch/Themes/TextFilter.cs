using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallSwitch.Themes
{
	class TextFilter
	{
		private string[] _words;

		public TextFilter(string text)
		{
			ParseFilterText(text);
		}

		private void ParseFilterText(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				_words = null;
				return;
			}

			var wordList = new List<string>();

			var sb = new StringBuilder();
			foreach (var ch in text)
			{
				if (char.IsWhiteSpace(ch))
				{
					if (sb.Length > 0)
					{
						wordList.Add(sb.ToString());
						sb.Clear();
					}
				}
				else
				{
					sb.Append(ch);
				}
			}

			if (sb.Length > 0) wordList.Add(sb.ToString());
			_words = wordList.ToArray();
		}

		public bool Match(string text)
		{
			if (_words == null) return true;

			foreach (var word in _words)
			{
				if (text.IndexOf(word, StringComparison.CurrentCultureIgnoreCase) < 0) return false;
			}

			return true;
		}
	}
}
