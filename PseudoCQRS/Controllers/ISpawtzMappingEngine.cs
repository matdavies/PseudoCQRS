namespace PseudoCQRS.Controllers
{
	public interface ISpawtzMappingEngine
	{
		TTo Map<TFrom, TTo>( TFrom viewModel );
	}
}
