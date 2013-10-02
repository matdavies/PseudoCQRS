namespace PseudoCQRS
{
	public interface ICommandHandlerFinder
	{
		ICommandHandler<TCommand> FindHandlerForCommand<TCommand>();
	}
}
