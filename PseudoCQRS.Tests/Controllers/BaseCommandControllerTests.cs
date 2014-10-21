using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using PseudoCQRS.Controllers;
using PseudoCQRS.Tests.Controllers.Helpers;
using Rhino.Mocks;

namespace PseudoCQRS.Tests.Controllers
{
	[TestFixture]
	public class BaseCommandControllerTests
	{
		private IViewModelToCommandMappingEngine _mapper;
		private IServiceLocator _mockedServiceLocator;
		private ICommandExecutor _commandExecutor;
		private IMessageManager _messageManager;
		private IReferrerProvider _referrerProvider;
		private DummyExecuteController _controller;


		[SetUp]
		public void Setup()
		{
			_mapper = MockRepository.GenerateMock<IViewModelToCommandMappingEngine>();
			_mockedServiceLocator = MockRepository.GenerateMock<IServiceLocator>();
			_mockedServiceLocator
				.Stub( x => x.GetInstance<IViewModelToCommandMappingEngine>() )
				.Return( _mapper );
			ServiceLocator.SetLocatorProvider( () => _mockedServiceLocator );

			_commandExecutor = MockRepository.GenerateMock<ICommandExecutor>();
			_messageManager = MockRepository.GenerateMock<IMessageManager>();
			_referrerProvider = MockRepository.GenerateMock<IReferrerProvider>();
			_controller = new DummyExecuteController( _commandExecutor, _messageManager, _referrerProvider );

			var routeData = new RouteData();
			routeData.Values.Add( "controller", "DummyDeleteFile" );
			_controller.ControllerContext = new ControllerContext
			{
				RouteData = routeData
			};
		}

		[Test]
		public void HasSetAllDependencies()
		{
			var commandExecutor = MockRepository.GenerateMock<ICommandExecutor>();

			var locator = MockRepository.GenerateMock<IServiceLocator>();
			locator
				.Stub( x => x.GetInstance<ICommandExecutor>() )
				.Return( commandExecutor );

			ServiceLocator.SetLocatorProvider( () => locator );
			var controller = new DummyCommandController();

			HasSetAllDependenciesControllerHelper.AssertFieldsAreNotNull( controller );
		}
	}
}