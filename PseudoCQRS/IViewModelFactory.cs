namespace PseudoCQRS
{
	public interface IViewModelFactory<TViewModel, TArgs> where TViewModel : class where TArgs : new()
	{
		TViewModel GetViewModel();
	}
}
