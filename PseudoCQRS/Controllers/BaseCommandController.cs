using System.Web.Mvc;

namespace PseudoCQRS.Controllers
{
	[DbSessionManager]
	public abstract class BaseCommandController<TViewModel, TCommand> : Controller
	{
		public abstract ActionResult OnSuccessfulExecution( TViewModel viewModel, CommandResult cmdResult );
		public abstract ActionResult OnFailureExecution( TViewModel viewModel );

		private readonly ICommandExecutor _commandExecutor;

		protected BaseCommandController( ICommandExecutor commandExecutor  )
		{
			_commandExecutor = commandExecutor;
		}

		protected ActionResult ExecuteCommand( TViewModel viewModel )
		{
			var command = Mapper.Map<TViewModel, TCommand>( viewModel );
			var result = _commandExecutor.ExecuteCommand( command );
			return result.ContainsError ? OnFailureExecution( viewModel ) : OnSuccessfulExecution( viewModel, result );
		}
	}
}
