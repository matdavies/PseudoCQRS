using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PseudoCQRS.Examples.NerdDinner.Entities;
using PseudoCQRS.Examples.NerdDinner.Infrastructure;

namespace PseudoCQRS.Examples.NerdDinner.Modules.DinnerCreate
{
	public class DinnerCreateCommandHandler : ICommandHandler<DinnerCreateCommand>
	{
		private readonly IRepository _repository;

		public DinnerCreateCommandHandler( IRepository repository )
		{
			_repository = repository;
		}

		public CommandResult Handle( DinnerCreateCommand command )
		{
			var host = _repository.Get<User>( command.HostedById );
			var dinner = new Dinner( command.Title, command.EventDate, command.Description, host );
			_repository.Save( dinner );
			return new DinnerCreateCommandResult
			{
				Id = dinner.Id,
				ContainsError = false,
				Message = "dinner " + command.Title + " created"
			};
		}
	}
}