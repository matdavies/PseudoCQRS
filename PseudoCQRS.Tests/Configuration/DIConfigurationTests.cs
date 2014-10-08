using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using PseudoCQRS.Checkers;
using PseudoCQRS.Configuration;
using PseudoCQRS.Controllers;
using PseudoCQRS.Helpers;
using PseudoCQRS.PropertyValueProviders;

namespace PseudoCQRS.Tests.Configuration
{
	[TestFixture]
	public class DIConfigurationTests
	{
		#region Dummy Classes

		private class DummyViewModel {}

		private class DummyViewModelProviderArgument {}

		private class DummyViewModelProvider : IViewModelProvider<DummyViewModel, DummyViewModelProviderArgument>
		{
			public DummyViewModel GetViewModel( DummyViewModelProviderArgument args )
			{
				throw new NotImplementedException();
			}
		}

		private class DummyCheckersExecutor : ICheckersExecuter
		{
			public CheckResult ExecuteAuthorizationCheckers( object instance )
			{
				throw new NotImplementedException();
			}

			public CheckResult ExecuteAccessCheckers( object instance )
			{
				throw new NotImplementedException();
			}

			public CheckResult ExecuteValidationCheckers<T>( T instance )
			{
				throw new NotImplementedException();
			}
		}

		private class DummyCheckersFinder : ICheckersFinder
		{
			public List<IValidationChecker<T>> FindValidationCheckers<T>( T instance )
			{
				throw new NotImplementedException();
			}

			public List<IAuthorizationChecker> FindAuthorizationCheckers( object instance )
			{
				throw new NotImplementedException();
			}

			public List<AccessCheckerAttributeDetails> FindAccessCheckers( object instance )
			{
				throw new NotImplementedException();
			}
		}

		private class DummyPrerequisitesChecker : IPrerequisitesChecker
		{
			public CheckResult Check<T>( T instance )
			{
				throw new NotImplementedException();
			}
		}

		private class DummyCommandExecutor : ICommandExecutor
		{
			public CommandResult ExecuteCommand<TCommand>( TCommand command )
			{
				throw new NotImplementedException();
			}
		}

		private class DummyMessageManager : IMessageManager
		{
			public void SetErrorMessage( string message )
			{
				throw new NotImplementedException();
			}

			public string GetErrorMessage()
			{
				throw new NotImplementedException();
			}

			public void SetSuccessMessage( string message )
			{
				throw new NotImplementedException();
			}

			public string GetSuccessMessage()
			{
				throw new NotImplementedException();
			}
		}

		private class DummyReferrerProvider : IReferrerProvider
		{
			public string GetAbsoluteUri()
			{
				throw new NotImplementedException();
			}
		}

		private class DummyObjectLookupCache : IObjectLookupCache
		{
			public T GetValue<T>( string key, T defaultValue )
			{
				throw new NotImplementedException();
			}

			public void SetValue<T>( string key, T value )
			{
				throw new NotImplementedException();
			}
		}

		private class DummyPropertyValueProviderFactory : IPropertyValueProviderFactory
		{
			public IEnumerable<IPropertyValueProvider> GetPropertyValueProviders()
			{
				throw new NotImplementedException();
			}

			public IPersistablePropertyValueProvider GetPersistablePropertyValueProvider( PersistanceLocation location )
			{
				throw new NotImplementedException();
			}
		}

		private class DummyAssemblyListProvider : IAssemblyListProvider
		{
			public IEnumerable<Assembly> GetAssemblies()
			{
				throw new NotImplementedException();
			}
		}

		private class DummyCommandBus : ICommandBus
		{
			public CommandResult Execute<TCommand>( TCommand command )
			{
				throw new NotImplementedException();
			}
		}

		private class DummyCommandHandlerFinder : ICommandHandlerFinder
		{
			public ICommandHandler<TCommand> FindHandlerForCommand<TCommand>()
			{
				throw new NotImplementedException();
			}
		}

