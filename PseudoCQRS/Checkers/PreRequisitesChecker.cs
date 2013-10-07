using System;

namespace PseudoCQRS.Checkers
{
	public class PrerequisitesChecker : IPrerequisitesChecker
	{
		private readonly ICheckersExecuter _checkersExecuter;

		public PrerequisitesChecker( ICheckersExecuter checkersExecuter )
		{
			_checkersExecuter = checkersExecuter;
		}

		public string Check<T>( T instance )
		{
			// Refactor: can we do something like DoActionsWhileFalse( x => x.ContainsError,  action1, action2, action3 )
			string result = _checkersExecuter.ExecuteAuthorizationCheckers( instance );
			if ( String.IsNullOrEmpty( result ) )
			{
				result = _checkersExecuter.ExecuteAccessCheckers( instance );
				if ( String.IsNullOrEmpty( result ) )
					result = _checkersExecuter.ExecuteValidationCheckers( instance );
			}

			return result;
		}

	}
}