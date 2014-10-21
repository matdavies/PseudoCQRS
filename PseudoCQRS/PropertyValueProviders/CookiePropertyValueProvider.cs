using System;
using System.Web;

namespace PseudoCQRS.PropertyValueProviders
{
	public class CookiePropertyValueProvider : BasePropertyValueProvider, IPropertyValueProvider, IPersistablePropertyValueProvider
	{
		private const string KeyFormat = @"{0}:{1}";

		private string GetKey( Type objectType, string propertyName )
		{
			return String.Format( KeyFormat, objectType.FullName, propertyName );
		}

		public bool HasValue<T>( string key )
		{
			return HttpContext.Current.Request.Cookies[ GetKey( typeof( T ), key ) ] != null;
		}

		public object GetValue<T>( string key, Type propertyType )
		{
			return ConvertValue( HttpContext.Current.Request.Cookies[ GetKey( typeof( T ), key ) ].Value, propertyType );
		}

		public void SetValue<T>( string key, object value )
		{
			HttpContext.Current.Response.Cookies.Set( new HttpCookie( GetKey( typeof( T ), key ), value.ToString() ) );
		}
	}
}