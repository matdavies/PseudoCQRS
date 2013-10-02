using System;
using PseudoCQRS.Helpers;

namespace PseudoCQRS
{
	public class CommandHandlerProvider : ICommandHandlerProvider
	{
		private readonly ICommandHandlerFinder _commandHandlerFinder;
		private readonly IMemoryCache _memoryKeyValueProvider;

		public CommandHandlerProvider( ICommandHandlerFinder commandHandlerFinder, IMemoryCache memoryKeyValueProvider )
		{
			_commandHandlerFinder = commandHandlerFinder;
			_memoryKeyValueProvider = memoryKeyValueProvider;
		}
		

		public ICommandHandler<TCommand> GetCommandHandler<TCommand>()
		{
			var commandTypeFullName = typeof(TCommand).FullName;

			var handler = _memoryKeyValueProvider.GetValue<ICommandHandler<TCommand>>( commandTypeFullName, null );
			if ( handler == null )
			{
				handler = _commandHandlerFinder.FindHandlerForCommand<TCommand>();
				if ( handler != null )
					_memoryKeyValueProvider.SetValue<ICommandHandler<TCommand>>( commandTypeFullName, handler );
			}

			return handler;
		}
	}
}
