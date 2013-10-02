using System.Linq;

namespace PseudoCQRS
{
	public static class CommandHandlerExtensionMethods
	{
		public static bool HasTransactionAttribute<T>( this ICommandHandler<T> commandHandler )
		{
			return commandHandler.GetType().GetCustomAttributes( typeof( DbTransactionAttribute ), false ).Any();
		}
	}
}
