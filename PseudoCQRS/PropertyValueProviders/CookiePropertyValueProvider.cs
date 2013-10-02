using System;
using System.Web;

namespace PseudoCQRS.PropertyValueProviders
{
	public class CookiePropertyValueProvider : BasePropertyValueProvider, IPropertyValueProvider, IPersistablePropertyValueProvider
	{
		private const string KeyFormat = @"{0}:{1}";

		public string GetKey( Type objectType, string propertyName )
		{
			return String.Format( KeyFormat, objectType.FullName, propertyName );
		}

		public bool HasValue( string key )
		{
			return HttpContext.Current.Request.Cookies[ key ] != null;
		}

		public object GetValue( Type propertyType, string key )
		{
			return ConvertValue( HttpContext.Current.Request.Cookies[ key ].Value, propertyType );
		}

		public void SetValue( string key, object value )
		{
			HttpContext.Current.Response.Cookies.Set( new HttpCookie( key, value.ToString() ) );
		}
	}
}