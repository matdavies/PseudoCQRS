using System;
using System.Linq;
using System.Web.Mvc;

namespace PseudoCQRS.Controllers
{
	public abstract class BaseExecuteController<TViewModel, TCommand> : BaseCommandController<TViewModel, TCommand>
		where TViewModel : class
	{
		private readonly IMessageManager _messageManager;

		protected BaseExecuteController( 
			ICommandExecutor commandExecutor, 
			IMessageManager messageManager )
			: base( commandExecutor )
		{
			_messageManager = messageManager;
		}

		[HttpPost]
		public virtual ActionResult Execute( TViewModel viewModel )
		{
			ActionResult result;
			if ( ModelState.IsValid )
				result = ExecuteCommand( viewModel );
			else
			{
				var errorMessage = String.Join( "\r\n", ModelState
					.Where( x => x.Value.Errors.Count > 0 )
					.Select( x => String.Join( "\r\n", x.Value.Errors.Select( y => String.IsNullOrEmpty( y.ErrorMessage ) ? y.Exception.ToString() : y.ErrorMessage ) ) ) );
				_messageManager.SetErrorMessage( errorMessage );
				result = OnFailureExecution( viewModel );
			}
			return result;
		}

	}
}
