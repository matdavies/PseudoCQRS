using System;
using System.Collections.Generic;
using NUnit.Framework;
using PseudoCQRS.Configuration;

namespace PseudoCQRS.Tests.Configuration
{
	[TestFixture]
	public class DependencyInjectionContainerTests
	{
		[SetUp]
		public void Setup()
		{
			_container = new DependencyInjectionContainer();
		}

		[TearDown]
		public void TearDown()
		{
			_container.ClearAll();
		}

		public interface IInterface {}

		public class Implementation : IInterface {}

		public interface IInterface2 {}

		public class Implementation2 : IInterface2 {}

		public class ClassWithDependencies
		{
			public ClassWithDependencies( IInterface iInterface, IInterface2 interface2 )
			{
				Interface = iInterface;
				Interface2 = interface2;
			}

			public IInterface Interface { get; private set; }

			public IInterface2 Interface2 { get; private set; }
		}

		public class ClassWithNoConstructor {}

		private DependencyInjectionContainer _container;

		private void AssertValidImplementationInstance( Type implementationType, object instance )
		{
			Assert.IsAssignableFrom( implementationType, instance );
			Assert.IsNotNull( instance );
		}

		[Test]
		public void CanRegisterAndResolveADependency()
		{
			_container.Register<IInterface, Implementation>();
			var implementation = _container.Resolve<IInterface>();

			AssertValidImplementationInstance( typeof( Implementation ), implementation );
		}

		[Test]
		public void CanRegisterMultipleDependencies()
		{
			var implementationType = typeof( Implementation );
			var implementation2Type = typeof( Implementation2 );
			_container.Register( new Dictionary<Type, Type>
			{
				{ typeof( IInterface ), implementationType },
				{ typeof( IInterface2 ), implementation2Type }
			} );

			AssertValidImplementationInstance( implementationType, _container.Resolve<IInterface>() );
			AssertValidImplementationInstance( implementation2Type, _container.Resolve<IInterface2>() );
		}

		[Test]
		public void CanResolveATypeWithDependencies()
		{
			_container.Register<IInterface, Implementation>();
			_container.Register<IInterface2, Implementation2>();

			var classWithDependencies = _container.Resolve<ClassWithDependencies>();
			Assert.IsNotNull( classWithDependencies );
		}

		[Test]
		public void CanResolveATypeWithNoConstructors()
		{
			_container.Register<IInterface, Implementation>();
			var classWithNoConstructor = _container.Resolve<ClassWithNoConstructor>();
			Assert.IsNotNull( classWithNoConstructor );
		}

		[Test]
		public void Resolve_AUnregisteredInterfaceType_ThrowAnException()
		{
			Assert.Throws<Exception>( () => _container.Resolve<IInterface>() );
		}
	}
}