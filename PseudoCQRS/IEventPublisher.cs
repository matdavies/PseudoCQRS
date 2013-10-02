﻿namespace PseudoCQRS
{
	public interface IEventPublisher
	{
		void Publish<T>( T @event );
		void PublishSynchnously<T>( T @event );
	}
}
