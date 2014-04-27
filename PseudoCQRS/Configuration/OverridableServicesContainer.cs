using System;
using System.Collections.Generic;
using System.Linq;
using PseudoCQRS.Checkers;
using PseudoCQRS.Controllers;
using PseudoCQRS.Helpers;
using PseudoCQRS.PropertyValueProviders;

namespace PseudoCQRS.Configuration
{
	public class OverridableServicesContainer
	{
		private readonly Dictionary<Type, Type> _overrides;

		public OverridableServicesContainer()
		{
			_overrides = new Dictionary<Type, Type>();
		}

		private void AddOverride( Type interfaceType, Type implementationType )
		{
			if ( implementationType.GetInterfaces().All( x => x != interfaceType ) )
				throw new Exception( "Implementation type do not inherit from interface type" );

			_overrides.Add( interfaceType, implementationType );
		}

		public void CheckersExecuter( Type checkersExecutorType )
		{
			AddOverride( typeof ( ICheckersExecuter ), checkersExecutorType );
		}

		public void CheckersFinder( Type checkersFinderType )
		{
			AddOverride( typeof ( ICheckersFinder ), checkersFinderType );
		}

		public void PrerequisitesChecker( Type prerequisitesCheckerType )
		{
			AddOverride( typeof( IPrerequisitesChecker ), prerequisitesCheckerType );
		}

		public void CommandExecutor( Type commandExecutorType )
		{
			AddOverride( typeof( ICommandExecutor ), commandExecutorType );
		}

		public void MessageManager( Type messageManagerType )
		{
			AddOverride( typeof( IMessageManager ), messageManagerType );
		}

		public void ReferrerProvider( Type referrerProviderType )
		{
			AddOverride( typeof( IReferrerProvider ), referrerProviderType );
		}

		public void ObjectLookupCache( Type objectLookupCacheType )
		{
			AddOverride( typeof ( IObjectLookupCache ), objectLookupCacheType );
		}

		public void PropertyValueProviderFactory( Type propertyValueProviderFactoryType )
		{
			AddOverride( typeof(IPropertyValueProviderFactory), propertyValueProviderFactoryType );
		}

		public void AssemblyListProvider( Type assemblyListProvider )
		{
			AddOverride( typeof ( IAssemblyListProvider ), assemblyListProvider );
		}

		public void CommandBus( Type commandBusType )
		{
			AddOverride( typeof ( ICommandBus ), commandBusType );
		}

		public void CommandHandlerFinder( Type commandHandlerFinderType )
		{
			AddOverride( typeof ( ICommandHandlerFinder ), commandHandlerFinderType );
		}

		public void CommandHandlerProvider( Type commandHandlerProviderType )
		{
			AddOverride( typeof(ICommandHandlerProvider), commandHandlerProviderType );
		}

		public void EventPublisher( Type eventPublisherType )
		{
			AddOverride( typeof(IEventPublisher), eventPublisherType );
		}

		public void SubscriptionService( Type subscriptionServiceType )
		{
			AddOverride( typeof(ISubscriptionService), subscriptionServiceType );
		}

		public void ViewModelProviderArgumentsProvider( Type viewModelProviderArgumentsProviderType )
		{
			AddOverride( typeof ( IViewModelProviderArgumentsProvider ), viewModelProviderArgumentsProviderType );
		}

		internal Dictionary<Type, Type> GetOverrides()
		{
			return _overrides;
		}
	}
}