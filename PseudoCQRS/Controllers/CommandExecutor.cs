namespace PseudoCQRS.Controllers
{
	public class CommandExecutor : ICommandExecutor 
	{

		private readonly ICommandBus _commandBus;
		private readonly IMessageManager _messageManager;

		public CommandExecutor( 
			ICommandBus commandBus, 
			IMessageManager messageManager )
		{
			_commandBus = commandBus;
			_messageManager = messageManager;
		}

		public CommandResult ExecuteCommand<TCommand>( TCommand command )
		{
			var result = _commandBus.Execute( command );
			if ( result.ContainsError )
				_messageManager.SetErrorMessage( result.Message );
			else
				_messageManager.SetSuccessMessage( result.Message );
			return result;
		}
	}
}