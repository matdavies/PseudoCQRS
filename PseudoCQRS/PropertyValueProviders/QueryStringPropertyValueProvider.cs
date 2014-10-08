using System;
using System.Web;

namespace PseudoCQRS.PropertyValueProviders
{
	public class QueryStringPropertyValueProvider : BasePropertyValueProvider, IPropertyValueProvider
	{
		public bool HasValue<T>( string key )
		{
			return HttpContext.Current.Request.QueryString[ key ] != null;
		}

		public object GetValue<T>( string key, Type propertyType )
		{
			return ConvertValue( HttpContext.Current.Request.QueryString[ key ], propertyType );
		}
	}
}