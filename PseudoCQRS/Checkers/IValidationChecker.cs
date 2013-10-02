namespace PseudoCQRS.Checkers
{
	public interface IValidationChecker<T>
	{
		CommandResult Check( T instance );
	}
}
