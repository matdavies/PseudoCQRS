using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using Rhino.Mocks;

namespace PseudoCQRS.Tests
{
	[TestFixture]
	public class EventPublisherTests
	{
		private ISubscriptionService _subscriptionService;
		private IDbSessionManager _dbSessionManager;
		private EventPublisher _publisher;

		private static StringBuilder _sharedStringBuilder = new StringBuilder();

		// ReSharper disable once MemberCanBePrivate.Global
		public class TestingEvent
		{
		}

		[SetUp]
		public void Setup()
		{
			_subscriptionService = MockRepository.GenerateMock<ISubscriptionService>();
			_dbSessionManager = MockRepository.GenerateMock<IDbSessionManager>();
			_publisher = new EventPublisher( _subscriptionService, _dbSessionManager );
		}

		// EXPECT: Call _subscriptionService.GetSubscriptions
		[Test]
		public void ShouldCallSubscriptionServiceGetSubscriptions()
		{
			_subscriptionService
				.Stub( x => x.GetSubscriptions<TestingEvent>() )
				.Return( new List<IEventSubscriber<TestingEvent>>() );

			_publisher.Publish<TestingEvent>( new TestingEvent() );

			_subscriptionService.AssertWasCalled( x => x.GetSubscriptions<TestingEvent>() );
		}

		// WHEN: _subscriptionService.GetSubscriptions returns > 0
		// EXPECT: Call Apply for each subscriber found.
		[Test]
		public void ShouldCallApplyMethodOfEachSubscriberFoundWhenSubscriptionServiceGetSubscriptionsReturnsGreaterThan0()
		{
			var mockedEventSubscriber1 = MockRepository.GenerateMock<IEventSubscriber<TestingEvent>>();
			var mockedEventSubscriber2 = MockRepository.GenerateMock<IEventSubscriber<TestingEvent>>();

			_subscriptionService
				.Stub( x => x.GetSubscriptions<TestingEvent>() )
				.Return( new List<IEventSubscriber<TestingEvent>>
				{
					mockedEventSubscriber1,
					mockedEventSubscriber2
				} );

			_publisher.Publish<TestingEvent>( new TestingEvent() );

			mockedEventSubscriber1.AssertWasCalled( x => x.Notify( Arg<TestingEvent>.Is.Anything ) );
			mockedEventSubscriber2.AssertWasCalled( x => x.Notify( Arg<TestingEvent>.Is.Anything ) );
		}

		public class AsyncSubscriber : IEventSubscriber<TestingEvent>
		{
			public void Notify( TestingEvent @event )
			{
				Thread.Sleep( 1000 );
				_sharedStringBuilder.Append( "Executing Subscriber, " );
			}

			public bool IsAsynchronous
			{
				get { return true; }
			}
		}

		public class AsyncSubscriber2 : IEventSubscriber<TestingEvent>
		{
			public void Notify( TestingEvent @event )
			{
			}

			public bool IsAsynchronous
			{
				get { return true; }
			}
		}

		[Test]
		public void CanNotifyAsynchronousSubscriber()
		{
			_sharedStringBuilder = new StringBuilder();
			_subscriptionService
				.Stub( x => x.GetSubscriptions<TestingEvent>() )
				.Return( new List<IEventSubscriber<TestingEvent>>
				{
					new AsyncSubscriber()
				} );

			_sharedStringBuilder.Append( "Started, " );
			_publisher.Publish<TestingEvent>( new TestingEvent() );
			_sharedStringBuilder.Append( "Ended, " );
			Thread.Sleep( 2000 );
			//Console.WriteLine( _sharedStringBuilder.ToString() );
			Assert.AreEqual( "Started, Ended, Executing Subscriber, ", _sharedStringBuilder.ToString() );
		}

