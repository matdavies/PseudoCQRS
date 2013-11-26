using System;

namespace PseudoCQRS.PropertyValueProviders
{
	public interface IPropertyValueProvider
	{
//		string GetKey( Type objectType, string propertyName );
		bool HasValue<T>( string key );
		object GetValue( Type propertyType, string key );

	}
}