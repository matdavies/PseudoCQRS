namespace PseudoCQRS.Checkers
{
	public class PreRequisitesChecker : IPreRequisitesChecker
	{
		private readonly ICheckersExecuter _checkersExecuter;

		public PreRequisitesChecker( ICheckersExecuter checkersExecuter )
		{
			_checkersExecuter = checkersExecuter;
		}

		public CommandResult Check<T>( T instance )
		{
			// Refactor: can we do something like DoActionsWhileFalse( x => x.ContainsError,  action1, action2, action3 )
			var result = new CommandResult();
			result = _checkersExecuter.ExecuteAuthorizationCheckers( instance );
			if ( !result.ContainsError )
			{
				result = _checkersExecuter.ExecuteAccessCheckers( instance );
				if ( !result.ContainsError )
					result = _checkersExecuter.ExecuteValidaitonCheckers( instance );
			}

			return result;
		}

	}
}