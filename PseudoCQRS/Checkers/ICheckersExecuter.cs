namespace PseudoCQRS.Checkers
{
	public interface ICheckersExecuter
	{
		CommandResult ExecuteAuthorizationCheckers( object instance );
		CommandResult ExecuteAccessCheckers( object instance );
		CommandResult ExecuteValidaitonCheckers<T>( T instance );
	}
}
