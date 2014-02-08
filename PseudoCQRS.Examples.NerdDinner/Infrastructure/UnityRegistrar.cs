using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using PseudoCQRS.Controllers;
using PseudoCQRS.Examples.NerdDinner.Modules;
using PseudoCQRS.Examples.NerdDinner.Modules.DinnerCreate;
using PseudoCQRS.Examples.NerdDinner.Modules.DinnerList;

namespace PseudoCQRS.Examples.NerdDinner.Infrastructure
{
	public static class UnityRegistrar
	{
		public static void Register( IUnityContainer container )
		{
			RegisterNonGenericImplementationsWithITypeName( container, typeof( ICommandBus ).Assembly );
			container.RegisterType<IMessageManager, SessionBasedMessageManager>();
			container.RegisterType( typeof( IViewModelFactory<,> ), typeof( ViewModelFactory<,> ) );
			container.RegisterType<IEventSubscriberAssembliesProvider, EventSubscriberAssembliesProvider>();
			container.RegisterType<IViewModelToCommandMappingEngine, NerdDinnerMappingEngine>();
			container.RegisterType<IDbSessionManager, NerdDinnerDbSessionManager>();
			container.RegisterType<IViewModelProvider<DinnerCreateViewModel, EmptyViewModelProviderArgument>, DinnerCreateViewModelProvider>();
			container.RegisterType<IViewModelProvider<List<DinnerListViewModel>, DinnerListArguments>, DinnerListViewModelProvider>();
			container.RegisterType<IRepository, Repository>();

			ServiceLocator.SetLocatorProvider( () => new UnityServiceLocator( container ) );
			container.RegisterInstance<IServiceLocator>( ServiceLocator.Current );
		}

		private static void RegisterNonGenericImplementationsWithITypeName( IUnityContainer container, Assembly inAssembly )
		{
			var typesWithITypeInterfaces = inAssembly.GetTypes()
												   .Where( x => x.IsClass && !x.IsAbstract && x.IsPublic )
												   .Where( x => x.FindInterfaces( ( y, z ) => y.Name == "I" + x.Name, null ).Any() );

			foreach ( var implementation in typesWithITypeInterfaces )
			{
				var interfaceType = implementation.GetInterface( "I" + implementation.Name );
				container.RegisterType( interfaceType, implementation );
			}
		}
	}
}