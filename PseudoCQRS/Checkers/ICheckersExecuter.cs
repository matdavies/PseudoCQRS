namespace PseudoCQRS.Checkers
{
	public interface ICheckersExecuter
	{
		string ExecuteAuthorizationCheckers( object instance );
		string ExecuteAccessCheckers( object instance );
		string ExecuteValidationCheckers<T>( T instance );
	}
}
