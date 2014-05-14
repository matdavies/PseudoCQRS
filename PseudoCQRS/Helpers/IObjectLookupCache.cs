namespace PseudoCQRS.Helpers
{
	public interface IObjectLookupCache
	{
		T GetValue<T>( string key, T defaultValue );
		void SetValue<T>( string key, T value );
	}
}