using System;
using System.Web;

namespace PseudoCQRS.PropertyValueProviders
{
	public class SessionPropertyValueProvider : BasePropertyValueProvider, IPropertyValueProvider, IPersistablePropertyValueProvider
	{
		private const string KeyFormat = "{0}:{1}";

		public string GetKey( Type objectType, string propertyName )
		{
			return string.Format( KeyFormat, objectType.FullName, propertyName );
		}

		public bool HasValue( string key )
		{
			return HttpContext.Current.Session[ key ] != null;
		}

		public object GetValue( Type propertyType, string key )
		{
			return ConvertValue( HttpContext.Current.Session[ key ].ToString(), propertyType );
		}

		public void SetValue( string key, object value )
		{
			HttpContext.Current.Session[ key ] = value.ToString();
		}
	}
}