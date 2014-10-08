namespace PseudoCQRS.Checkers
{
	public interface ICheckersExecuter
	{
		CheckResult ExecuteAuthorizationCheckers( object instance );
		CheckResult ExecuteAccessCheckers( object instance );
		CheckResult ExecuteValidationCheckers<T>( T instance );
	}
}