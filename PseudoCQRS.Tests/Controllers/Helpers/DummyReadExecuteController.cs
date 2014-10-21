using System.Web.Mvc;
using PseudoCQRS.Controllers;

namespace PseudoCQRS.Tests.Controllers.Helpers
{
	public class DummyReadExecuteController : BaseReadExecuteController<DummyReadExecuteViewModel, DummyReadExecuteViewModelArgs, DummyReadExecuteCommand>
	{
		public DummyReadExecuteController(
			ICommandExecutor commandExecutor,
			IViewModelFactory<DummyReadExecuteViewModel, DummyReadExecuteViewModelArgs> viewModelFactory )
			: base( commandExecutor, viewModelFactory ) {}

		public DummyReadExecuteController() {}

		public override ActionResult OnFailureExecution( DummyReadExecuteViewModel viewModel )
		{
			var actionResult = base.OnFailureExecution( viewModel );
			TempData[ "Error" ] = "Failed";
			return actionResult;
		}

		public override ActionResult OnSuccessfulExecution( DummyReadExecuteViewModel viewModel, CommandResult command )
		{
			return Content( "Success" );
		}

		public override string ViewPath
		{
			get { return "Testing/Page"; }
		}
	}
}