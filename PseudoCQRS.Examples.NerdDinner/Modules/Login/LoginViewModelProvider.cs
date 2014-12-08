using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PseudoCQRS.Examples.NerdDinner.Modules.Login
{
	public class LoginViewModelProvider : IViewModelProvider<LoginViewModel, EmptyViewModelProviderArgument>
	{
		public LoginViewModel GetViewModel( EmptyViewModelProviderArgument args )
		{
			return new LoginViewModel();
		}
	}
}