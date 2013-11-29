using System.Collections.Generic;
using FluentValidation.Attributes;

namespace PseudoCQRS.Examples.NerdDinner.Modules.DinnerCreate
{
	[Validator( typeof( DinnerCreateViewModelValidator ) )]
	public class DinnerCreateViewModel
	{
		public string Title { get; set; }
		public string EventDate { get; set; }
		public string Description { get; set; }
		public int HostedById { get; set; }
		public Dictionary<int, string> Hosts { get; set; }
	}
}