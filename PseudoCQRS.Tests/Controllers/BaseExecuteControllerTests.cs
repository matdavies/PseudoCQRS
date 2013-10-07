using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using PseudoCQRS.Controllers;
using PseudoCQRS.Tests.Controllers.Helpers;
using Rhino.Mocks;

namespace PseudoCQRS.Tests.Controllers
{
	[TestFixture]
	public class BaseExecuteControllerTests
	{
		private ICommandExecutor _commandExecutor;
		private IMessageManager _messageManager;
		private DummyExecuteController _controller;

		[SetUp]
		public void Setup()
		{
			_commandExecutor = MockRepository.GenerateMock<ICommandExecutor>();
			_messageManager = MockRepository.GenerateMock<IMessageManager>();
			_controller = new DummyExecuteController( _commandExecutor, _messageManager );
		}

		private ActionResult ArrangeAndAct_WhenModalStateIsNotValid()
		{
			_controller.ModelState.AddModelError( "TestError", "Error Message" );
			return _controller.Execute( new DummyExecuteViewModel() );
		}

		private ActionResult ArrangeAndAct_WhenModelStateIsValid()
		{
			var mapper = MockRepository.GenerateMock<IViewModelToCommandMappingEngine>();
			var mockedServiceLocator = MockRepository.GenerateMock<IServiceLocator>();
			mockedServiceLocator
				.Stub( x => x.GetInstance<IViewModelToCommandMappingEngine>() )
				.Return( mapper );
			ServiceLocator.SetLocatorProvider( () => mockedServiceLocator );
			_commandExecutor
				.Stub( x => x.ExecuteCommand<DummyExecuteCommand>( null ) )
				.IgnoreArguments()
				.Return( new CommandResult
				{
					ContainsError = false,
				} );

			return _controller.Execute( new DummyExecuteViewModel() );
		}

		[Test]
		public void ShouldSetValidationFailue_WhenModalStateIsNotValid()
		{
			ArrangeAndAct_WhenModalStateIsNotValid();
			_messageManager
				.AssertWasCalled( x => x.SetErrorMessage( Arg<string>.Is.Anything ) );
		}

		[Test]
		public void ShouldReturnErrorActionResult_WhenModalStateIsNotValid()
		{
			Assert.AreEqual( "Error", ( (ContentResult)ArrangeAndAct_WhenModalStateIsNotValid() ).Content );
		}

		[Test]
		public void ShouldExecuteCommand_WhenModalStateIsValid()
		{
			var result = ArrangeAndAct_WhenModelStateIsValid();
			_commandExecutor
				.AssertWasCalled( x => x.ExecuteCommand( Arg<DummyExecuteCommand>.Is.Anything ) );
		}

	}
}
