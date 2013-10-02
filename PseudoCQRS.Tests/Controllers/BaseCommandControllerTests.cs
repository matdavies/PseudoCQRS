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
		private const string ErrorMessage = "Error Message returned from CommandBus";
		private const string SuccessMessage = "Success Message returned from CommandBus";

		private ISpawtzMappingEngine _mapper;
		private IServiceLocator _mockedServiceLocator;
		private ICommandExecutor _commandExecutor;
		private IMessageManager _messageManager;
		private DummyExecuteController _controller;


		[SetUp]
		public void Setup()
		{
			_mapper = MockRepository.GenerateMock<ISpawtzMappingEngine>();
			_mockedServiceLocator = MockRepository.GenerateMock<IServiceLocator>();
			_mockedServiceLocator
				.Stub( x => x.GetInstance<ISpawtzMappingEngine>() )
				.Return( _mapper );
			ServiceLocator.SetLocatorProvider( () => _mockedServiceLocator );

			_commandExecutor = MockRepository.GenerateMock<ICommandExecutor>();
			_messageManager = MockRepository.GenerateMock<IMessageManager>();
			_controller = new DummyExecuteController( _commandExecutor, _messageManager );

			var routeData = new RouteData();
			routeData.Values.Add( "controller", "DummyDeleteFile" );
			_controller.ControllerContext = new ControllerContext { RouteData = routeData };

		}

		private ActionResult ArrangeAndAct( bool commandBusError = false )
		{
			_commandExecutor
				.Stub( x => x.ExecuteCommand<DummyExecuteCommand>( null ) )
				.IgnoreArguments()
				.Return( new CommandResult
				{
					ContainsError = commandBusError,
					Message = commandBusError ? ErrorMessage : SuccessMessage,
				} );

			return _controller.Execute( new DummyExecuteViewModel() );
		}

		[Test]
		public void ShouldReturnSuccessActionResult_WhenNoErrorReturnedFromCommandBus()
		{
			var result = ArrangeAndAct();
			Assert.AreEqual( "Success", ( (ContentResult)result ).Content );
		}

		[Test]
		public void ShouldReturnOnFailureActionResult_WhenCommandBusReturnsAnError()
		{
			var result = ArrangeAndAct( commandBusError: true );
			Assert.AreEqual( "Error", ( (ContentResult)result ).Content );
		}

	}
}
