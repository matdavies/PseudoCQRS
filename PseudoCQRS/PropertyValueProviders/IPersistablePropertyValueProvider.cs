namespace PseudoCQRS.PropertyValueProviders
{
	public interface IPersistablePropertyValueProvider
	{
		void SetValue( string key, object value );
	}
}
