using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;

namespace PseudoCQRS.Configuration
{
	public class DependencyInjectionServiceLocator : ServiceLocatorImplBase
	{
		private readonly IDependencyInjectionContainer _container;

		public DependencyInjectionServiceLocator( IDependencyInjectionContainer container )
		{
			_container = container;
		}

		protected override object DoGetInstance( Type serviceType, string key )
		{
			return _container.Resolve( serviceType );
		}

		protected override IEnumerable<object> DoGetAllInstances( Type serviceType )
		{
			return new object[] {_container.Resolve( serviceType )};
		}
	}
}
