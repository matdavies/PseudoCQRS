using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace PseudoCQRS
{
	public class CommandHandlerFinder : ICommandHandlerFinder
	{
		private readonly IServiceLocator _serviceLocator;

		public CommandHandlerFinder( IServiceLocator serviceLocator )
		{
			_serviceLocator = serviceLocator;
		}

		public ICommandHandler<TCommand> FindHandlerForCommand<TCommand>()
		{
			ICommandHandler<TCommand> result = default( ICommandHandler<TCommand> );

			var commandType = typeof( TCommand );
			var handlerInheritingFromType = typeof( ICommandHandler<> ).MakeGenericType( commandType );

			var commandHandlerType = commandType.Assembly.GetTypes().SingleOrDefault( x => x.GetInterfaces().Any( y => y == handlerInheritingFromType ) );

			if ( commandHandlerType != null )
				result = _serviceLocator.GetInstance( commandHandlerType ) as ICommandHandler<TCommand>;

			return result;
		}

	}
}
