using System;
using NUnit.Framework;
using PseudoCQRS.Helpers;
using Rhino.Mocks;

namespace PseudoCQRS.Tests
{
	[TestFixture]
	public class CommandHandlerProviderTests
	{
		private ICommandHandlerFinder _commandHandlerFinder;
		private IMemoryCache _keyValueProvider;
		private CommandHandlerProvider _provider;

		[SetUp]
		public void Setup()
		{
			_commandHandlerFinder = MockRepository.GenerateMock<ICommandHandlerFinder>();
			_keyValueProvider = MockRepository.GenerateMock<IMemoryCache>();
			_provider = new CommandHandlerProvider( _commandHandlerFinder, _keyValueProvider );
		}

		private ICommandHandler<BlankSimpleTestCommand> ExecuteArrangeAndAct(
			ICommandHandler<BlankSimpleTestCommand> commandHandlerFinderRetVal = null,
			ICommandHandler<BlankSimpleTestCommand> keyValueProviderRetVal = null )
		{
			_commandHandlerFinder
				.Stub( x => x.FindHandlerForCommand<BlankSimpleTestCommand>() )
				.IgnoreArguments()
				.Return( commandHandlerFinderRetVal );

			_keyValueProvider
				.Stub( x => x.GetValue<ICommandHandler<BlankSimpleTestCommand>>( typeof( BlankSimpleTestCommand ).FullName, null ) )
				.IgnoreArguments()
				.Return( keyValueProviderRetVal );

			return _provider.GetCommandHandler<BlankSimpleTestCommand>();
		}



		// WHEN: _keyValueProvider.GetValue is null
		// EXPECT: Call _commandHandlerFinder.FindHandlerForCommand
		[Test]
		public void ExecuteWhenKeyValueProviderReturnsNullShouldCallFinder()
		{
			var result = ExecuteArrangeAndAct();

			_commandHandlerFinder.AssertWasCalled( x => x.FindHandlerForCommand<BlankSimpleTestCommand>() );
		}

		// WHEN: _keyValueProvider.GetValue is null
		// WHEN: _commandHandlerFinder.FindHandlerForCommand is not null
		// EXPECT: Call _keyValueProvider.SetValue
		[Test]
		public void ExecuteWhenKeyValueProviderReturnsNullButFinderIsNotNullThenShouldCallSetValue()
		{
			var handler = MockRepository.GenerateMock<ICommandHandler<BlankSimpleTestCommand>>();
			var result = ExecuteArrangeAndAct( commandHandlerFinderRetVal: handler );

			_keyValueProvider.AssertWasCalled( x => x.SetValue<ICommandHandler<BlankSimpleTestCommand>>(
				Arg<string>.Is.Same( typeof( BlankSimpleTestCommand ).FullName ),
				Arg<ICommandHandler<BlankSimpleTestCommand>>.Is.Anything )
			);

		}

		// WHEN: _keyValueProvider.GetValue is not null
		// EXPECT: Not Call _commandHandlerFinder.FindHandlerForCommand
		[Test]
		public void ExecuteWhenKeyValueProviderIsNotNullThenShouldNotCallFinder()
		{
			var handler = MockRepository.GenerateMock<ICommandHandler<BlankSimpleTestCommand>>();

			var result = ExecuteArrangeAndAct( keyValueProviderRetVal: handler );

			_commandHandlerFinder.AssertWasNotCalled( x => x.FindHandlerForCommand<BlankSimpleTestCommand>() );
		}


	}
}
