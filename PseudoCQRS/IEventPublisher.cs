namespace PseudoCQRS
{
	public interface IEventPublisher
	{
		void Publish<T>( T @event );
		void PublishSynchronously<T>( T @event );
	}
}