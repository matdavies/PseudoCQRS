namespace PseudoCQRS.Checkers
{
	public interface IAccessChecker
	{
		CommandResult Check( string propertyName, object instance );
	}
}
