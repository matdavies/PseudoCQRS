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

		public CommandResult Handle( DinnerCreateCommand cmd )
		{
			var host = _repository.Get<User>( cmd.HostedById );
			var dinner = new Dinner( cmd.Title, cmd.EventDate, cmd.Description, host );
			_repository.Save( dinner );
			return new DinnerCreateCommandResult
			{
				Id = dinner.Id,
				ContainsError = false,
				Message = "dinner " + cmd.Title + " created"
			};
		}
	}
}