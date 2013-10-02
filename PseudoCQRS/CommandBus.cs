using System;
using PseudoCQRS.Checkers;

namespace PseudoCQRS
{
	public class CommandBus : ICommandBus
	{
		private readonly ICommandHandlerProvider _commandHandlerProvider;
		private readonly IDbSessionManager _dbSessionManager;
		private readonly IPreRequisitesChecker _preRequisitesChecker;

		public CommandBus(
			ICommandHandlerProvider commandHandlerProvider,
			IDbSessionManager dbSessionManager,
			IPreRequisitesChecker preRequisitesChecker )
		{
			_commandHandlerProvider = commandHandlerProvider;
			_dbSessionManager = dbSessionManager;
			_preRequisitesChecker = preRequisitesChecker;
		}

		public CommandResult Execute<TCommand>( TCommand command )
		{
			var result = new CommandResult
			{
				ContainsError = true,
				Message = String.Format( "Handler not found for command {0}", command.GetType().Name )
			};

			var handler = _commandHandlerProvider.GetCommandHandler<TCommand>();

			if ( handler != null )
			{
				var hasTransactionAttribute = handler.HasTransactionAttribute();

				if ( hasTransactionAttribute )
					_dbSessionManager.OpenTransaction();

				result = _preRequisitesChecker.Check( command );
				if ( !result.ContainsError )
					result = handler.Handle( command );

				if ( hasTransactionAttribute )
					_dbSessionManager.CommitTransaction();
			}


			return result;
		}

	}
}
