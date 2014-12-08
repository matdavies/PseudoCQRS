using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PseudoCQRS.Configuration;

namespace PseudoCQRS.Mvc
{
	internal class ImplementationsFinder : IImplementationsFinder
	{
		public Dictionary<Type, Type> FindImplementations()
		{
			return new Dictionary<Type, Type>
			{
				{ typeof( IHttpContextWrapper ), typeof( HttpContextWrapper ) }
			};
		}
	}
}
