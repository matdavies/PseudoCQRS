using System;

namespace PseudoCQRS.Checkers
{
	[AttributeUsage( AttributeTargets.Class, AllowMultiple = true, Inherited = true )]
	public abstract class BaseCheckAttribute : Attribute
	{
		public Type CheckerType { get; private set; }

		protected BaseCheckAttribute( Type checkerType )
		{
			CheckerType = checkerType;
		}
	}
}