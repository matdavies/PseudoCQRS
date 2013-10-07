namespace PseudoCQRS.Checkers
{
	public interface IPrerequisitesChecker
	{
		string Check<T>( T instance );
	}
}
