using System;
using System.Web;

namespace PseudoCQRS.PropertyValueProviders
{
	public class SessionPropertyValueProvider : BasePropertyValueProvider, IPropertyValueProvider, IPersistablePropertyValueProvider
	{
		private const string KeyFormat = "{0}:{1}";

		private string GetKey( Type objectType, string propertyName )
		{
			return string.Format( KeyFormat, objectType.FullName, propertyName );
		}

		public bool HasValue<T>( string key )
		{
			return HttpContext.Current.Session[ GetKey( typeof( T ), key ) ] != null;
		}

		public object GetValue<T>( string key, Type propertyType )
		{
			return ConvertValue( HttpContext.Current.Session[ GetKey( typeof( T ), key ) ].ToString(), propertyType );
		}

		public void SetValue<T>( string key, object value )
		{
			HttpContext.Current.Session[ GetKey( typeof( T ), key ) ] = value.ToString();
		}
	}
}