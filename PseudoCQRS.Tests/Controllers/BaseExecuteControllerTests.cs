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
		private IReferrerProvider _referrerProvider;
		private DummyExecuteController _controller;

		[SetUp]
		public void Setup()
		{
			_commandExecutor = MockRepository.GenerateMock<ICommandExecutor>();
			_messageManager = MockRepository.GenerateMock<IMessageManager>();
			_referrerProvider = MockRepository.GenerateMock<IReferrerProvider>();
			_controller = new DummyExecuteController( _commandExecutor, _messageManager, _referrerProvider );
		}

		private ActionResult ArrangeAndAct_WhenModelStateIsNotValid( string url )
		{
			_controller.ModelState.AddModelError( "TestError", "Error Message" );

			_referrerProvider
				.Stub( x => x.GetAbsoluteUri() )
				.Return( url );

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
					Message = "Success"
				} );

			_referrerProvider
				.Stub( x => x.GetAbsoluteUri() )
				.Return( "localhost" );

			return _controller.Execute( new DummyExecuteViewModel() );
		}

		[Test]
		public void ShouldSetValidationFailue_WhenModalStateIsNotValid()
		{
			ArrangeAndAct_WhenModelStateIsNotValid( "localhost" );
			_messageManager
				.AssertWasCalled( x => x.SetErrorMessage( Arg<string>.Is.Anything ) );
		}

		[Test]
		public void Execute_WhenViewModelInValid_ReturnsRedirectResult()
		{
			var actionResult = ArrangeAndAct_WhenModelStateIsNotValid( "localhost" );
			Assert.IsInstanceOf<RedirectResult>( actionResult );
			//Assert.AreEqual("Error Message", ((ContentResult)ArrangeAndAct_WhenModelStateIsNotValid()).Content);
		}

		[Test]
		public void ShouldExecuteCommand_WhenModalStateIsValid()
		{
			var result = ArrangeAndAct_WhenModelStateIsValid();
			_commandExecutor
				.AssertWasCalled( x => x.ExecuteCommand( Arg<DummyExecuteCommand>.Is.Anything ) );
		}

		[Test]
		public void Execute_WhenViewModelValid_ReturnsRedirectResult()
		{
			var actionResult = ArrangeAndAct_WhenModelStateIsValid();
			Assert.IsInstanceOf<RedirectResult>( actionResult );
		}

		[Test]
		public void HasSetAllDependencies()
		{
			var messageManager = MockRepository.GenerateMock<IMessageManager>();
			var commandExecutor = MockRepository.GenerateMock<ICommandExecutor>();
			var referrerProvider = MockRepository.GenerateMock<IReferrerProvider>();

			var locator = MockRepository.GenerateMock<IServiceLocator>();
			locator
				.Stub( x => x.GetInstance<ICommandExecutor>() )
				.Return( commandExecutor );
			locator
				.Stub( x => x.GetInstance<IMessageManager>() )
				.Return( messageManager );
			locator
				.Stub( x => x.GetInstance<IReferrerProvider>() )
				.Return( referrerProvider );

			ServiceLocator.SetLocatorProvider( () => locator );
			var controller = new DummyExecuteController();

			HasSetAllDependenciesControllerHelper.AssertFieldsAreNotNull( controller );
		}
	}
}