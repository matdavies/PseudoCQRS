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

		public void PublishSynchnously<T>( T @event )
		{
			PublishInternal( @event, false );
		}

		private void PublishInternal<T>( T @event, bool doAsync )
		{
			var subscribers = _subscriptionService.GetSubscriptions<T>();
			if ( subscribers != null )
			{
				foreach ( var subscriber in subscribers )
				{
					if ( doAsync && subscriber.IsAsynchronous )
					{
						var t = new Thread( () =>
						{

							subscriber.Notify( @event );
							_dbSessionManager.CloseSession();
							/*
							try
							{
								subscriber.Notify( @event );
								_dbSessionManager.CloseSession();
							}
							catch ( Exception exception )
							{

								File.AppendAllText( @"D:\EventPublisherExceptions.txt", String.Format( "{0} {1}", exception.Message, exception.StackTrace ) + "]\r\n\r\n" );
								throw;
							}
							*/
						});
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
}
