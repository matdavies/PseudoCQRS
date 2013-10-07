namespace PseudoCQRS.Checkers
{
	public interface IAccessChecker
	{
		CheckResult Check( string propertyName, object instance );
	}
}
