using System.Web.Mvc;

namespace PseudoCQRS.Controllers
{
	[DbSessionManager]
	public abstract class BaseCommandController<TViewModel, TCommand> : Controller
	{
		private readonly ICommandExecutor _commandExecutor;

		protected BaseCommandController( ICommandExecutor commandExecutor  )
		{
			_commandExecutor = commandExecutor;
		}

        protected CommandResult ExecuteCommand(TViewModel viewModel)
        {
            var command = Mapper.Map<TViewModel, TCommand>(viewModel);
            var result = _commandExecutor.ExecuteCommand(command);
            return result;
        }
    }
}
