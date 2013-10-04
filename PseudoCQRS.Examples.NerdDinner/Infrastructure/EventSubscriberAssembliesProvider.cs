using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PseudoCQRS.Examples.NerdDinner.Infrastructure
{
	public class EventSubscriberAssembliesProvider : IEventSubscriberAssembliesProvider
	{
		public IEnumerable<Assembly> GetEventSubscriberAssemblies()
		{
			return new List<Assembly>
			{
				this.GetType().Assembly
			};
		}
	}
}