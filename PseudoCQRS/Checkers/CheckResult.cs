using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCQRS.Checkers
{
	public class CheckResult
	{
		public bool ContainsError { get; set; }
		public string Message { get; set; }
	}
}
