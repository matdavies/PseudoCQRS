using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PseudoCQRS.Examples.NerdDinner.Modules.DinnerList
{
	public class DinnerListViewModel
	{
		public int HostedByUserId { get; set; }
		public Dictionary<int, string> Hosts { get; set; }
		public List<DinnerViewModel> Dinners { get; set; }
	}
}