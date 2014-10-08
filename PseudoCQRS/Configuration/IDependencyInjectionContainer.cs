using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCQRS.Configuration
{
	public interface IDependencyInjectionContainer
	{
		void Register( Type interfaceType, Type implementationType );
		void Register<TInterface, TImplementation>();
		void Register( IDictionary<Type, Type> mappings );
		object Resolve( Type type );
		TService Resolve<TService>() where TService : class;
	}
}