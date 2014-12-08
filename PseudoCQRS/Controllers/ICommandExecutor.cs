namespace PseudoCQRS.Controllers
{
	public interface ICommandExecutor
	{
		CommandResult ExecuteCommand<TCommand>( TCommand command );
	}
}