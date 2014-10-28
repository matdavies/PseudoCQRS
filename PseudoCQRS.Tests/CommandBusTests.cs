using System;
using NUnit.Framework;
using PseudoCQRS.Checkers;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

namespace PseudoCQRS.Tests
{
	[TestFixture]
	public class CommandBusTests
	{
		private ICommandHandlerProvider _commandHandlerProvider;
		private IDbSessionManager _dbSessionManager;
		private IPrerequisitesChecker _prerequisitesChecker;
		private CommandBus _bus;

		[SetUp]
		public void Setup()
		{
			_commandHandlerProvider = MockRepository.GenerateMock<ICommandHandlerProvider>();
			_dbSessionManager = MockRepository.GenerateMock<IDbSessionManager>();
			_prerequisitesChecker = MockRepository.GenerateMock<IPrerequisitesChecker>();

			_bus = new CommandBus( _commandHandlerProvider, _dbSessionManager, _prerequisitesChecker );
		}

		private CommandResult ExecuteArrangeAndAct(
			ICommandHandler<BlankSimpleTestCommand> getCommandHandlerRetVal = null,
			string commandPreHandleResult = "" )
		{
			_commandHandlerProvider
				.Stub( x => x.GetCommandHandler<BlankSimpleTestCommand>() )
				.Return( getCommandHandlerRetVal );

			_prerequisitesChecker
				.Stub( x => x.Check( Arg<BlankSimpleTestCommand>.Is.Anything ) )
				.Return( String.IsNullOrEmpty( commandPreHandleResult ) ? new CheckResult() : new CheckResult()
				{
					ContainsError = true,
					Message = commandPreHandleResult
				} );

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
			ExecuteArrangeAndAct( handler, "Error" );

			handler.AssertWasNotCalled( x => x.Handle( Arg<BlankSimpleTestCommand>.Is.Anything ) );
		}

		[Test]
		public void ShouldReturnFalseWhenPreHandlerReturnsFalse()
		{
			var handler = MockRepository.GenerateMock<ICommandHandler<BlankSimpleTestCommand>>();
			var result = ExecuteArrangeAndAct( handler, "Error" );

			Assert.IsTrue( result.ContainsError );
		}

		[DbTransaction]
		internal class CommandHandlerWithTransactionAttribute : ICommandHandler<BlankSimpleTestCommand>
		{
			public CommandResult Handle( BlankSimpleTestCommand command )
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

		[Test]
		public void Execute_HasTransactionAttributeAndOnError_RollbackTransaction()
		{
			_commandHandlerProvider
			.Stub( x => x.GetCommandHandler<BlankSimpleTestCommand>() )
			.Return( new CommandHandlerWithTransactionAttribute() );

			_prerequisitesChecker
				.Stub( x => x.Check( Arg<BlankSimpleTestCommand>.Is.Anything ) )
				.Throw( new Exception( "Test Exception" ) );

			try
			{
				_bus.Execute( new BlankSimpleTestCommand() );
			}
			catch ( Exception )
			{				
			}
			_dbSessionManager.AssertWasCalled( x => x.RollbackTransaction() );


		}
	}
}