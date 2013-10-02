namespace PseudoCQRS.Checkers
{
	public interface IAuthorizationChecker
	{
		CommandResult Check();
	}
}