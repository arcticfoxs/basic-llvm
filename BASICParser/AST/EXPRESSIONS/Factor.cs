using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLLVM.AST
{
	class Factor
	{
		public List<Primary> primarys;

		public Factor()
		{
			primarys = new List<Primary>();
		}
		public Factor(List<Primary> _primarys)
		{
			primarys = _primarys;
		}
		public void add(Primary primary)
		{
			primarys.Add(primary);
		}
	}
}
