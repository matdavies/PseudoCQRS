namespace PseudoCQRS.Checkers
{
	public interface IPreRequisitesChecker
	{
		CommandResult Check<T>( T instance );
	}
}
