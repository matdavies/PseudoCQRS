using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PseudoCQRS.Controllers;

namespace PseudoCQRS.Examples.NerdDinner.Modules.Login
{
	public class LoginController : BaseReadExecuteController<LoginViewModel, EmptyViewModelProviderArgument, LoginCommand>
	{
		public LoginController( ICommandExecutor commandExecutor, IViewModelFactory<LoginViewModel, EmptyViewModelProviderArgument> viewModelFactory ) 
			: base( commandExecutor, viewModelFactory ) {}

		public override ActionResult OnSuccessfulExecution( LoginViewModel viewModel, CommandResult commandResult )
		{
			return RedirectToAction( "Index", "Home" );
		}

		public override string ViewPath
		{
			get { return "Account/Login"; }
		}
	}
}