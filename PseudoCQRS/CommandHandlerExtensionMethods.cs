using System;
using System.Linq;

namespace PseudoCQRS
{
	public static class CommandHandlerExtensionMethods
	{
		public static bool HasTransactionAttribute<T>( this ICommandHandler<T> commandHandler )
		{
			var attributeType = Type.GetType( "PseudoCQRS.DbTransactionAttribute, PseudoCQRS.Mvc, Version=4.2.0.0, Culture=neutral, PublicKeyToken=null" );
			if ( attributeType == null )
				return false;
			return commandHandler.GetType().GetCustomAttributes( attributeType , false ).Any();
		}
	}
}