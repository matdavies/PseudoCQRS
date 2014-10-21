using System.Linq;
using System.Threading;

namespace PseudoCQRS
{
	public class EventPublisher : IEventPublisher
	{
		private readonly ISubscriptionService _subscriptionService;
		private readonly IDbSessionManager _dbSessionManager;

		public EventPublisher( ISubscriptionService subscriptionService, IDbSessionManager dbSessionManager )
		{
			_subscriptionService = subscriptionService;
			_dbSessionManager = dbSessionManager;
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
					var t = new Thread( () =>
					{
						subscriber.Notify( @event );
						_dbSessionManager.CloseSession();
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