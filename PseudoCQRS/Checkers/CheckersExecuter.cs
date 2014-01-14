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

		public CheckResult ExecuteAuthorizationCheckers( object instance )
		{
			var result = new CheckResult();

			foreach ( var checker in _checkersFinder.FindAuthorizationCheckers( instance ) )
			{
				var checkResult = checker.Check();
				if ( checkResult.ContainsError )
				{
				    result = checkResult;
					break;
				}
			}

			return result;
		}

		public CheckResult ExecuteAccessCheckers( object instance )
		{
			var result = new CheckResult();
			foreach ( var accessCheckerDetails in _checkersFinder.FindAccessCheckers( instance ) )
			{
				var checkResult = accessCheckerDetails.AccessChecker.Check( accessCheckerDetails.PropertyName, instance );
				if ( checkResult.ContainsError )
				{
					result = checkResult;
					break;
				}
			}
			return result;
		}

		public CheckResult ExecuteValidationCheckers<T>( T instance )
		{
		    var result = new CheckResult();

			foreach ( var checker in _checkersFinder.FindValidationCheckers( instance ) )
			{
				var checkResult = checker.Check( instance );
				if ( checkResult.ContainsError )
				{
					result = checkResult;
					break;
				}
			}

			return result;
		}
	}
}