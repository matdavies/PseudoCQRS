using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;

namespace PseudoCQRS.PropertyValueProviders
{
	public class FormDataPropertyValueProvider : BasePropertyValueProvider, IPropertyValueProvider
	{
		private string GetKey( Type objectType, string propertyName )
		{
			return propertyName;
		}

		public bool HasValue<T>( string key )
		{
			var hasValue = HttpContext.Current.Request.Form[ key ] != null;
			if ( !hasValue && HttpContext.Current.Request.Form[ key + "_Exists" ] != null )
				hasValue = true;

			return hasValue;
		}

		public object GetValue<T>( string key, Type propertyType )
		{
			object val = HttpContext.Current.Request.Form[ GetKey( propertyType, key ) ];
			if ( PropertyIsListAndValueIsNull( propertyType, val ) )
				return GetEmptyListProperty( propertyType );

			return ConvertValue( val.ToString(), propertyType );
		}

		private bool PropertyIsListAndValueIsNull( Type propertyType, object value )
		{
			return propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof( List<> ) && value == null;
		}

		private IList GetEmptyListProperty( Type propertyType )
		{
			var containedType = propertyType.GetGenericArguments()[ 0 ];
			var emptyList = (IList)Activator.CreateInstance( ( typeof( List<> ).MakeGenericType( containedType ) ) );
			return emptyList;
		}
	}
}