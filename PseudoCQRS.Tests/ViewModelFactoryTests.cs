using System;
using NUnit.Framework;
using PseudoCQRS.Checkers;
using Rhino.Mocks;

namespace PseudoCQRS.Tests
{
	[TestFixture]
	public class ViewModelFactoryTests
	{
		public class TestViewModel {}

		public class TestViewModelProviderArgument {}

		private IViewModelProvider<TestViewModel, TestViewModelProviderArgument> _viewModelProvider;
		private IViewModelProviderArgumentsProvider _viewModelProviderArgumentsProvider;
		private IPrerequisitesChecker _prerequisitesChecker;
		private ViewModelFactory<TestViewModel, TestViewModelProviderArgument> _viewModelFactory;

		[SetUp]
		public void Setup()
		{
			_viewModelProvider = MockRepository.GenerateMock<IViewModelProvider<TestViewModel, TestViewModelProviderArgument>>();
			_viewModelProviderArgumentsProvider = MockRepository.GenerateMock<IViewModelProviderArgumentsProvider>();
			_prerequisitesChecker = MockRepository.GenerateMock<IPrerequisitesChecker>();
			_viewModelFactory = new ViewModelFactory<TestViewModel, TestViewModelProviderArgument>(
				_viewModelProvider,
				_viewModelProviderArgumentsProvider,
				_prerequisitesChecker );
		}

		[Test]
		public void ShouldThrowExceptionWhenPreRequisiteCheckContainsError()
		{
			_prerequisitesChecker
				.Stub( x => x.Check( Arg<TestViewModelProviderArgument>.Is.Anything ) )
				.Return( new CheckResult
				{
					ContainsError = true,
					Message = "Error"
				} );

			Assert.Throws<ArgumentException>( () => _viewModelFactory.GetViewModel() );
		}

		[Test]
		public void ShouldReturnViewModelWhenPreRequisiteCheckReturnsSuccess()
		{
			_prerequisitesChecker
				.Stub( x => x.Check( Arg<TestViewModelProviderArgument>.Is.Anything ) )
				.Return( new CheckResult() );

			_viewModelProvider
				.Stub( x => x.GetViewModel( Arg<TestViewModelProviderArgument>.Is.Anything ) )
				.Return( new TestViewModel() );

			var viewModel = _viewModelFactory.GetViewModel();

			Assert.IsNotNull( viewModel );
		}
	}
}