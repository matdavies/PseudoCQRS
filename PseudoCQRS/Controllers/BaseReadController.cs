using System.Web.Mvc;
using PseudoCQRS.Controllers.ExtensionMethods;

namespace PseudoCQRS.Controllers
{
	public abstract class BaseReadController<TViewModel, TArgs> : Controller, IViewPath
		where TViewModel : class 
		where TArgs : new()
	{
		private readonly IViewModelFactory<TViewModel, TArgs> _viewModelFactory;

		public abstract string ViewPath { get; }

		protected BaseReadController( IViewModelFactory<TViewModel, TArgs> viewModelFactory)
		{
			_viewModelFactory = viewModelFactory;
		}

		public virtual ActionResult Execute()
		{
			return View( this.GetView(), _viewModelFactory.GetViewModel() );
		}

	}
}
