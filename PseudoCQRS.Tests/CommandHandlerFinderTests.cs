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

		[SetUp]
		public void Setup()
		{
			_serviceLocator = MockRepository.GenerateMock<IServiceLocator>();
			_serviceLocator
				.Stub( x => x.GetInstance( typeof( TestCommandHandler ) ) )
				.Return( new TestCommandHandler() );

			_finder = new CommandHandlerFinder( _serviceLocator );
		}


		public class TestCommand : ICommand
		{

		}

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
	}
}
