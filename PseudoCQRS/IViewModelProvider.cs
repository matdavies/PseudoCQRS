namespace PseudoCQRS
{
	public interface IViewModelProvider<TViewModel, TArgs>
		where TViewModel : class
		where TArgs : new()
	{
		TViewModel GetViewModel( TArgs args );
	}
}