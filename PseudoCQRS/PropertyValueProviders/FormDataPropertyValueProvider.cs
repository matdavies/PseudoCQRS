using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;

namespace PseudoCQRS.PropertyValueProviders
{
	public class FormDataPropertyValueProvider : BasePropertyValueProvider, IPropertyValueProvider
	{
		public string GetKey( Type objectType, string propertyName )
		{
			return propertyName;
		}

		public bool HasValue( string key )
		{
			var hasValue = HttpContext.Current.Request.Form[ key ] != null;
			if ( !hasValue && HttpContext.Current.Request.Form[ key + "_Exists" ] != null )
				hasValue = true;

			return hasValue;
		}

		public object GetValue( Type propertyType, string key )
		{
			object val = HttpContext.Current.Request.Form[ key ];
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