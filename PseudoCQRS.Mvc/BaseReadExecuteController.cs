using System;
using System.Net;
using System.Web;
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

		public override ActionResult OnFailureExecution( TViewModel viewModel, CommandResult commandResult )
		{
			return View( this.GetView(), viewModel );
		}


		protected BaseReadExecuteController(
			ICommandExecutor commandExecutor,
			IViewModelFactory<TViewModel, TArgs> viewModelFactory )
			: base( commandExecutor )
		{
			_viewModelFactory = viewModelFactory;
		}

		protected BaseReadExecuteController()
			: this(
				ServiceLocator.Current.GetInstance<ICommandExecutor>(),
				ServiceLocator.Current.GetInstance<IViewModelFactory<TViewModel, TArgs>>() ) {}


		protected virtual ActionResult GetActionResult( TViewModel viewModel )
		{
			return ExecuteCommandAndGetActionResult( viewModel );
		}


		[AcceptVerbs( HttpVerbs.Get | HttpVerbs.Head )]
		public virtual ActionResult Execute()
		{
			return GetViewResult( GetViewModel() );
		}

		protected virtual TViewModel GetViewModel()
		{
			try
			{
				return _viewModelFactory.GetViewModel();
			}
			catch ( ArgumentBindingException exception )
			{
				throw new HttpException( 404, exception.Message, exception.InnerException );
			}
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
	}
}