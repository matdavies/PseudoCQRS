using System;
using Microsoft.Practices.ServiceLocation;

namespace PseudoCQRS.Controllers
{
	public static class Mapper
	{
		public static TTo Map<TFrom, TTo>( TFrom viewModel )
		{
			IViewModelToCommandMappingEngine mappingEngine = null;

			try
			{
				mappingEngine = ServiceLocator.Current.GetInstance<IViewModelToCommandMappingEngine>();
			}
			catch 
			{
				throw new ApplicationException( "ServiceLocator is null. Did you call ServiceLocator.SetLocatorProvider( {arg} ) in application initialization code? " );
			}

			if ( mappingEngine == null )
				throw new Exception( "IViewModelToCommandMappingEngine implementation is not registered. Please register it in your initialization code." );

			return mappingEngine.Map<TFrom, TTo>( viewModel );
		}
	}
}
