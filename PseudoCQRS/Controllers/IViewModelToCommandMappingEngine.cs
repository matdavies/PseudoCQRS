namespace PseudoCQRS.Controllers
{
	public interface IViewModelToCommandMappingEngine
	{
		TTo Map<TFrom, TTo>( TFrom viewModel );
	}
}