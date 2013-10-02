using System.Collections.Generic;

namespace PseudoCQRS
{
	public interface ISubscriptionService
	{
		IEnumerable<IEventSubscriber<T>> GetSubscriptions<T>();
	}
}
