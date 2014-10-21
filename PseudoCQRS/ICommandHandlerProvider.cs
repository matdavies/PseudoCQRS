namespace PseudoCQRS
{
	public interface ICommandHandlerProvider
	{
		ICommandHandler<TCommand> GetCommandHandler<TCommand>();
	}
}