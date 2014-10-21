using PseudoCQRS.Controllers;

namespace PseudoCQRS.Tests.Controllers.Helpers
{
	public class DummyReadController : BaseReadController<DummyReadViewModel, DummyReadViewModelProviderArgument>
	{
		public DummyReadController( IViewModelFactory<DummyReadViewModel, DummyReadViewModelProviderArgument> viewModelFactory ) : base( viewModelFactory ) {}

		public DummyReadController() {}

		public override string ViewPath
		{
			get { return "Test/Read"; }
		}
	}
}