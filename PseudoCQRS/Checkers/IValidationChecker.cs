namespace PseudoCQRS.Checkers
{
	public interface IValidationChecker<in T>
	{
		CheckResult Check( T instance );
	}
}