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


		private string ArrangeAndAct(
			bool authorizationCheckContainsError,
			bool accessCheckContainsError,
			bool validationCheckContainsError
			)
		{
			_checkersExecuter
				.Stub( x => x.ExecuteAuthorizationCheckers( Arg<BlankSimpleTestCommand>.Is.Anything ) )
				.Return( authorizationCheckContainsError ? "Error" : String.Empty );

			_checkersExecuter
				.Stub( x => x.ExecuteAccessCheckers( Arg<BlankSimpleTestCommand>.Is.Anything ) )
				.Return( accessCheckContainsError ? "Error" : String.Empty );

			_checkersExecuter
				.Stub( x => x.ExecuteValidationCheckers( Arg<BlankSimpleTestCommand>.Is.Anything ) )
				.Return( validationCheckContainsError ? "Error" : String.Empty );

			return _prerequisitesChecker.Check( new BlankSimpleTestCommand() );

		}

		[Test]
		public void ShouldFailWhenAuthorizationCheckFails()
		{
			var result = ArrangeAndAct( true, false, false );
			Assert.IsNotNullOrEmpty( result );
		}

		[Test]
		public void ShouldFailWhenAccessCheckFails()
		{
			var result = ArrangeAndAct( false, true, false );
			Assert.IsNotNullOrEmpty( result );
		}

		[Test]
		public void ShouldFaileWhenValidationCheckFails()
		{
			var result = ArrangeAndAct( false, false, true );
			Assert.IsNotNullOrEmpty( result );
		}

		[Test]
		public void ShouldBeSuccessfulWhenAllChecksPass()
		{
			var result = ArrangeAndAct( false, false, false );
			Assert.IsNullOrEmpty( result );
		}
	}
}
