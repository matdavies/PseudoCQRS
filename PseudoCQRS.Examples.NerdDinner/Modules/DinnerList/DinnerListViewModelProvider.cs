using System;
using System.Collections.Generic;
using System.Linq;
using PseudoCQRS.Examples.NerdDinner.Entities;
using PseudoCQRS.Examples.NerdDinner.Infrastructure;

namespace PseudoCQRS.Examples.NerdDinner.Modules.DinnerList
{
    public class DinnerListViewModelProvider : IViewModelProvider<List<DinnerListViewModel>, DinnerListArguments>
    {

	    private readonly IRepository _repository;

	    public DinnerListViewModelProvider( IRepository repository )
	    {
		    _repository = repository;
	    }

	    public List<DinnerListViewModel> GetViewModel( DinnerListArguments args )
	    {
		    return _repository.GetAll<Dinner>().Select( x => new DinnerListViewModel()
		    {
			    Id = x.Id,
			    Title = x.Title,
			    DateTime = x.EventDate.ToString( "dd/MM/yyyy" )
		    } ).ToList();
	    }
    }
}
