namespace PseudoCQRS
{
	public interface ICommandBus
	{
		CommandResult Execute<TCommand>( TCommand command );
	}
}