		private class DummyCommandHandlerProvider : ICommandHandlerProvider
		{
			public ICommandHandler<TCommand> GetCommandHandler<TCommand>()
			{
				throw new NotImplementedException();
			}
		}

		private class DummyEventPublisher : IEventPublisher
		{
			public void Publish<T>( T @event )
			{
				throw new NotImplementedException();
			}

			public void PublishSynchronously<T>( T @event )
			{
				throw new NotImplementedException();
			}
		}

		private class DummySubscriptionService : ISubscriptionService
		{
			public IEnumerable<IEventSubscriber<T>> GetSubscriptions<T>()
			{
				throw new NotImplementedException();
			}
		}

		private class DummyViewModelProviderArgumentsProvider : IViewModelProviderArgumentsProvider
		{
			public TArg GetArguments<TArg>() where TArg : new()
			{
				throw new NotImplementedException();
			}

			public void Persist<TArg>() where TArg : new()
			{
				throw new NotImplementedException();
			}
		}

		#endregion

		#region Get Overridden Implementations Tests.

		[Test]
		public void CheckersExecutor_WhenOverridden_ShouldReturnOverriddenImplementationCheckersExecutor()
		{
			Type implementationCheckersExecutorType = typeof( DummyCheckersExecutor );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.CheckersExecutor<DummyCheckersExecutor>();
			WhenOverridden_ShouldReturnOverriddenImplementationType( typeof( ICheckersExecuter ),
			                                                         implementationCheckersExecutorType, getServiceToOverrideAction );
		}

		[Test]
		public void CheckersFinder_WhenOverridden_ShouldReturnOverriddenImplementationCheckersFinder()
		{
			Type implementationCheckersFinderType = typeof( DummyCheckersFinder );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.CheckersFinder<DummyCheckersFinder>();
			WhenOverridden_ShouldReturnOverriddenImplementationType( typeof( ICheckersFinder ), implementationCheckersFinderType,
			                                                         getServiceToOverrideAction );
		}

		[Test]
		public void PrerequisitesChecker_WhenOverridden_ShouldReturnOverriddenImplementationPrerequisitesChecker()
		{
			Type implementationPrerequisitesCheckerType = typeof( DummyPrerequisitesChecker );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.PrerequisitesChecker<DummyPrerequisitesChecker>();
			WhenOverridden_ShouldReturnOverriddenImplementationType( typeof( IPrerequisitesChecker ),
			                                                         implementationPrerequisitesCheckerType, getServiceToOverrideAction );
		}

		[Test]
		public void CommandExecutor_WhenOverridden_ShouldReturnOverriddenImplementationCommandExecutor()
		{
			Type implementationCommandExecutorType = typeof( DummyCommandExecutor );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.CommandExecutor<DummyCommandExecutor>();
			WhenOverridden_ShouldReturnOverriddenImplementationType( typeof( ICommandExecutor ), implementationCommandExecutorType,
			                                                         getServiceToOverrideAction );
		}

		[Test]
		public void MessageManager_WhenOverridden_ShouldReturnOverriddenImplementationMessageManager()
		{
			Type implementationMessageManagerType = typeof( DummyMessageManager );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.MessageManager<DummyMessageManager>();
			WhenOverridden_ShouldReturnOverriddenImplementationType( typeof( IMessageManager ), implementationMessageManagerType,
			                                                         getServiceToOverrideAction );
		}

		[Test]
		public void ReferrerProvider_WhenOverridden_ShouldReturnOverriddenImplementationReferrerProvider()
		{
			Type implementationReferrerProviderType = typeof( DummyReferrerProvider );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.ReferrerProvider<DummyReferrerProvider>();
			WhenOverridden_ShouldReturnOverriddenImplementationType( typeof( IReferrerProvider ),
			                                                         implementationReferrerProviderType, getServiceToOverrideAction );
		}

