using System;
using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace PseudoCQRS
{
	public class CommandHandlerFinder : ICommandHandlerFinder
	{
		private readonly IServiceLocator _serviceLocator;
	    private readonly IAssemblyListProvider _assembliesListProvider;

		public CommandHandlerFinder( IServiceLocator serviceLocator, IAssemblyListProvider assembliesListProvider )
		{
		    _serviceLocator = serviceLocator;
		    _assembliesListProvider = assembliesListProvider;
		}

	    public ICommandHandler<TCommand> FindHandlerForCommand<TCommand>()
		{
			ICommandHandler<TCommand> result = default( ICommandHandler<TCommand> );

			var commandType = typeof( TCommand );
			var handlerInheritingFromType = typeof( ICommandHandler<> ).MakeGenericType( commandType );

			var commandHandlerType = GetCommandHandlerType( handlerInheritingFromType );
			if ( commandHandlerType != null )
				result = _serviceLocator.GetInstance( commandHandlerType ) as ICommandHandler<TCommand>;

			return result;
		}

	    private Type GetCommandHandlerType( Type handlerInheritingFromType )
	    {
            var commandHandlerType = _assembliesListProvider
                .GetAssemblies()
                .SelectMany( x => x.GetTypes() )
                .SingleOrDefault( x => x.GetInterfaces().Any( y => y == handlerInheritingFromType ) );

	        return commandHandlerType;
	    }
	}
}
