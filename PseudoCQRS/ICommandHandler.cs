namespace PseudoCQRS
{
	public interface ICommandHandler<TCommand>
	{
		CommandResult Handle( TCommand cmd );
	}
}
