namespace PseudoCQRS.Checkers
{
	public interface IAuthorizationChecker
	{
		CheckResult Check();
	}
}