		[Test]
		public void ObjectLookupCache_WhenOverridden_ShouldReturnOverriddenImplementationObjectLookupCache()
		{
			Type implementationObjectLookupCacheType = typeof( DummyObjectLookupCache );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.ObjectLookupCache<DummyObjectLookupCache>();
			WhenOverridden_ShouldReturnOverriddenImplementationType( typeof( IObjectLookupCache ),
			                                                         implementationObjectLookupCacheType, getServiceToOverrideAction );
		}

		[Test]
		public void PropertyValueProviderFactory_WhenOverridden_ShouldReturnOverriddenImplementationPropertyValueProviderFactory
			()
		{
			Type implementationPropertyValueProviderFactoryType = typeof( DummyPropertyValueProviderFactory );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.PropertyValueProviderFactory<DummyPropertyValueProviderFactory>();
			WhenOverridden_ShouldReturnOverriddenImplementationType( typeof( IPropertyValueProviderFactory ),
			                                                         implementationPropertyValueProviderFactoryType, getServiceToOverrideAction );
		}

		[Test]
		public void AssemblyListProvider_WhenOverridden_ShouldReturnOverriddenImplementationAssemblyListProvider()
		{
			Type implementationAssemblyListProviderType = typeof( DummyAssemblyListProvider );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.AssemblyListProvider<DummyAssemblyListProvider>();
			WhenOverridden_ShouldReturnOverriddenImplementationType( typeof( IAssemblyListProvider ),
			                                                         implementationAssemblyListProviderType, getServiceToOverrideAction );
		}

		[Test]
		public void CommandBus_WhenOverridden_ShouldReturnOverriddenImplementationCommandBus()
		{
			Type implementationCommandBusType = typeof( DummyCommandBus );
			Action<OverridableServicesContainer> getServiceToOverrideAction = x => x.CommandBus<DummyCommandBus>();
			WhenOverridden_ShouldReturnOverriddenImplementationType( typeof( ICommandBus ), implementationCommandBusType,
			                                                         getServiceToOverrideAction );
		}

		[Test]
		public void CommandHandlerFinder_WhenOverridden_ShouldReturnOverriddenImplementationCommandHandlerFinder()
		{
			Type implementationCommandHandlerFinderType = typeof( DummyCommandHandlerFinder );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.CommandHandlerFinder<DummyCommandHandlerFinder>();
			WhenOverridden_ShouldReturnOverriddenImplementationType( typeof( ICommandHandlerFinder ),
			                                                         implementationCommandHandlerFinderType, getServiceToOverrideAction );
		}

		[Test]
		public void CommandHandlerProvider_WhenOverridden_ShouldReturnOverriddenImplementationCommandHandlerProvider()
		{
			Type implementationCommandHandlerProviderType = typeof( DummyCommandHandlerProvider );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.CommandHandlerProvider<DummyCommandHandlerProvider>();
			WhenOverridden_ShouldReturnOverriddenImplementationType( typeof( ICommandHandlerProvider ),
			                                                         implementationCommandHandlerProviderType, getServiceToOverrideAction );
		}

		[Test]
		public void EventPublisher_WhenOverridden_ShouldReturnOverriddenImplementationEventPublisher()
		{
			Type implementationEventPublisherType = typeof( DummyEventPublisher );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.EventPublisher<DummyEventPublisher>();
			WhenOverridden_ShouldReturnOverriddenImplementationType( typeof( IEventPublisher ), implementationEventPublisherType,
			                                                         getServiceToOverrideAction );
		}

		[Test]
		public void SubscriptionService_WhenOverridden_ShouldReturnOverriddenImplementationSubscriptionService()
		{
			Type implementationSubscriptionServiceType = typeof( DummySubscriptionService );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.SubscriptionService<DummySubscriptionService>();
			WhenOverridden_ShouldReturnOverriddenImplementationType( typeof( ISubscriptionService ),
			                                                         implementationSubscriptionServiceType, getServiceToOverrideAction );
		}


