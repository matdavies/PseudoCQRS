using System.Reflection;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using Rhino.Mocks;

namespace PseudoCQRS.Tests
{
    [TestFixture]
    public class CommandHandlerFinderTests
    {
        private IServiceLocator _serviceLocator;
        private CommandHandlerFinder _finder;
        private IAssemblyListProvider _assembliesListProvider;

        [SetUp]
        public void Setup()
        {
            _serviceLocator = MockRepository.GenerateMock<IServiceLocator>();
            _serviceLocator
                .Stub( x => x.GetInstance( typeof ( TestCommandHandler ) ) )
                .Return( new TestCommandHandler() );

            _assembliesListProvider = MockRepository.GenerateMock<IAssemblyListProvider>();
            _assembliesListProvider
                .Stub( x => x.GetAssemblies() )
                .Return( new Assembly[]
                {
                    this.GetType().Assembly
                } );

            _finder = new CommandHandlerFinder( _serviceLocator, _assembliesListProvider );
        }


        public class TestCommand : ICommand {}

        public class TestCommandHandler : ICommandHandler<TestCommand>
        {
            public CommandResult Handle( TestCommand cmd )
            {
                return new CommandResult();
            }
        }

        [Test]
        public void ShouldReturnHandlerWhenFound()
        {
            Assert.IsNotNull( _finder.FindHandlerForCommand<TestCommand>() );
        }

        [Test]
        public void ShouldFindCommandHandlerFromAssembliesListProvider()
        {
            _finder.FindHandlerForCommand<TestCommand>();
            _assembliesListProvider
                .AssertWasCalled( x => x.GetAssemblies() );
        }
    }
}