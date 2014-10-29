using System;
using System.Collections.Generic;
using System.Linq;
using PseudoCQRS.Examples.NerdDinner.Entities;
using PseudoCQRS.Examples.NerdDinner.Infrastructure;

namespace PseudoCQRS.Examples.NerdDinner.Modules.DinnerCreate
{
	public class DinnerCreateViewModelProvider : IViewModelProvider<DinnerCreateViewModel, EmptyViewModelProviderArgument>
	{
		private readonly IRepository _repository;

		public DinnerCreateViewModelProvider( IRepository repository )
		{
			_repository = repository;
		}

		public DinnerCreateViewModel GetViewModel( EmptyViewModelProviderArgument args )
		{
			return new DinnerCreateViewModel
			{
				EventDate = DateTime.Today.ToShortDateString(),
				Hosts = _repository.GetAll<User>().ToDictionary( x => x.Id, x => x.Name )
			};
		}
	}
}