		[Test]
		public void
			ViewModelProviderArgumentsProvider_WhenOverridden_ShouldReturnOverriddenImplementationViewModelProviderArgumentsProvider
			()
		{
			Type implementationViewModelProviderArgumentsProviderType = typeof( DummyViewModelProviderArgumentsProvider );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.ViewModelProviderArgumentsProvider<DummyViewModelProviderArgumentsProvider>();
			WhenOverridden_ShouldReturnOverriddenImplementationType( typeof( IViewModelProviderArgumentsProvider ),
			                                                         implementationViewModelProviderArgumentsProviderType, getServiceToOverrideAction );
		}

		private static void WhenOverridden_ShouldReturnOverriddenImplementationType( Type interfaceType,
		                                                                             Type overriddenImplementationType, Action<OverridableServicesContainer> getServiceToOverride )
		{
			var configuration = new DIConfiguration();
			var mappedTypes = configuration
				.SetImplementationType( getServiceToOverride )
				.Build();

			Assert.IsTrue( mappedTypes.ContainsKey( interfaceType ) );
			Assert.AreEqual( overriddenImplementationType, mappedTypes[ interfaceType ] );
		}

		#endregion

		#region Get Default Implementation Tests.

		[Test]
		public void CheckersExecutor_WhenNotOverridden_ShouldMapDefaultImplementationCheckersExecutorType()
		{
			WhenNotOverridden_ShouldReturnDefaultImplementation( typeof( ICheckersExecuter ), typeof( CheckersExecuter ) );
		}

		[Test]
		public void CheckersFinder_WhenNotOverridden_ShouldMapDefaultImplementationCheckersFinder()
		{
			WhenNotOverridden_ShouldReturnDefaultImplementation( typeof( ICheckersFinder ), typeof( CheckersFinder ) );
		}

		[Test]
		public void PrerequisitesChecker_WhenNotOverridden_ShouldMapDefaultImplementationPrerequisitesChecker()
		{
			WhenNotOverridden_ShouldReturnDefaultImplementation( typeof( IPrerequisitesChecker ), typeof( PrerequisitesChecker ) );
		}

		[Test]
		public void CommandExecutor_WhenNotOverridden_ShouldMapDefaultImplementationCommandExecutor()
		{
			WhenNotOverridden_ShouldReturnDefaultImplementation( typeof( ICommandExecutor ), typeof( CommandExecutor ) );
		}

		[Test]
		public void MessageManager_WhenNotOverridden_ShouldMapDefaultImplementationMessageManager()
		{
			WhenNotOverridden_ShouldReturnDefaultImplementation( typeof( IMessageManager ), typeof( SessionBasedMessageManager ) );
		}

		[Test]
		public void ReferrerProvider_WhenNotOverridden_ShouldMapDefaultImplementationReferrerProvider()
		{
			WhenNotOverridden_ShouldReturnDefaultImplementation( typeof( IReferrerProvider ), typeof( ReferrerProvider ) );
		}

		[Test]
		public void ObjectLookupCache_WhenNotOverridden_ShouldMapDefaultImplementationObjectLookupCache()
		{
			WhenNotOverridden_ShouldReturnDefaultImplementation( typeof( IObjectLookupCache ), typeof( ObjectLookupCache ) );
		}

		[Test]
		public void PropertyValueProviderFactory_WhenNotOverridden_ShouldMapDefaultImplementationPropertyValueProviderFactory()
		{
			WhenNotOverridden_ShouldReturnDefaultImplementation( typeof( IPropertyValueProviderFactory ),
			                                                     typeof( PropertyValueProviderFactory ) );
		}

