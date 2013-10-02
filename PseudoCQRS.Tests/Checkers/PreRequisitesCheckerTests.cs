using NUnit.Framework;
using PseudoCQRS.Checkers;
using Rhino.Mocks;

namespace PseudoCQRS.Tests.Checkers
{
	[TestFixture]
	public class PreRequisitesCheckerTests
	{
		private ICheckersExecuter _checkersExecuter;
		private PreRequisitesChecker _preRequisitesChecker;

		[SetUp]
		public void Setup()
		{
			_checkersExecuter = MockRepository.GenerateMock<ICheckersExecuter>();
			_preRequisitesChecker = new PreRequisitesChecker( _checkersExecuter );
		}


		private CommandResult ArrangeAndAct(
			bool authorizationCheckContainsError,
			bool accessCheckContainsError,
			bool validationCheckContainsError
			)
		{
			_checkersExecuter
				.Stub( x => x.ExecuteAuthorizationCheckers( Arg<BlankSimpleTestCommand>.Is.Anything ) )
				.Return( new CommandResult
				{
					ContainsError = authorizationCheckContainsError
				} );

			_checkersExecuter
				.Stub( x => x.ExecuteAccessCheckers( Arg<BlankSimpleTestCommand>.Is.Anything ) )
				.Return( new CommandResult
				{
					ContainsError = accessCheckContainsError
				} );

			_checkersExecuter
				.Stub( x => x.ExecuteValidaitonCheckers( Arg<BlankSimpleTestCommand>.Is.Anything ) )
				.Return( new CommandResult
				{
					ContainsError = validationCheckContainsError
				} );

			return _preRequisitesChecker.Check( new BlankSimpleTestCommand() );

		}

		[Test]
		public void ShouldFailWhenAuthorizationCheckFails()
		{
			var result = ArrangeAndAct( true, false, false );
			Assert.IsTrue( result.ContainsError );
		}

		[Test]
		public void ShouldFailWhenAccessCheckFails()
		{
			var result = ArrangeAndAct( false, true, false );
			Assert.IsTrue( result.ContainsError );
		}

		[Test]
		public void ShouldFaileWhenValidationCheckFails()
		{
			var result = ArrangeAndAct( false, false, true );
			Assert.IsTrue( result.ContainsError );
		}

		[Test]
		public void ShouldBeSuccessfulWhenAllChecksPass()
		{
			var result = ArrangeAndAct( false, false, false );
			Assert.IsFalse( result.ContainsError );
		}
	}
}
