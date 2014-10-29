using PseudoCQRS.Controllers;

namespace PseudoCQRS.Examples.NerdDinner.Infrastructure
{
	public class NerdDinnerMappingEngine : IViewModelToCommandMappingEngine
	{
		public TTo Map<TFrom, TTo>( TFrom viewModel )
		{
			return AutoMapper.Mapper.Map<TFrom, TTo>( viewModel );
		}
	}
}