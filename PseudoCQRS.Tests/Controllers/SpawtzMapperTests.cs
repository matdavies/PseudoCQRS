using System;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using PseudoCQRS.Controllers;
using Rhino.Mocks;

namespace PseudoCQRS.Tests.Controllers
{
	[TestFixture]
	public class SpawtzMapperTests
	{
		public class TestViewModel { }

		public class TestCommand { }

		[SetUp]
		public void Setup()
		{
			ServiceLocator.SetLocatorProvider( () => null );
		}

		[Test]
		public void ShouldRaiseApplicationExceptionIfServiceLocatorNotRegisterred()
		{
			Assert.Throws<ApplicationException>( () => SpawtzMapper.Map<TestViewModel, TestCommand>( new TestViewModel() ) );
		}

		[Test]
		public void ShouldRaiseExceptionWhenImplementationNotFound()
		{
			var mockedServiceLocator = MockRepository.GenerateMock<IServiceLocator>();
			ServiceLocator.SetLocatorProvider( () => mockedServiceLocator );
			Assert.Throws<Exception>( () => SpawtzMapper.Map<TestViewModel, TestCommand>( new TestViewModel() ) );
		}

		[Test]
		public void ShouldNotRaiseExceptionWhenImplementationFound()
		{
			var mockedServiceLocator = MockRepository.GenerateMock<IServiceLocator>();
			mockedServiceLocator
				.Stub( x => x.GetInstance<ISpawtzMappingEngine>() )
				.Repeat.Once()
				.Return( MockRepository.GenerateMock<ISpawtzMappingEngine>() );
			ServiceLocator.SetLocatorProvider( () => mockedServiceLocator );

			Assert.DoesNotThrow( () => SpawtzMapper.Map<TestViewModel, TestCommand>( new TestViewModel() ) );
		}
	}
}
