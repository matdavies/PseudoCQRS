using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using PseudoCQRS.Controllers.ExtensionMethods;

namespace PseudoCQRS.Controllers
{
	public abstract class BaseReadExecuteController<TViewModel, TArgs, TCommand> : BaseCommandController<TViewModel, TCommand>, IViewPath
		where TViewModel : class
		where TArgs : new()
	{
		private readonly IViewModelFactory<TViewModel, TArgs> _viewModelFactory;

		public abstract string ViewPath { get; }

		public abstract ActionResult OnSuccessfulExecution( TViewModel viewModel, CommandResult commandResult );


		protected BaseReadExecuteController(
			ICommandExecutor commandExecutor,
			IViewModelFactory<TViewModel, TArgs> viewModelFactory )
			: base( commandExecutor )
		{
			_viewModelFactory = viewModelFactory;
		}

		public BaseReadExecuteController()
			: this(
				ServiceLocator.Current.GetInstance<ICommandExecutor>(),
				ServiceLocator.Current.GetInstance<IViewModelFactory<TViewModel, TArgs>>() ) {}


		protected virtual ActionResult GetActionResult( TViewModel viewModel )
		{
			var result = ExecuteCommand( viewModel );
			return result.ContainsError ? OnFailureExecution( viewModel ) : OnSuccessfulExecution( viewModel, result );
		}


		[AcceptVerbs( HttpVerbs.Get | HttpVerbs.Head )]
		public virtual ActionResult Execute()
		{
			return GetViewResult( GetViewModel() );
		}

	    protected virtual TViewModel GetViewModel()
	    {
	        return _viewModelFactory.GetViewModel();
	    }

	    [HttpPost]
		public virtual ActionResult Execute( FormCollection form )
		{
			var viewModel = GetViewModel();
			return TryUpdateModel( viewModel ) ? GetActionResult( viewModel ) : GetViewResult( viewModel );
		}

		protected virtual ViewResult GetViewResult( TViewModel viewModel )
		{
			return View( this.GetView(), viewModel );
		}

		public virtual ActionResult OnFailureExecution( TViewModel viewModel )
		{
			return View( this.GetView(), viewModel );
		}
	}
}