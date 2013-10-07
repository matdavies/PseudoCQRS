using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PseudoCQRS.Checkers;
using PseudoCQRS.Examples.NerdDinner.Infrastructure;

namespace PseudoCQRS.Examples.NerdDinner.Modules
{
	public class AdminAuthorizationChecker : IAuthorizationChecker
	{
		private readonly IRepository _repository;
	
		public AdminAuthorizationChecker( IRepository repository )
		{
			_repository = repository;
		}

		public CommandResult Check()
		{
			throw new NotImplementedException();
		}
	}
}