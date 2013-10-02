using System;
using System.Web;

namespace PseudoCQRS.PropertyValueProviders
{
	public class QueryStringPropertyValueProvider : BasePropertyValueProvider, IPropertyValueProvider
	{
		public string GetKey( Type objectType, string propertyName )
		{
			return propertyName;
		}

		public bool HasValue( string key )
		{
			return HttpContext.Current.Request.QueryString[ key ] != null;
		}

		public object GetValue( Type propertyType, string key )
		{
			return ConvertValue( HttpContext.Current.Request.QueryString[ key ], propertyType );
		}
	}
}