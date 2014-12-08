using System;

namespace PseudoCQRS
{
	[AttributeUsage( AttributeTargets.Class, AllowMultiple = false, Inherited = false )]
	public class DbTransactionAttribute : Attribute
	{
		
	}
}