using System;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace PseudoCQRS
{
	public class EventPublisher : IEventPublisher
	{
		private readonly ISubscriptionService _subscriptionService;
		private readonly IDbSessionManager _dbSessionManager;
		private readonly ILogger _logger;

		public EventPublisher( ISubscriptionService subscriptionService, IDbSessionManager dbSessionManager, ILogger logger )
		{
			_subscriptionService = subscriptionService;
			_dbSessionManager = dbSessionManager;
			_logger = logger;
		}

		public void Publish<T>( T @event )
		{
			PublishInternal( @event, true );
		}

		public void PublishSynchronously<T>( T @event )
		{
			PublishInternal( @event, false );
		}

		private void PublishInternal<T>( T @event, bool doAsync )
		{
			var subscribers = _subscriptionService.GetSubscriptions<T>();
			foreach ( var subscriber in subscribers )
			{
				if ( doAsync && subscriber.IsAsynchronous )
				{
					var eventSubscriber = subscriber;
					var t = new Thread( () =>
					{
						try
						{
							eventSubscriber.Notify( @event );
						}
						catch ( Exception )
						{
							string message = $@"Error occurred when the following subcriber '{eventSubscriber.GetType().FullName}' was notified about event '{@event.GetType()}'";
							_logger.Log( message, @event );
						} finally
						{
							_dbSessionManager.CloseSession();
						}
					} );
					t.Start();
				}
				else
				{
					subscriber.Notify( @event );
				}
			}
		}
	}
}