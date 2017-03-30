using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace PseudoCQRS.Controllers
{
	public abstract class BaseExecuteController<TViewModel, TCommand> : BaseCommandController<TViewModel, TCommand> where TViewModel : class
	{
		private readonly IMessageManager _messageManager;
		private readonly IReferrerProvider _referrerProvider;

		protected BaseExecuteController(
			ICommandExecutor commandExecutor,
			IMessageManager messageManager,
			IReferrerProvider referrerProvider )
			: base( commandExecutor )
		{
			_messageManager = messageManager;
			_referrerProvider = referrerProvider;
		}

		protected BaseExecuteController()
			: this(
				ServiceLocator.Current.GetInstance<ICommandExecutor>(),
				ServiceLocator.Current.GetInstance<IMessageManager>(),
				ServiceLocator.Current.GetInstance<IReferrerProvider>() ) {}

		public override ActionResult OnSuccessfulExecution( TViewModel viewModel, CommandResult commandResult )
		{
			return Redirect( _referrerProvider.GetAbsoluteUri() );
		}

		public override ActionResult OnFailureExecution( TViewModel viewModel, CommandResult commandResult )
		{
			return Redirect( _referrerProvider.GetAbsoluteUri() );
		}

		[HttpPost]
		public virtual ActionResult Execute( TViewModel viewModel )
		{
			if ( !ModelState.IsValid )
			{
				var errorMessage = GetErrorMessage();
				_messageManager.SetErrorMessage( errorMessage );
				return OnFailureExecution( viewModel, new CommandResult
				{
					ContainsError = true,
					Message = errorMessage
				} );
			}

			return ExecuteCommandAndGetActionResult( viewModel );			
		}

		protected virtual string GetErrorMessage()
		{
			return String.Join( "\r\n", ModelState
				                            .Where( x => x.Value.Errors.Count > 0 )
				                            .Select( x => String.Join( "\r\n", x.Value.Errors.Select( y => String.IsNullOrEmpty( y.ErrorMessage ) ? y.Exception.ToString() : y.ErrorMessage ) ) ) );
		}
	}
}