namespace PseudoCQRS.Checkers
{
	public class CheckersExecuter : ICheckersExecuter
	{
		private readonly ICheckersFinder _checkersFinder;

		public CheckersExecuter( ICheckersFinder checkersFinder )
		{
			_checkersFinder = checkersFinder;
		}

		public CommandResult ExecuteAuthorizationCheckers( object instance )
		{
			var result = new CommandResult();

			foreach ( var checker in _checkersFinder.FindAuthorizationCheckers( instance ) )
			{
				result = checker.Check();
				if ( result.ContainsError )
					break;
			}

			return result;
		}

		public CommandResult ExecuteAccessCheckers( object instance )
		{
			var result = new CommandResult();
			foreach ( var accessCheckerDetails in _checkersFinder.FindAccessCheckers( instance ) )
			{
				result = accessCheckerDetails.AccessChecker.Check( accessCheckerDetails.PropertyName, instance );
				if ( result.ContainsError )
					break;
			}
			return result;
		}

		public CommandResult ExecuteValidaitonCheckers<T>( T instance )
		{
			var result = new CommandResult();


			foreach ( var checker in _checkersFinder.FindValidationCheckers<T>( instance ) )
			{
				result = checker.Check( instance );
				if ( result.ContainsError )
					break;
			}

			return result;
		}
	}
}