		[Test]
		public void AssemblyListProvider_WhenNotOverridden_ShouldMapDefaultImplementationAssemblyListProvider()
		{
			WhenNotOverridden_ShouldReturnDefaultImplementation( typeof( IAssemblyListProvider ), typeof( AssemblyListProvider ) );
		}

		[Test]
		public void CommandBus_WhenNotOverridden_ShouldMapDefaultImplementationCommandBus()
		{
			WhenNotOverridden_ShouldReturnDefaultImplementation( typeof( ICommandBus ), typeof( CommandBus ) );
		}

		[Test]
		public void CommandHandlerFinder_WhenNotOverridden_ShouldMapDefaultImplementationCommandHandlerFinder()
		{
			WhenNotOverridden_ShouldReturnDefaultImplementation( typeof( ICommandHandlerFinder ),
			                                                     typeof( CommandHandlerFinder ) );
		}

		[Test]
		public void CommandHandlerProvider_WhenNotOverridden_ShouldMapDefaultImplementationCommandHandlerProvider()
		{
			WhenNotOverridden_ShouldReturnDefaultImplementation( typeof( ICommandHandlerProvider ),
			                                                     typeof( CommandHandlerProvider ) );
		}

		[Test]
		public void EventPublisher_WhenNotOverridden_ShouldMapDefaultImplementationEventPublisher()
		{
			WhenNotOverridden_ShouldReturnDefaultImplementation( typeof( IEventPublisher ), typeof( EventPublisher ) );
		}

		[Test]
		public void SubscriptionService_WhenNotOverridden_ShouldMapDefaultImplementationSubscriptionService()
		{
			WhenNotOverridden_ShouldReturnDefaultImplementation( typeof( ISubscriptionService ), typeof( SubscriptionService ) );
		}

		[Test]
		public void
			ViewModelProviderArgumentsProvider_WhenNotOverridden_ShouldMapDefaultImplementationViewModelProviderArgumentsProvider()
		{
			WhenNotOverridden_ShouldReturnDefaultImplementation( typeof( IViewModelProviderArgumentsProvider ),
			                                                     typeof( ViewModelProviderArgumentsProvider ) );
		}


		private static void WhenNotOverridden_ShouldReturnDefaultImplementation( Type interfaceType,
		                                                                         Type defaultImplementationType )
		{
			var configuration = new DIConfiguration();
			var mappedTypes = configuration
				.Build();

			Assert.IsTrue( mappedTypes.ContainsKey( interfaceType ) );
			Assert.AreEqual( defaultImplementationType, mappedTypes[ interfaceType ] );
		}

		#endregion

		[Test]
		public void Build_ConfigureViewModelProvidersIn_ShouldMapAllViewModelProviders()
		{
			new DIConfiguration()
				.SetImplementationType( x => x.CommandBus<DummyCommandBus>() );

			var types = new DIConfiguration()
				.ConfigureViewModelProvidersIn( this.GetType().Assembly )
				.Build();

			var viewModelProviderInterfaceType = typeof( IViewModelProvider<DummyViewModel, DummyViewModelProviderArgument> );
			var viewModelProviderImplementationType = typeof( DummyViewModelProvider );
			Assert.IsTrue( types.ContainsKey( viewModelProviderInterfaceType ) );
			Assert.AreEqual( viewModelProviderImplementationType, types[ viewModelProviderInterfaceType ] );
		}

		[Test]
		public void Build_ConfigureViewModelProvidersIn_ShouldMapAllViewModelFactories()
		{
			var types = new DIConfiguration()
				.ConfigureViewModelProvidersIn( this.GetType().Assembly )
				.Build();

			var interfaceType = typeof( IViewModelFactory<DummyViewModel, DummyViewModelProviderArgument> );
			var implementationType = typeof( ViewModelFactory<DummyViewModel, DummyViewModelProviderArgument> );
			Assert.IsTrue( types.ContainsKey( interfaceType ) );
			Assert.AreEqual( implementationType, types[ interfaceType ] );
		}
	}
}