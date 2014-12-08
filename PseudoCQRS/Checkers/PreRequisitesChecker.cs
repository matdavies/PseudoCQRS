namespace PseudoCQRS.Checkers
{
	public class PrerequisitesChecker : IPrerequisitesChecker
	{
		private readonly ICheckersExecuter _checkersExecuter;

		public PrerequisitesChecker( ICheckersExecuter checkersExecuter )
		{
			_checkersExecuter = checkersExecuter;
		}

		public CheckResult Check<T>( T instance )
		{
			// Refactor: can we do something like DoActionsWhileFalse( x => x.ContainsError,  action1, action2, action3 )
			var result = _checkersExecuter.ExecuteAuthorizationCheckers( instance );
			if ( !result.ContainsError )
			{
				result = _checkersExecuter.ExecuteAccessCheckers( instance );
				if ( !result.ContainsError )
					result = _checkersExecuter.ExecuteValidationCheckers( instance );
			}

			return result;
		}
	}
}