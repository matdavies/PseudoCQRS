using System;
using PseudoCQRS.Checkers;

namespace PseudoCQRS
{
	public class ViewModelFactory<TViewModel, TArg> : IViewModelFactory<TViewModel, TArg>
		where TViewModel : class
		where TArg : new()
	{
		private readonly IViewModelProvider<TViewModel, TArg> _viewModelProvider;
		private readonly IViewModelProviderArgumentsProvider _argumentsProvider;
		private readonly IPrerequisitesChecker _prerequisitesChecker;

		public ViewModelFactory(
			IViewModelProvider<TViewModel, TArg> viewModelProvider,
			IViewModelProviderArgumentsProvider argumentsProvider,
			IPrerequisitesChecker prerequisitesChecker )
		{
			_viewModelProvider = viewModelProvider;
			_argumentsProvider = argumentsProvider;
			_prerequisitesChecker = prerequisitesChecker;
		}

		public TViewModel GetViewModel()
		{
			var args = _argumentsProvider.GetArguments<TArg>();
			var checkResult = _prerequisitesChecker.Check( args );
			if ( checkResult.ContainsError )
				throw new ArgumentException( checkResult.Message );

			return _viewModelProvider.GetViewModel( args );
		}
	}
}