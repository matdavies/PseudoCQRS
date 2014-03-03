using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using PseudoCQRS.Controllers.ExtensionMethods;

namespace PseudoCQRS.Controllers
{
	[DbSessionManager]
	public abstract class BaseReadController<TViewModel, TArgs> : Controller, IViewPath
		where TViewModel : class
		where TArgs : new()
	{
		private readonly IViewModelFactory<TViewModel, TArgs> _viewModelFactory;

		public abstract string ViewPath { get; }

		protected BaseReadController( IViewModelFactory<TViewModel, TArgs> viewModelFactory )
		{
			_viewModelFactory = viewModelFactory;
		}

		public BaseReadController()
			: this( ServiceLocator.Current.GetInstance<IViewModelFactory<TViewModel, TArgs>>() ) {}

		public virtual ActionResult Execute()
		{
			return GetActionResult( _viewModelFactory.GetViewModel() );
		}

		protected virtual ViewResult GetViewResult( TViewModel viewModel )
		{
			return View( this.GetView(), viewModel );
		}

		protected virtual ActionResult GetActionResult( TViewModel viewModel )
		{
			return GetViewResult( viewModel );
		}
	}
}