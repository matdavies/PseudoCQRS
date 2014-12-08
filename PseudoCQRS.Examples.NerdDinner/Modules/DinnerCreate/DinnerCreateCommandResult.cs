using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PseudoCQRS.Examples.NerdDinner.Modules.DinnerCreate
{
	public class DinnerCreateCommandResult : CommandResult
	{
		public int Id { get; set; }
	}
}