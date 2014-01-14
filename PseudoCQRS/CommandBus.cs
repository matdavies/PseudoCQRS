using System;
using PseudoCQRS.Checkers;

namespace PseudoCQRS
{
	public class CommandBus : ICommandBus
	{
		private readonly ICommandHandlerProvider _commandHandlerProvider;
		private readonly IDbSessionManager _dbSessionManager;
		private readonly IPrerequisitesChecker _prerequisitesChecker;

		public CommandBus(
			ICommandHandlerProvider commandHandlerProvider,
			IDbSessionManager dbSessionManager,
			IPrerequisitesChecker prerequisitesChecker )
		{
			_commandHandlerProvider = commandHandlerProvider;
			_dbSessionManager = dbSessionManager;
			_prerequisitesChecker = prerequisitesChecker;
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

				var checkResult = _prerequisitesChecker.Check( command );
				if ( !checkResult.ContainsError )
					result = handler.Handle( command );
				else
					result.Message = checkResult.Message;

				if ( hasTransactionAttribute )
					_dbSessionManager.CommitTransaction();
			}


			return result;
		}

	}
}
