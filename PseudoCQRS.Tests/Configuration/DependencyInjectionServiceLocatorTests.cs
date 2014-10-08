using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PseudoCQRS.Configuration;
using Rhino.Mocks;

namespace PseudoCQRS.Tests.Configuration
{
	[TestFixture]
	public class DependencyInjectionServiceLocatorTests
	{
		public class DummyDependencyInjectionServiceLocator : DependencyInjectionServiceLocator
		{
			public DummyDependencyInjectionServiceLocator( IDependencyInjectionContainer container ) : base( container ) {}

			public object CallDoGetInstance( Type serviceType, string key )
			{
				return DoGetInstance( serviceType, key );
			}

			public IEnumerable<object> CallDoGetAllInstances( Type serviceType )
			{
				return DoGetAllInstances( serviceType );
			}
		}

		[Test]
		public void DoGetInstance_WhenCalledWithAServiceType_UsesDIContainerToResolveObject()
		{
			var container = MockRepository.GenerateMock<IDependencyInjectionContainer>();
			var serviceLocator = new DummyDependencyInjectionServiceLocator( container );
			serviceLocator.CallDoGetInstance( this.GetType(), String.Empty );

			container.AssertWasCalled( x => x.Resolve( this.GetType() ) );
		}

		[Test]
		public void DoGetAllInstances_WhenCalledWithAServiceType_UsesDIContainerToResolveObject()
		{
			var container = MockRepository.GenerateMock<IDependencyInjectionContainer>();
			var serviceLocator = new DummyDependencyInjectionServiceLocator( container );
			serviceLocator.CallDoGetAllInstances( this.GetType() );

			container.AssertWasCalled( x => x.Resolve( this.GetType() ) );
		}
	}
}