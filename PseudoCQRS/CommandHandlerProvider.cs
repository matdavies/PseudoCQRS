using PseudoCQRS.Helpers;

namespace PseudoCQRS
{
	public class CommandHandlerProvider : ICommandHandlerProvider
	{
		private readonly ICommandHandlerFinder _commandHandlerFinder;
		private readonly IObjectLookupCache _cache;

        public CommandHandlerProvider(ICommandHandlerFinder commandHandlerFinder, IObjectLookupCache cache)
		{
			_commandHandlerFinder = commandHandlerFinder;
			_cache = cache;
		}
		

		public ICommandHandler<TCommand> GetCommandHandler<TCommand>()
		{
			var commandTypeFullName = typeof(TCommand).FullName;

			var handler = _cache.GetValue<ICommandHandler<TCommand>>( commandTypeFullName, null );
			if ( handler == null )
			{
				handler = _commandHandlerFinder.FindHandlerForCommand<TCommand>();
				if ( handler != null )
					_cache.SetValue<ICommandHandler<TCommand>>( commandTypeFullName, handler );
			}

			return handler;
		}
	}
}
