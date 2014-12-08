using System;
using System.Collections.Generic;
using System.Linq;
using PseudoCQRS.Examples.NerdDinner.Entities;
using PseudoCQRS.Examples.NerdDinner.Infrastructure;

namespace PseudoCQRS.Examples.NerdDinner.Modules.DinnerList
{
	public class DinnerListViewModelProvider : IViewModelProvider<DinnerListViewModel, DinnerListArguments>
	{
		private readonly IRepository _repository;

		public DinnerListViewModelProvider( IRepository repository )
		{
			_repository = repository;
		}

		public DinnerListViewModel GetViewModel( DinnerListArguments arguments )
		{
			return new DinnerListViewModel
			{
				Dinners = GetDinners( arguments ),
				Hosts = GetHosts(),
				HostedByUserId = arguments.HostedByUserId
			};
		}

		private Dictionary<int, string> GetHosts()
		{
			return _repository.GetAll<User>().ToDictionary( x => x.Id, x => x.Name );
		}

		private List<DinnerViewModel> GetDinners( DinnerListArguments arguments )
		{
			return _repository
				.GetAll<Dinner>()
				.Where( dinner => DinnerShouldBeIncluded( arguments, dinner ) )
				.Select( dinner => new DinnerViewModel()
				{
					Id = dinner.Id,
					Title = dinner.Title,
					DateTime = dinner.EventDate.ToString( "dd/MM/yyyy" ),
					HostedBy = dinner.HostedBy.Name
				} ).ToList();
		}

		private static bool DinnerShouldBeIncluded( DinnerListArguments arguments, Dinner dinner )
		{
			return
				dinner.EventDate >= DateTime.Now.Date
				&&
				(
					arguments.HostedByUserId == 0
					||
					dinner.HostedBy.Id == arguments.HostedByUserId
					);
		}
	}
}