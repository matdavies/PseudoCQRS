namespace PseudoCQRS
{
	public interface IEventSubscriber<TEvent>
	{
		void Notify( TEvent @event );
		bool IsAsynchronous { get; }
	}
}