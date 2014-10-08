using NUnit.Framework;
using PseudoCQRS.Controllers;
using PseudoCQRS.Tests.Controllers.Helpers;
using Rhino.Mocks;

namespace PseudoCQRS.Tests.Controllers
{
	[TestFixture]
	public class CommandExecutorTests
	{
		private ICommandBus _commandBus;
		private IMessageManager _messageManager;
		private CommandExecutor _commandExecutor;

		[SetUp]
		public void Setup()
		{
			_commandBus = MockRepository.GenerateMock<ICommandBus>();
			_messageManager = MockRepository.GenerateMock<IMessageManager>();

			_commandExecutor = new CommandExecutor( _commandBus, _messageManager );
		}

		[Test]
		public void ErrorMessageSetWhenCommandResultHasError()
		{
			_commandBus.Stub(
			                 x => x.Execute( Arg<DummyExecuteCommand>.Is.Anything ) )
			           .Return( new CommandResult()
			           {
				           ContainsError = true,
				           Message = "Error"
			           } );

			_commandExecutor.ExecuteCommand( new DummyExecuteCommand() );

			_messageManager.AssertWasCalled( x => x.SetErrorMessage( "Error" ) );
		}


		[Test]
		public void ShouldSetSuccessMessageInMessageManager_WhenNoErrorReturnedFromCommandBus()
		{
			_commandBus.Stub(
			                 x => x.Execute( Arg<DummyExecuteCommand>.Is.Anything ) )
			           .Return( new CommandResult()
			           {
				           ContainsError = false,
				           Message = "Success"
			           } );

			_commandExecutor.ExecuteCommand( new DummyExecuteCommand() );

			_messageManager.AssertWasCalled( x => x.SetSuccessMessage( "Success" ) );
		}
	}
}