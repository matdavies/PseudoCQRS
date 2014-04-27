using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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

		private class DummyViewModel
		{
		}

		private class DummyViewModelProviderArgument
		{
		}

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

		#region Get Overriden Implementations Tests.

		[Test]
		public void CheckersExecutor_WhenOverriden_ShouldReturnOverridenImplementationCheckersExecutor()
		{
			Type implementationCheckersExecutorType = typeof ( DummyCheckersExecutor );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.CheckersExecuter( implementationCheckersExecutorType );
			WhenOverriden_ShouldReturnOverridenImplementationType( typeof ( ICheckersExecuter ),
																	implementationCheckersExecutorType, getServiceToOverrideAction );
		}

		[Test]
		public void CheckersFinder_WhenOverriden_ShouldReturnOverridenImplementationCheckersFinder()
		{
			Type implementationCheckersFinderType = typeof ( DummyCheckersFinder );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.CheckersFinder( implementationCheckersFinderType );
			WhenOverriden_ShouldReturnOverridenImplementationType( typeof ( ICheckersFinder ), implementationCheckersFinderType,
																	getServiceToOverrideAction );
		}

		[Test]
		public void PrerequisitesChecker_WhenOverriden_ShouldReturnOverridenImplementationPrerequisitesChecker()
		{
			Type implementationPrerequisitesCheckerType = typeof ( DummyPrerequisitesChecker );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.PrerequisitesChecker( implementationPrerequisitesCheckerType );
			WhenOverriden_ShouldReturnOverridenImplementationType( typeof ( IPrerequisitesChecker ),
																	implementationPrerequisitesCheckerType, getServiceToOverrideAction );
		}

		[Test]
		public void CommandExecutor_WhenOverriden_ShouldReturnOverridenImplementationCommandExecutor()
		{
			Type implementationCommandExecutorType = typeof ( DummyCommandExecutor );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.CommandExecutor( implementationCommandExecutorType );
			WhenOverriden_ShouldReturnOverridenImplementationType( typeof ( ICommandExecutor ), implementationCommandExecutorType,
																	getServiceToOverrideAction );
		}

		[Test]
		public void MessageManager_WhenOverriden_ShouldReturnOverridenImplementationMessageManager()
		{
			Type implementationMessageManagerType = typeof ( DummyMessageManager );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.MessageManager( implementationMessageManagerType );
			WhenOverriden_ShouldReturnOverridenImplementationType( typeof ( IMessageManager ), implementationMessageManagerType,
																	getServiceToOverrideAction );
		}

		[Test]
		public void ReferrerProvider_WhenOverriden_ShouldReturnOverridenImplementationReferrerProvider()
		{
			Type implementationReferrerProviderType = typeof ( DummyReferrerProvider );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.ReferrerProvider( implementationReferrerProviderType );
			WhenOverriden_ShouldReturnOverridenImplementationType( typeof ( IReferrerProvider ),
																	implementationReferrerProviderType, getServiceToOverrideAction );
		}

		[Test]
		public void ObjectLookupCache_WhenOverriden_ShouldReturnOverridenImplementationObjectLookupCache()
		{
			Type implementationObjectLookupCacheType = typeof ( DummyObjectLookupCache );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.ObjectLookupCache( implementationObjectLookupCacheType );
			WhenOverriden_ShouldReturnOverridenImplementationType( typeof ( IObjectLookupCache ),
																	implementationObjectLookupCacheType, getServiceToOverrideAction );
		}

		[Test]
		public void PropertyValueProviderFactory_WhenOverriden_ShouldReturnOverridenImplementationPropertyValueProviderFactory
			()
		{
			Type implementationPropertyValueProviderFactoryType = typeof ( DummyPropertyValueProviderFactory );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.PropertyValueProviderFactory( implementationPropertyValueProviderFactoryType );
			WhenOverriden_ShouldReturnOverridenImplementationType( typeof ( IPropertyValueProviderFactory ),
																	implementationPropertyValueProviderFactoryType, getServiceToOverrideAction );
		}

		[Test]
		public void AssemblyListProvider_WhenOverriden_ShouldReturnOverridenImplementationAssemblyListProvider()
		{
			Type implementationAssemblyListProviderType = typeof ( DummyAssemblyListProvider );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.AssemblyListProvider( implementationAssemblyListProviderType );
			WhenOverriden_ShouldReturnOverridenImplementationType( typeof ( IAssemblyListProvider ),
																	implementationAssemblyListProviderType, getServiceToOverrideAction );
		}

		[Test]
		public void CommandBus_WhenOverriden_ShouldReturnOverridenImplementationCommandBus()
		{
			Type implementationCommandBusType = typeof ( DummyCommandBus );
			Action<OverridableServicesContainer> getServiceToOverrideAction = x => x.CommandBus( implementationCommandBusType );
			WhenOverriden_ShouldReturnOverridenImplementationType( typeof ( ICommandBus ), implementationCommandBusType,
																	getServiceToOverrideAction );
		}

		[Test]
		public void CommandHandlerFinder_WhenOverriden_ShouldReturnOverridenImplementationCommandHandlerFinder()
		{
			Type implementationCommandHandlerFinderType = typeof ( DummyCommandHandlerFinder );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.CommandHandlerFinder( implementationCommandHandlerFinderType );
			WhenOverriden_ShouldReturnOverridenImplementationType( typeof ( ICommandHandlerFinder ),
																	implementationCommandHandlerFinderType, getServiceToOverrideAction );
		}

		[Test]
		public void CommandHandlerProvider_WhenOverriden_ShouldReturnOverridenImplementationCommandHandlerProvider()
		{
			Type implementationCommandHandlerProviderType = typeof ( DummyCommandHandlerProvider );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.CommandHandlerProvider( implementationCommandHandlerProviderType );
			WhenOverriden_ShouldReturnOverridenImplementationType( typeof ( ICommandHandlerProvider ),
																	implementationCommandHandlerProviderType, getServiceToOverrideAction );
		}

		[Test]
		public void EventPublisher_WhenOverriden_ShouldReturnOverridenImplementationEventPublisher()
		{
			Type implementationEventPublisherType = typeof ( DummyEventPublisher );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.EventPublisher( implementationEventPublisherType );
			WhenOverriden_ShouldReturnOverridenImplementationType( typeof ( IEventPublisher ), implementationEventPublisherType,
																	getServiceToOverrideAction );
		}

		[Test]
		public void SubscriptionService_WhenOverriden_ShouldReturnOverridenImplementationSubscriptionService()
		{
			Type implementationSubscriptionServiceType = typeof ( DummySubscriptionService );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.SubscriptionService( implementationSubscriptionServiceType );
			WhenOverriden_ShouldReturnOverridenImplementationType( typeof ( ISubscriptionService ),
																	implementationSubscriptionServiceType, getServiceToOverrideAction );
		}

		//[Test]
		//public void ViewModelFactory_WhenOverriden_ShouldReturnOverridenImplementationViewModelFactory()
		//{
		//	Type implementationViewModelFactoryType = typeof( DummyViewModelFactory<DummyViewModel,DummyViewModelProviderArgument> );
		//	Action<OverridableServicesContainer> getServiceToOverrideAction = x => x.ViewModelFactory( implementationViewModelFactoryType );
		//	WhenOverriden_ShouldReturnOverridenImplementationType( typeof( IViewModelFactory<DummyViewModel, DummyViewModelProviderArgument> ), implementationViewModelFactoryType, getServiceToOverrideAction );
		//}

		[Test]
		public void
			ViewModelProviderArgumentsProvider_WhenOverriden_ShouldReturnOverridenImplementationViewModelProviderArgumentsProvider
			()
		{
			Type implementationViewModelProviderArgumentsProviderType = typeof ( DummyViewModelProviderArgumentsProvider );
			Action<OverridableServicesContainer> getServiceToOverrideAction =
				x => x.ViewModelProviderArgumentsProvider( implementationViewModelProviderArgumentsProviderType );
			WhenOverriden_ShouldReturnOverridenImplementationType( typeof ( IViewModelProviderArgumentsProvider ),
																	implementationViewModelProviderArgumentsProviderType, getServiceToOverrideAction );
		}

		private static void WhenOverriden_ShouldReturnOverridenImplementationType( Type interfaceType,
																					Type overridenImplementationType, Action<OverridableServicesContainer> getServiceToOverride )
		{
			var configuration = new DIConfiguration();
			var mappedTypes = configuration
				.SetImplementationType( getServiceToOverride )
				.Build();

			Assert.IsTrue( mappedTypes.ContainsKey( interfaceType ) );
			Assert.AreEqual( overridenImplementationType, mappedTypes[interfaceType] );
		}

		#endregion

		#region Get Default Implementation Tests.

		[Test]
		public void CheckersExecutor_WhenNotOverriden_ShouldMapDefaultImplementationCheckersExecutorType()
		{
			WhenNotOverriden_ShouldReturnDefaultImplementation( typeof ( ICheckersExecuter ), typeof ( CheckersExecuter ) );
		}

		[Test]
		public void CheckersFinder_WhenNotOverriden_ShouldMapDefaultImplementationCheckersFinder()
		{
			WhenNotOverriden_ShouldReturnDefaultImplementation( typeof ( ICheckersFinder ), typeof ( CheckersFinder ) );
		}

		[Test]
		public void PrerequisitesChecker_WhenNotOverriden_ShouldMapDefaultImplementationPrerequisitesChecker()
		{
			WhenNotOverriden_ShouldReturnDefaultImplementation( typeof ( IPrerequisitesChecker ), typeof ( PrerequisitesChecker ) );
		}

		[Test]
		public void CommandExecutor_WhenNotOverriden_ShouldMapDefaultImplementationCommandExecutor()
		{
			WhenNotOverriden_ShouldReturnDefaultImplementation( typeof ( ICommandExecutor ), typeof ( CommandExecutor ) );
		}

		[Test]
		public void MessageManager_WhenNotOverriden_ShouldMapDefaultImplementationMessageManager()
		{
			WhenNotOverriden_ShouldReturnDefaultImplementation( typeof ( IMessageManager ), typeof ( SessionBasedMessageManager ) );
		}

		[Test]
		public void ReferrerProvider_WhenNotOverriden_ShouldMapDefaultImplementationReferrerProvider()
		{
			WhenNotOverriden_ShouldReturnDefaultImplementation( typeof ( IReferrerProvider ), typeof ( ReferrerProvider ) );
		}

		[Test]
		public void ObjectLookupCache_WhenNotOverriden_ShouldMapDefaultImplementationObjectLookupCache()
		{
			WhenNotOverriden_ShouldReturnDefaultImplementation( typeof ( IObjectLookupCache ), typeof ( ObjectLookupCache ) );
		}

		[Test]
		public void PropertyValueProviderFactory_WhenNotOverriden_ShouldMapDefaultImplementationPropertyValueProviderFactory()
		{
			WhenNotOverriden_ShouldReturnDefaultImplementation( typeof ( IPropertyValueProviderFactory ),
																typeof ( PropertyValueProviderFactory ) );
		}

		[Test]
		public void AssemblyListProvider_WhenNotOverriden_ShouldMapDefaultImplementationAssemblyListProvider()
		{
			WhenNotOverriden_ShouldReturnDefaultImplementation( typeof ( IAssemblyListProvider ), typeof ( AssemblyListProvider ) );
		}

		[Test]
		public void CommandBus_WhenNotOverriden_ShouldMapDefaultImplementationCommandBus()
		{
			WhenNotOverriden_ShouldReturnDefaultImplementation( typeof ( ICommandBus ), typeof ( CommandBus ) );
		}

		[Test]
		public void CommandHandlerFinder_WhenNotOverriden_ShouldMapDefaultImplementationCommandHandlerFinder()
		{
			WhenNotOverriden_ShouldReturnDefaultImplementation( typeof ( ICommandHandlerFinder ),
																typeof ( ICommandHandlerFinder ) );
		}

		[Test]
		public void CommandHandlerProvider_WhenNotOverriden_ShouldMapDefaultImplementationCommandHandlerProvider()
		{
			WhenNotOverriden_ShouldReturnDefaultImplementation( typeof ( ICommandHandlerProvider ),
																typeof ( CommandHandlerProvider ) );
		}

		[Test]
		public void EventPublisher_WhenNotOverriden_ShouldMapDefaultImplementationEventPublisher()
		{
			WhenNotOverriden_ShouldReturnDefaultImplementation( typeof ( IEventPublisher ), typeof ( EventPublisher ) );
		}

		[Test]
		public void SubscriptionService_WhenNotOverriden_ShouldMapDefaultImplementationSubscriptionService()
		{
			WhenNotOverriden_ShouldReturnDefaultImplementation( typeof ( ISubscriptionService ), typeof ( SubscriptionService ) );
		}

		[Test]
		public void
			ViewModelProviderArgumentsProvider_WhenNotOverriden_ShouldMapDefaultImplementationViewModelProviderArgumentsProvider()
		{
			WhenNotOverriden_ShouldReturnDefaultImplementation( typeof ( IViewModelProviderArgumentsProvider ),
																typeof ( ViewModelProviderArgumentsProvider ) );
		}


		private static void WhenNotOverriden_ShouldReturnDefaultImplementation( Type interfaceType,
																				Type defaultImplementationType )
		{
			var configuration = new DIConfiguration();
			var mappedTypes = configuration
				.Build();

			Assert.IsTrue( mappedTypes.ContainsKey( interfaceType ) );
			Assert.AreEqual( defaultImplementationType, mappedTypes[interfaceType] );
		}

		#endregion


		[Test]
		public void Build_ConfigureViewModelProvidersIn_ShouldMapAllViewModelProviders()
		{
			var types = new DIConfiguration()
				.ConfigureViewModelProvidersIn( this.GetType().Assembly )
				.Build();

			var viewModelProviderInterfaceType = typeof ( IViewModelProvider<DummyViewModel, DummyViewModelProviderArgument> );
			var viewModelProviderImplementationType = typeof ( DummyViewModelProvider );
			Assert.IsTrue( types.ContainsKey( viewModelProviderInterfaceType ) );
			Assert.AreEqual( viewModelProviderImplementationType,  types[viewModelProviderInterfaceType] );
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
			Assert.AreEqual( implementationType, types[interfaceType] );			
		}

	}
}
