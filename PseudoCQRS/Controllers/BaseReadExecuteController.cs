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

        public abstract ActionResult OnSuccessfulExecution( TViewModel viewModel, CommandResult cmdResult );


        protected BaseReadExecuteController(
            ICommandExecutor commandExecutor,
            IViewModelFactory<TViewModel, TArgs> viewModelFactory )
            : base( commandExecutor )
        {
            _viewModelFactory = viewModelFactory;
        }

        public BaseReadExecuteController()
        {
            _viewModelFactory = ServiceLocator.Current.GetInstance<IViewModelFactory<TViewModel, TArgs>>();
        }


        private ActionResult GetActionResult( TViewModel viewModel )
        {
            var result = ExecuteCommand( viewModel );
            return result.ContainsError ? OnFailureExecution( viewModel ) : OnSuccessfulExecution( viewModel, result );
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
            return TryUpdateModel( viewModel ) ? GetActionResult( viewModel ) : View( this.GetView(), viewModel );
        }

        public virtual ActionResult OnFailureExecution( TViewModel viewModel )
        {
            return View( this.GetView(), viewModel );
        }
    }
}