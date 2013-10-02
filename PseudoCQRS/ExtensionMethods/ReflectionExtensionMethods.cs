using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PseudoCQRS.ExtensionMethods
{
	public static class ReflectionExtensionMethods
	{
		public static IEnumerable<Type> GetImplementationsOf( this Assembly assembly, Type genericType, Type genericArgumentType )
		{
			return assembly.GetTypes().Where( x => x.GetInterfaces().Any( y =>
					y.IsGenericType && y.GetGenericTypeDefinition() == genericType && y.GetGenericArguments().First() == genericArgumentType ) );
		}
	}
}
