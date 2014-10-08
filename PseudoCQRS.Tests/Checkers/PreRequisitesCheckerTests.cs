using System;
using NUnit.Framework;
using PseudoCQRS.Checkers;
using Rhino.Mocks;

namespace PseudoCQRS.Tests.Checkers
{
	[TestFixture]
	public class PreRequisitesCheckerTests
	{
		private ICheckersExecuter _checkersExecuter;
		private PrerequisitesChecker _prerequisitesChecker;

		[SetUp]
		public void Setup()
		{
			_checkersExecuter = MockRepository.GenerateMock<ICheckersExecuter>();
			_prerequisitesChecker = new PrerequisitesChecker( _checkersExecuter );
		}


		private CheckResult ArrangeAndAct(
			bool authorizationCheckContainsError,
			bool accessCheckContainsError,
			bool validationCheckContainsError
			)
		{
			var errorResult = new CheckResult()
			{
				ContainsError = true,
				Message = "Error"
			};

			var successResult = new CheckResult();

			_checkersExecuter
				.Stub( x => x.ExecuteAuthorizationCheckers( Arg<BlankSimpleTestCommand>.Is.Anything ) )
				.Return( authorizationCheckContainsError ? errorResult : successResult );

			_checkersExecuter
				.Stub( x => x.ExecuteAccessCheckers( Arg<BlankSimpleTestCommand>.Is.Anything ) )
				.Return( accessCheckContainsError ? errorResult : successResult );

			_checkersExecuter
				.Stub( x => x.ExecuteValidationCheckers( Arg<BlankSimpleTestCommand>.Is.Anything ) )
				.Return( validationCheckContainsError ? errorResult : successResult );

			return _prerequisitesChecker.Check( new BlankSimpleTestCommand() );
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
		public void ShouldFailWhenValidationCheckFails()
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