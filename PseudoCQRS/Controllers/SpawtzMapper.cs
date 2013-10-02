using System;
using Microsoft.Practices.ServiceLocation;

namespace PseudoCQRS.Controllers
{
	public static class SpawtzMapper
	{
		public static TTo Map<TFrom, TTo>( TFrom viewModel )
		{
			ISpawtzMappingEngine mappingEngine = null;

			try
			{
				mappingEngine = ServiceLocator.Current.GetInstance<ISpawtzMappingEngine>();
			}
			catch 
			{
				throw new ApplicationException( "ServiceLocator is null. Did you call ServiceLocator.SetLocatorProvider( {arg} ) in application initialization code? " );
			}

			if ( mappingEngine == null )
				throw new Exception( "ISpawtzMappingEngine implementation is not registered. Please register it in your initialization code." );

			return mappingEngine.Map<TFrom, TTo>( viewModel );
		}
	}
}
