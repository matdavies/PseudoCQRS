using System;
using NUnit.Framework;
using PseudoCQRS.Checkers;
using Rhino.Mocks;

namespace PseudoCQRS.Tests
{
	[TestFixture]
	public class ViewModelFactoryTests
	{
		public class TestViewModel { }

		public class TestViewModelProviderArgument { }

		private IViewModelProvider<TestViewModel, TestViewModelProviderArgument> _viewModelProvider;
		private IViewModelProviderArgumentsProvider _viewModelProviderArgumentsProvider;
		private IPreRequisitesChecker _preRequisitesChecker;
		private ViewModelFactory<TestViewModel, TestViewModelProviderArgument> _viewModelFactory;

		[SetUp]
		public void Setup()
		{
			_viewModelProvider = MockRepository.GenerateMock<IViewModelProvider<TestViewModel, TestViewModelProviderArgument>>();
			_viewModelProviderArgumentsProvider = MockRepository.GenerateMock<IViewModelProviderArgumentsProvider>();
			_preRequisitesChecker = MockRepository.GenerateMock<IPreRequisitesChecker>();
			_viewModelFactory = new ViewModelFactory<TestViewModel, TestViewModelProviderArgument>(
				_viewModelProvider,
				_viewModelProviderArgumentsProvider,
				_preRequisitesChecker );
		}

		[Test]
		public void ShouldThrowExceptionWhenPreRequisiteCheckContainsError()
		{
			_preRequisitesChecker
				.Stub( x => x.Check( Arg<TestViewModelProviderArgument>.Is.Anything ) )
				.Return( new CommandResult
				{
					ContainsError = true
				} );

			Assert.Throws<ArgumentException>( () => _viewModelFactory.GetViewModel() );
		}

		[Test]
		public void ShouldReturnViewModelWhenPreRequisiteCheckReturnsSuccess()
		{
			_preRequisitesChecker
				.Stub( x => x.Check( Arg<TestViewModelProviderArgument>.Is.Anything ) )
				.Return( new CommandResult() );

			_viewModelProvider
				.Stub( x => x.GetViewModel( Arg<TestViewModelProviderArgument>.Is.Anything ) )
				.Return( new TestViewModel() );

			var viewModel = _viewModelFactory.GetViewModel();

			Assert.IsNotNull( viewModel );

		}
	}
}
