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
		private readonly IPreRequisitesChecker _preRequisitesChecker;

		public ViewModelFactory(
			IViewModelProvider<TViewModel, TArg> viewModelProvider,
			IViewModelProviderArgumentsProvider argumentsProvider,
			IPreRequisitesChecker preRequisitesChecker )
		{
			_viewModelProvider = viewModelProvider;
			_argumentsProvider = argumentsProvider;
			_preRequisitesChecker = preRequisitesChecker;
		}

		public TViewModel GetViewModel()
		{
			var args = _argumentsProvider.GetArguments<TArg>();
			var checkResult = _preRequisitesChecker.Check( args );
			if ( checkResult.ContainsError )
				throw new ArgumentException( checkResult.Message );

			return _viewModelProvider.GetViewModel( args );
		}
	}
}
