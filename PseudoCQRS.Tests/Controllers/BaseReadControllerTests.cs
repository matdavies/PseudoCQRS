using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using PseudoCQRS.Tests.Controllers.Helpers;
using Rhino.Mocks;

namespace PseudoCQRS.Tests.Controllers
{
	[TestFixture]
	public class BaseReadControllerTests
	{
		private IViewModelFactory<DummyReadViewModel, DummyReadViewModelProviderArgument> _viewModelFactory;
		private DummyReadController _controller;

		[SetUp]
		public void Setup()
		{
			_viewModelFactory = MockRepository.GenerateMock<IViewModelFactory<DummyReadViewModel, DummyReadViewModelProviderArgument>>();
			_controller = new DummyReadController( _viewModelFactory );

			var routeData = new RouteData();
			routeData.Values.Add( "controller", "DummyDeleteFile" );
			_controller.ControllerContext = new ControllerContext { RouteData = routeData };
		}

		[Test]
		public void ShouldReturnView()
		{
			Assert.IsInstanceOf<ViewResult>( _controller.Execute() );

		}

		[Test]
		public void ShouldCallGetViewModel()
		{
			_controller.Execute();
			_viewModelFactory
				.AssertWasCalled( x => x.GetViewModel() );
		}
	}
}
