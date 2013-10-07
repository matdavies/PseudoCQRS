namespace PseudoCQRS.Checkers
{
	public interface IValidationChecker<T>
	{
		CheckResult Check( T instance );
	}
}