		[Test]
		public void ShouldRunSyncWhenForced()
		{
			_sharedStringBuilder = new StringBuilder();
			_subscriptionService
				.Stub( x => x.GetSubscriptions<TestingEvent>() )
				.Return( new List<IEventSubscriber<TestingEvent>>
				{
					new AsyncSubscriber()
				} );

			_sharedStringBuilder.Append( "Started, " );
			_publisher.PublishSynchronously<TestingEvent>( new TestingEvent() );
			_sharedStringBuilder.Append( "Ended, " );
			Thread.Sleep( 2000 );
			Assert.AreEqual( "Started, Executing Subscriber, Ended, ", _sharedStringBuilder.ToString() );
		}

		[Test]
		public void ShouldCloseDbSessionAfterCallingEachAsyncSubscriber()
		{
			_subscriptionService
				.Stub( x => x.GetSubscriptions<TestingEvent>() )
				.Return( new List<IEventSubscriber<TestingEvent>>
				{
					new AsyncSubscriber(),
					new AsyncSubscriber2()
				} );
			_publisher.Publish( new TestingEvent() );
			Thread.Sleep( 2000 );
			Console.WriteLine( _sharedStringBuilder.ToString() );
			_dbSessionManager.AssertWasCalled( x => x.CloseSession(), x => x.Repeat.Twice() );
		}

		public class DummyEventForTestingCanRunSubscribers
		{
		}

		public class EventSubscriberWhichAlwaysRun : IEventSubscriber<DummyEventForTestingCanRunSubscribers>
		{
			public void Notify( DummyEventForTestingCanRunSubscribers @event ) => NotifiedSubscribersForTestingCanRunSubscribers.Add( this.GetType() );

			public bool IsAsynchronous { get; } = false;
		}

		public class EventSubscriberWhichCanDecideButRunRunAlways : IEventSubscriber<DummyEventForTestingCanRunSubscribers>, IEventSubscriberRunnable<DummyEventForTestingCanRunSubscribers>
		{
			public void Notify( DummyEventForTestingCanRunSubscribers @event ) => NotifiedSubscribersForTestingCanRunSubscribers.Add( this.GetType() );

			public bool IsAsynchronous { get; } = false;
			public bool CanRun( DummyEventForTestingCanRunSubscribers @event ) => true;
		}

		public class EventSubscriberWhichCanDecideButNeverRuns : IEventSubscriber<DummyEventForTestingCanRunSubscribers>, IEventSubscriberRunnable<DummyEventForTestingCanRunSubscribers>
		{
			public void Notify( DummyEventForTestingCanRunSubscribers @event ) => NotifiedSubscribersForTestingCanRunSubscribers.Add( this.GetType() );

			public bool IsAsynchronous { get; } = false;
			public bool CanRun( DummyEventForTestingCanRunSubscribers @event ) => false;
		}

		private static readonly List<Type> NotifiedSubscribersForTestingCanRunSubscribers = new List<Type>();

		[Test]
		public void CheckIfSubscriberIsRunnableBeforeNotifyingThem()
		{
			_subscriptionService
				.Stub( x => x.GetSubscriptions<DummyEventForTestingCanRunSubscribers>() )
				.Return( new List<IEventSubscriber<DummyEventForTestingCanRunSubscribers>>
				{
					new EventSubscriberWhichAlwaysRun(),
					new EventSubscriberWhichCanDecideButRunRunAlways(),
					new EventSubscriberWhichCanDecideButNeverRuns(),

				} );
			_publisher.Publish( new DummyEventForTestingCanRunSubscribers() );

			Assert.AreEqual( 2, NotifiedSubscribersForTestingCanRunSubscribers.Count );
			Assert.Contains( typeof( EventSubscriberWhichAlwaysRun ), NotifiedSubscribersForTestingCanRunSubscribers );
			Assert.Contains( typeof( EventSubscriberWhichCanDecideButRunRunAlways ), NotifiedSubscribersForTestingCanRunSubscribers );
		}
	}
}