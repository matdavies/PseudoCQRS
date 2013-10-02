using System;

namespace PseudoCQRS.Checkers
{
	public class AuthorizationCheckAttribute : BaseCheckAttribute
	{
		public AuthorizationCheckAttribute( Type checkerType ) : base( checkerType ) { }
	}
}
