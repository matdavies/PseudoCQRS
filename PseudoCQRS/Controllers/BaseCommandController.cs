using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

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

	    public BaseCommandController()
	        : this( ServiceLocator.Current.GetInstance<ICommandExecutor>() ) {}


	    protected virtual CommandResult ExecuteCommand(TViewModel viewModel)
        {
            var command = ConvertViewModelToCommand( viewModel );
            var result = ExecuteCommand( command );
            return result;
        }

	    private CommandResult ExecuteCommand( TCommand command )
	    {
	        return _commandExecutor.ExecuteCommand(command);
	    }

	    protected virtual TCommand ConvertViewModelToCommand( TViewModel viewModel )
	    {
	        return Mapper.Map<TViewModel, TCommand>(viewModel);
	    }
	}
}
