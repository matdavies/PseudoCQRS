using PseudoCQRS.Controllers;

namespace PseudoCQRS.Tests.Controllers.Helpers
{
	public class DummyExecuteController : BaseExecuteController<DummyExecuteViewModel, DummyExecuteCommand>
	{


		public DummyExecuteController( ICommandExecutor commandExecutor, IMessageManager messageManager )
			: base( commandExecutor, messageManager )
		{
		}

		public override System.Web.Mvc.ActionResult OnFailureExecution( DummyExecuteViewModel viewModel )
		{
			return Content( "Error" );
		}

		public override System.Web.Mvc.ActionResult OnSuccessfulExecution( DummyExecuteViewModel viewModel, CommandResult command )
		{
			return Content( "Success" );
		}
	}
}
