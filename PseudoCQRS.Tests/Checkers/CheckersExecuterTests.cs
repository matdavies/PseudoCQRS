using System;
using System.Collections.Generic;
using NUnit.Framework;
using PseudoCQRS.Checkers;
using Rhino.Mocks;

namespace PseudoCQRS.Tests.Checkers
{
	[TestFixture]
	public class CheckersExecuterTests
	{
		private ICheckersFinder _checkersFinder;
		private CheckersExecuter _checkersExecuter;

		[SetUp]
		public void Setup()
		{
			_checkersFinder = MockRepository.GenerateMock<ICheckersFinder>();
			_checkersExecuter = new CheckersExecuter( _checkersFinder );
		}

		private string ExecuteAuthorizationCheckers_ArrangeAndAct( bool containsErrror )
		{
			var mockedAuthorizationChecker = MockRepository.GenerateMock<IAuthorizationChecker>();
			mockedAuthorizationChecker
				.Stub( x => x.Check() )
				.Return( new CheckResult
				{
					Message = containsErrror ? "Error" : String.Empty,
					ContainsError = containsErrror
				} );

			_checkersFinder
				.Stub( x => x.FindAuthorizationCheckers( Arg<BlankSimpleTestCommand>.Is.Anything ) )
				.Return( new List<IAuthorizationChecker> { mockedAuthorizationChecker } );

			return _checkersExecuter.ExecuteAuthorizationCheckers( Arg<BlankSimpleTestCommand>.Is.Anything );
		}

		[Test]
		public void ExecuteAuthorizationCheckers_ShouldFailWhenCheckerFails()
		{
			var result = ExecuteAuthorizationCheckers_ArrangeAndAct( true );
			Assert.IsNotNullOrEmpty( result );
		}

		[Test]
		public void ExecuteAuthorizationChecker_ShouldPassWhenAllCheckersPass()
		{
			var result = ExecuteAuthorizationCheckers_ArrangeAndAct( false );
			Assert.IsNullOrEmpty( result );
		}


		private string ExecuteAccessCheckers_ArrangeAndAct( bool containsError )
		{
			var mockedAccessChecker = MockRepository.GenerateMock<IAccessChecker>();
			mockedAccessChecker
				.Stub( x => x.Check( Arg<String>.Is.Anything, Arg<object>.Is.Anything ) )
				.Return( new CheckResult
				{
					Message = containsError ? "Error" : String.Empty,
					ContainsError = containsError
				} );
			_checkersFinder
				.Stub( x => x.FindAccessCheckers( Arg<BlankSimpleTestCommand>.Is.Anything ) )
				.Return( new List<AccessCheckerAttributeDetails>
				{
					new AccessCheckerAttributeDetails
					{
						AccessChecker = mockedAccessChecker,
						PropertyName = "TaskId"
					}
				} );

			return _checkersExecuter.ExecuteAccessCheckers( Arg<BlankSimpleTestCommand>.Is.Anything );
		}

		[Test]
		public void ExecuteAccessCheckers_ShouldFailWhenCheckerFails()
		{
			var result = ExecuteAccessCheckers_ArrangeAndAct( true );
			Assert.IsNotNullOrEmpty( result );
		}

		[Test]
		public void ExecuteAccessCheckers_ShouldPassWhenCheckerPass()
		{
			var result = ExecuteAccessCheckers_ArrangeAndAct( false );
			Assert.IsNullOrEmpty( result );
		}

		private string ExecuteValidationCheckers_ArrangeAndAct( bool containsError )
		{
			var mockedValidationChecker = MockRepository.GenerateMock<IValidationChecker<BlankSimpleTestCommand>>();
			mockedValidationChecker
				.Stub( x => x.Check( Arg<BlankSimpleTestCommand>.Is.Anything ) )
				.Return( new CheckResult
				{
					Message = containsError ? "Error" : String.Empty,
					ContainsError = containsError
				} );
			_checkersFinder
				.Stub( x => x.FindValidationCheckers( Arg<BlankSimpleTestCommand>.Is.Anything ) )
				.Return( new List<IValidationChecker<BlankSimpleTestCommand>>
				{
					mockedValidationChecker
				} );

			return _checkersExecuter.ExecuteValidationCheckers( new BlankSimpleTestCommand() );
		}

		[Test]
		public void ExecuteValidationCheckers_ShouldFailWhenCheckerFails()
		{
			var result = ExecuteValidationCheckers_ArrangeAndAct( true );
			Assert.IsNotNullOrEmpty( result );
		}

		[Test]
		public void ExecuteValidationCheckers_ShouldPassWhenCheckerPass()
		{
			var result = ExecuteValidationCheckers_ArrangeAndAct( false );
			Assert.IsNullOrEmpty( result );
		}
	}
}
