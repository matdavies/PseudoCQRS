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
		public DinnerCreateController(
			ICommandExecutor commandExecutor,
			IViewModelFactory<DinnerCreateViewModel, EmptyViewModelProviderArgument> viewModelFactory )
			: base( commandExecutor, viewModelFactory )
		{
			
		}

		public override ActionResult OnSuccessfulExecution( DinnerCreateViewModel viewModel, CommandResult commandResult )
		{
			return RedirectToAction( "Details", "Dinner", new { ((DinnerCreateCommandResult)commandResult).Id } );
		}

		public override string ViewPath
		{
			get { return "Dinner/Create"; }
		}
	}
}