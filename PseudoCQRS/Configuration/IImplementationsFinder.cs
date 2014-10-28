using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCQRS.Configuration
{
	internal interface IImplementationsFinder
	{
		Dictionary<Type, Type> FindImplementations();
	}
}
