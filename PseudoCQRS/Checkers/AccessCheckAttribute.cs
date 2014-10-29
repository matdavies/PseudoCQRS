using System;

namespace PseudoCQRS.Checkers
{
	public class AccessCheckAttribute : BaseCheckAttribute
	{
		public string PropertyName { get; private set; }

		public AccessCheckAttribute( Type checkerType, string propertyName )
			: base( checkerType )
		{
			PropertyName = propertyName;
		}
	}
}