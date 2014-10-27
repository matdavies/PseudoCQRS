using System;
using Microsoft.Practices.ServiceLocation;

namespace PseudoCQRS.PropertyValueProviders
{
	public class SessionPropertyValueProvider : BasePropertyValueProvider, IPropertyValueProvider, IPersistablePropertyValueProvider
	{
		private const string KeyFormat = "{0}:{1}";

		private readonly IHttpContextWrapper _httpContextWrapper;

		public SessionPropertyValueProvider()
			:this(ServiceLocator.Current.GetInstance<IHttpContextWrapper>())
		{

		}

		public SessionPropertyValueProvider( IHttpContextWrapper httpContextWrapper )
		{
			_httpContextWrapper = httpContextWrapper;
		}

		private string GetKey( Type objectType, string propertyName )
		{
			return string.Format( KeyFormat, objectType.FullName, propertyName );
		}

		public bool HasValue<T>( string key )
		{
			return _httpContextWrapper.ContainsSessionItem( GetKey( typeof( T ), key ) );
		}

		public object GetValue<T>( string key, Type propertyType )
		{
			return ConvertValue( _httpContextWrapper.GetSessionItem(  GetKey( typeof( T ), key ) ).ToString(), propertyType );
		}

		public void SetValue<T>( string key, object value )
		{
			_httpContextWrapper.SetSessionItem( GetKey( typeof( T ), key ) , value.ToString() );
		}
	}
}