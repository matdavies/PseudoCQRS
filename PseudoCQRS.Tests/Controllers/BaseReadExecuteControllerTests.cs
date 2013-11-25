using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using PseudoCQRS.Controllers;
using PseudoCQRS.Tests.Controllers.Helpers;
using Rhino.Mocks;

namespace PseudoCQRS.Tests.Controllers
{
    [TestFixture]
    public class BaseReadExecuteControllerTests
    {
        private IViewModelToCommandMappingEngine _mapper;
        private IServiceLocator _mockedServiceLocator;

        private DummyReadExecuteController _controller;
        private ICommandExecutor _commandExecutor;
        private IViewModelFactory<DummyReadExecuteViewModel, DummyReadExecuteViewModelArgs> _viewModelFactory;

        [SetUp]
        public void Setup()
        {
            _mapper = MockRepository.GenerateMock<IViewModelToCommandMappingEngine>();
            _mockedServiceLocator = MockRepository.GenerateMock<IServiceLocator>();
            _mockedServiceLocator
                .Stub( x => x.GetInstance<IViewModelToCommandMappingEngine>() )
                .Return( _mapper );
            ServiceLocator.SetLocatorProvider( () => _mockedServiceLocator );

            _commandExecutor = MockRepository.GenerateMock<ICommandExecutor>();
            _viewModelFactory = MockRepository.GenerateMock<IViewModelFactory<DummyReadExecuteViewModel, DummyReadExecuteViewModelArgs>>();

            _controller = new DummyReadExecuteController(
                _commandExecutor,
                _viewModelFactory );

            _viewModelFactory
                .Stub( x => x.GetViewModel() )
                .Return( new DummyReadExecuteViewModel() );

            var routeData = new RouteData();
            routeData.Values.Add( "controller", "DummyDeleteFile" );
            _controller.ControllerContext = new ControllerContext
            {
                RouteData = routeData
            };
        }


        [Test]
        public void GET_ShouldReturnView()
        {
            Assert.IsInstanceOf<ViewResult>( _controller.Execute() );
        }

        [Test]
        public void GET_ShouldCallViewModelProvider()
        {
            _controller.Execute();
            _viewModelFactory
                .AssertWasCalled( x => x.GetViewModel() );
        }

        private ActionResult PostArrangeExecute( bool executeCommandContainsError = false )
        {
            var routeData = new RouteData();
            routeData.Values.Add( "controller", "DummyReadExecute" );
            _controller.ControllerContext = new ControllerContext
            {
                RouteData = routeData
            };
            var form = new FormCollection();
            _controller.ValueProvider = form.ToValueProvider();

            _viewModelFactory
                .Stub( x => x.GetViewModel() )
                .Return( new DummyReadExecuteViewModel() );

            _commandExecutor
                .Stub( x => x.ExecuteCommand( Arg<DummyReadExecuteCommand>.Is.Anything ) )
                .Return( new CommandResult
                {
                    ContainsError = executeCommandContainsError
                } );

            return _controller.Execute( form );
        }

        [Test]
        public void POST_ShouldExecuteATestWhenCanTryUpdateModelReturnsTrue()
        {
            PostArrangeExecute();
            _commandExecutor.AssertWasCalled( x => x.ExecuteCommand( Arg<DummyReadExecuteCommand>.Is.Anything ) );
        }

        [Test]
        public void POST_ShouldReturnOnFailureActionWhenCommandContainsError()
        {
            PostArrangeExecute( true );
            Assert.AreEqual( "Failed", _controller.TempData[ "Error" ] );
        }

        [Test]
        public void HasSetAllDependencies()
        {
            var viewModelFactory = MockRepository.GenerateMock<IViewModelFactory<DummyReadExecuteViewModel, DummyReadExecuteViewModelArgs>>();
            var commandExecutor = MockRepository.GenerateMock<ICommandExecutor>();
            var locator = MockRepository.GenerateMock<IServiceLocator>();
            locator
                .Stub( x => x.GetInstance<IViewModelFactory<DummyReadExecuteViewModel, DummyReadExecuteViewModelArgs>>() )
                .Return( viewModelFactory );

            locator
                .Stub( x => x.GetInstance<ICommandExecutor>() )
                .Return( commandExecutor );

            ServiceLocator.SetLocatorProvider( () => locator );
            var controller = new DummyReadExecuteController();

            HasSetAllDependenciesControllerHelper.AssertFieldsAreNotNull( controller );
        }
    }
}