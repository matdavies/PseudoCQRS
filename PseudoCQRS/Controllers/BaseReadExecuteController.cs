using System.Web.Mvc;
using PseudoCQRS.Controllers.ExtensionMethods;

namespace PseudoCQRS.Controllers
{
	public abstract class BaseReadExecuteController<TViewModel, TArgs, TCommand> : BaseCommandController<TViewModel, TCommand>, IViewPath
		where TViewModel : class
		where TArgs : new()
	{
		private readonly IViewModelFactory<TViewModel, TArgs> _viewModelFactory;

		public abstract string ViewPath { get; }

		protected BaseReadExecuteController(
			ICommandExecutor commandExecutor,
			IViewModelFactory<TViewModel, TArgs> viewModelFactory )
			: base( commandExecutor )
		{
			_viewModelFactory = viewModelFactory;
		}

		[HttpGet]
		public virtual ActionResult Execute()
		{
			return View( this.GetView(), _viewModelFactory.GetViewModel() );
		}

		[HttpPost]
		public virtual ActionResult Execute( FormCollection form )
		{
			var viewModel = _viewModelFactory.GetViewModel();
			return TryUpdateModel( viewModel ) ? ExecuteCommand( viewModel ) : View( this.GetView(), viewModel );
		}

		public override ActionResult OnFailureExecution( TViewModel viewModel )
		{
			return View( this.GetView(), viewModel );
		}
	}
}
