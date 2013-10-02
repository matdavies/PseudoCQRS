using PseudoCQRS.PropertyValueProviders;

namespace PseudoCQRS
{
	public class ViewModelProviderArgumentsProvider : IViewModelProviderArgumentsProvider
	{

		private readonly IPropertyValueProviderFactory _propertyValueProviderFactory;

		public ViewModelProviderArgumentsProvider( IPropertyValueProviderFactory propertyValueProviderFactory )
		{
			_propertyValueProviderFactory = propertyValueProviderFactory;
		}

		private readonly PropertyInfoCacher _propertyInfoCacher = new PropertyInfoCacher();

		public TArg GetArguments<TArg>() where TArg : new()
		{
			var retVal = new TArg();
			var properties = _propertyInfoCacher.GetProperties( typeof( TArg ) );
			var propertyValueProviders = _propertyValueProviderFactory.GetPropertyValueProviders();
			foreach ( var propertyValueProvider in propertyValueProviders )
			{
				foreach ( var kvp in properties )
				{
					var key = propertyValueProvider.GetKey( typeof( TArg ), kvp.Key );
					if ( propertyValueProvider.HasValue( key ) )
						kvp.Value.SetValue( retVal, propertyValueProvider.GetValue( kvp.Value.PropertyType, key ), null );
				}
			}
			return retVal;
		}

	}
}