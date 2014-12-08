using System.Collections.Generic;

namespace PseudoCQRS.Helpers
{
	public class ObjectLookupCache : IObjectLookupCache
	{
		private static readonly Dictionary<string, object> InternalCache;

		static ObjectLookupCache()
		{
			InternalCache = new Dictionary<string, object>();
		}

		public T GetValue<T>( string key, T defaultValue )
		{
			if ( InternalCache.ContainsKey( key ) )
				return (T)InternalCache[ key ];

			return defaultValue;
		}

		public void SetValue<T>( string key, T value )
		{
			InternalCache[ key ] = value;
		}
	}
}