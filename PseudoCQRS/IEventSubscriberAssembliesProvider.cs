using System.Collections.Generic;
using System.Reflection;

namespace PseudoCQRS
{
	public interface IEventSubscriberAssembliesProvider
	{
		IEnumerable<Assembly> GetEventSubscriberAssemblies();
	}
}