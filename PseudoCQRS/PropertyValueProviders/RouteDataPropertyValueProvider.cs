using System;
using System.Web;

namespace PseudoCQRS.PropertyValueProviders
{
	public class RouteDataPropertyValueProvider : BasePropertyValueProvider, IPropertyValueProvider
	{
		public bool HasValue<T>( string key )
		{
			return HttpContext.Current.Request.RequestContext.RouteData.Values.ContainsKey( key );
		}

		public object GetValue<T>( string key, Type propertyType )
		{
			return ConvertValue( HttpContext.Current.Request.RequestContext.RouteData.Values[ key ].ToString(), propertyType );
		}
	}
}