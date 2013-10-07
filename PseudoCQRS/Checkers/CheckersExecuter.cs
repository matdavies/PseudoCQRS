using System;

namespace PseudoCQRS.Checkers
{
	public class CheckersExecuter : ICheckersExecuter
	{
		private readonly ICheckersFinder _checkersFinder;

		public CheckersExecuter( ICheckersFinder checkersFinder )
		{
			_checkersFinder = checkersFinder;
		}

		public string ExecuteAuthorizationCheckers( object instance )
		{
			var result = String.Empty;

			foreach ( var checker in _checkersFinder.FindAuthorizationCheckers( instance ) )
			{
				var checkResult = checker.Check();
				if ( checkResult.ContainsError )
				{
					result = checkResult.Message;
					break;
				}
			}

			return result;
		}

		public string ExecuteAccessCheckers( object instance )
		{
			var result = String.Empty;
			foreach ( var accessCheckerDetails in _checkersFinder.FindAccessCheckers( instance ) )
			{
				var checkResult = accessCheckerDetails.AccessChecker.Check( accessCheckerDetails.PropertyName, instance );
				if ( checkResult.ContainsError )
				{
					result = checkResult.Message;
					break;
				}
			}
			return result;
		}

		public string ExecuteValidationCheckers<T>( T instance )
		{
			var result = String.Empty;

			foreach ( var checker in _checkersFinder.FindValidationCheckers<T>( instance ) )
			{
				var checkResult = checker.Check( instance );
				if ( checkResult.ContainsError )
				{
					result = checkResult.Message;
					break;
				}
			}

			return result;
		}
	}
}