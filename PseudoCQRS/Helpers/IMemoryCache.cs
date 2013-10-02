using System.Linq;

namespace PseudoCQRS.Helpers
{
	public interface IMemoryCache
	{
		T GetValue<T>( string key, T defaultValue );
		void SetValue<T>( string key, T value );
	}
}
