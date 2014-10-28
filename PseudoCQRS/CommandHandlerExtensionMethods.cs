using System;
using System.Linq;

namespace PseudoCQRS
{
	public static class CommandHandlerExtensionMethods
	{
		public static bool HasTransactionAttribute<T>( this ICommandHandler<T> commandHandler )
		{
			var attributeType = typeof( DbTransactionAttribute );
			return commandHandler.GetType().GetCustomAttributes( attributeType , false ).Any();
		}
	}
}