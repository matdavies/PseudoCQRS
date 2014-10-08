using System;

namespace PseudoCQRS.Checkers
{
	public class ValidationCheckAttribute : BaseCheckAttribute
	{
		public ValidationCheckAttribute( Type checkerType ) : base( checkerType ) {}
	}
}