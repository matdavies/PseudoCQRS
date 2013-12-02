using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using PseudoCQRS.ExtensionMethods;
using PseudoCQRS.Helpers;

namespace PseudoCQRS
{
	public class SubscriptionService : ISubscriptionService
	{
        private readonly IObjectLookupCache _cache;
		private readonly IEventSubscriberAssembliesProvider _eventSubscriberAssembliesProvider;

		public SubscriptionService(
            IObjectLookupCache cache, 
			IEventSubscriberAssembliesProvider eventSubscriberAssembliesProvider )
		{
			_cache = cache;
			_eventSubscriberAssembliesProvider = eventSubscriberAssembliesProvider;
		}

		public IEnumerable<IEventSubscriber<T>> GetSubscriptions<T>()
		{
			var result = new List<IEventSubscriber<T>>();

			string typeFullName = typeof( T ).FullName;
			var subscribers = _cache.GetValue<IEnumerable<Type>>( typeFullName, null );
			if ( subscribers == null )
			{
				subscribers = ( from assembly in _eventSubscriberAssembliesProvider.GetEventSubscriberAssemblies()
				                from t in assembly.GetImplementationsOf( typeof ( IEventSubscriber<> ), typeof ( T ) )
				                select t ).ToList();
					
				if ( subscribers.Any() )
					_cache.SetValue<IEnumerable<Type>>( typeFullName, subscribers );
			}

			foreach ( var subscriberType in subscribers )
				result.Add( ServiceLocator.Current.GetInstance( subscriberType ) as IEventSubscriber<T> );

			return result;
		}
	}
}
