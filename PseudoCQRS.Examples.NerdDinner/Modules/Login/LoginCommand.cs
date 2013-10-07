using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PseudoCQRS.Examples.NerdDinner.Modules.Login
{
	public class LoginCommand
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}