using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCQRS
{
	public class ArgumentBindingException : Exception
	{
		public string Value { get; }
		public Type PropertyType { get; }

		public ArgumentBindingException( string value, Type propertyType, Exception innerException = null )
			: base( String.Empty, innerException )
		{
			Value = value;
			PropertyType = propertyType;
		}

		public override string Message => $"Cannot bind '{Value}' to property '{PropertyType.Name}";
	}
}