using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PseudoCQRS.Examples.NerdDinner.Modules.DinnerCreate
{
	public class DinnerCreateCommand
	{
		public string Title { get; set; }
		public DateTime EventDate { get; set; }
		public string Description { get; set; }
		public int HostedById { get; set; }
	}
}