// TODO: Check parsing of this! might struggle with eg ,,,"Hello",,
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM.AST
{
	class PrintList
	{
		public List<PrintItem> items;
		public enum printseparator { NULL, COMMA, SEMICOLON };

		public List<printseparator> separators;

		public PrintList()
		{
			items = new List<PrintItem>();
			separators = new List<printseparator>();
		}

		public void add(PrintItem item, printseparator separator)
		{
			items.Add(item);
			separators.Add(separator);
		}

		public void add(PrintItem item)
		{
			items.Add(item);
		}
	}
}
