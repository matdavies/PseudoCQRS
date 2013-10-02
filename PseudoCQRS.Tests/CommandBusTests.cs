using NUnit.Framework;
using PseudoCQRS.Checkers;
using Rhino.Mocks;

namespace PseudoCQRS.Tests
{
	[TestFixture]
	public class CommandBusTests
	{
		private ICommandHandlerProvider _commandHandlerProvider;
		private IDbSessionManager _dbSessionManager;
		private IPreRequisitesChecker _preRequisitesChecker;
		private CommandBus _bus;

		[SetUp]
		public void Setup()
		{
			_commandHandlerProvider = MockRepository.GenerateMock<ICommandHandlerProvider>();
			_dbSessionManager = MockRepository.GenerateMock<IDbSessionManager>();
			_preRequisitesChecker = MockRepository.GenerateMock<IPreRequisitesChecker>();

			_bus = new CommandBus( _commandHandlerProvider, _dbSessionManager, _preRequisitesChecker );
		}

		private CommandResult ExecuteArrangeAndAct( 
			ICommandHandler<BlankSimpleTestCommand> getCommandHandlerRetVal = null,
			CommandResult commandPreHandleResult = null )
		{
			_commandHandlerProvider
				.Stub( x => x.GetCommandHandler<BlankSimpleTestCommand>() )
				.Return( getCommandHandlerRetVal );

			_preRequisitesChecker
				.Stub( x => x.Check( Arg<BlankSimpleTestCommand>.Is.Anything ) )
				.Return( commandPreHandleResult ?? new CommandResult() );

			return _bus.Execute( new BlankSimpleTestCommand() );
		}


		[Test]
		public void ShouldReturnFalseWhenHandlerNotFound()
		{
			var result = ExecuteArrangeAndAct();

			Assert.AreEqual( true, result.ContainsError );
			Assert.IsNotNullOrEmpty( result.Message );
		}


		[Test]
		public void ShouldCallHandlerWhenNotNull()
		{
			var handler = MockRepository.GenerateMock<ICommandHandler<BlankSimpleTestCommand>>();
			ExecuteArrangeAndAct( handler );

			handler.AssertWasCalled( x => x.Handle( Arg<BlankSimpleTestCommand>.Is.Anything ) );
		}

		[Test]
		public void ShouldNotCallHandler_WhenPreHandlerReturnsFalse()
		{
			var handler = MockRepository.GenerateMock<ICommandHandler<BlankSimpleTestCommand>>();
			ExecuteArrangeAndAct( handler, new CommandResult
			{
				ContainsError = true
			} );

			handler.AssertWasNotCalled( x => x.Handle( Arg<BlankSimpleTestCommand>.Is.Anything ) );
		}

		[Test]
		public void ShouldReturnFalseWhenPreHandlerReturnsFalse()
		{
			var handler = MockRepository.GenerateMock<ICommandHandler<BlankSimpleTestCommand>>();
			var result = ExecuteArrangeAndAct( handler, new CommandResult
			{
				ContainsError = true
			} );

			Assert.IsTrue( result.ContainsError );
			
		}

		[DbTransaction]
		internal class CommandHandlerWithTransactionAttribute : ICommandHandler<BlankSimpleTestCommand>
		{
			public CommandResult Handle( BlankSimpleTestCommand cmd )
			{
				return new CommandResult();
			}
		}

		[Test]
		public void ShouldOpenTransactionWhenDbTransactionAttributeIsApplied()
		{
			ExecuteArrangeAndAct( new CommandHandlerWithTransactionAttribute() );
			_dbSessionManager.AssertWasCalled( x => x.OpenTransaction() );
		}

		[Test]
		public void ShouldCommitTransactionWhenDbTransactionAttributeIsApplied()
		{
			ExecuteArrangeAndAct( new CommandHandlerWithTransactionAttribute() );
			_dbSessionManager.AssertWasCalled( x => x.CommitTransaction() );			
		}



	}
}
