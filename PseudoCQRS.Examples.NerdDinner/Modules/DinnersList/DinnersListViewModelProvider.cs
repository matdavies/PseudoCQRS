using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PseudoCQRS.Examples.NerdDinner.Entities;
using PseudoCQRS.Examples.NerdDinner.Infrastructure;

namespace PseudoCQRS.Examples.NerdDinner.Modules.DinnersList
{
	public class DinnersListViewModelProvider : IViewModelProvider<DinnersListViewModel, EmptyViewModelProviderArgument>
	{
		private readonly IRepository _repository;

		public DinnersListViewModelProvider( IRepository repository )
		{
			_repository = repository;
		}

		public DinnersListViewModel GetViewModel( EmptyViewModelProviderArgument args )
		{
			return new DinnersListViewModel();
		}
	}
}