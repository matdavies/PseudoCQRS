using System;
using System.Collections.Generic;
using System.Linq;
using PseudoCQRS.Examples.NerdDinner.Entities;
using PseudoCQRS.Examples.NerdDinner.Infrastructure;

namespace PseudoCQRS.Examples.NerdDinner.Modules.Login
{
	public class LoginCommandHandler : ICommandHandler<LoginCommand>
	{
		private readonly IRepository _repository;

		public LoginCommandHandler( IRepository repository )
		{
			_repository = repository;
		}

		public CommandResult Handle( LoginCommand command )
		{
			var result = new CommandResult
			{
				ContainsError = true,
				Message = "invalid username/password"
			};

			var user = _repository.GetSingleOrDefault<User>( x => x.Username.ToLower().Equals( command.Username.ToLower() ) && x.Password.Equals( command.Password ) );
			if ( user != null )
				result = new CommandResult();

			return result;
		}
	}
}