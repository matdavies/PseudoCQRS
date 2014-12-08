namespace PseudoCQRS.Checkers
{
	public interface IPrerequisitesChecker
	{
		CheckResult Check<T>( T instance );
	}
}