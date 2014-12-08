using System.Collections.Generic;

namespace PseudoCQRS.Checkers
{
	public interface ICheckersFinder
	{
		List<IValidationChecker<T>> FindValidationCheckers<T>( T instance );
		List<IAuthorizationChecker> FindAuthorizationCheckers( object instance );
		List<AccessCheckerAttributeDetails> FindAccessCheckers( object instance );
	}
}