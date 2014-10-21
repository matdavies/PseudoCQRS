using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PseudoCQRS.Controllers;

namespace PseudoCQRS.Examples.NerdDinner.Modules.DinnerCreate
{
	public class DinnerCreateController : BaseReadExecuteController<DinnerCreateViewModel, EmptyViewModelProviderArgument, DinnerCreateCommand>
	{
		public override string ViewPath
		{
			get { return "Dinner/Create"; }
		}

		public override ActionResult OnSuccessfulExecution( DinnerCreateViewModel viewModel, CommandResult commandResult )
		{
			return Redirect( "/" );
		}
	}
}