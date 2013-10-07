using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PseudoCQRS.Controllers;

namespace PseudoCQRS.Examples.NerdDinner.Modules.DinnersList
{
	public class DinnersListController : BaseReadController<DinnersListViewModel, EmptyViewModelProviderArgument>
	{
		public DinnersListController( IViewModelFactory<DinnersListViewModel, EmptyViewModelProviderArgument> viewModelFactory ) : base( viewModelFactory ) {}

		public override string ViewPath
		{
			get { return "Dinner/List"; }
		}
	}
}