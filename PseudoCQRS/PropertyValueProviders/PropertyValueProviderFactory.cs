using System.Collections.Generic;

namespace PseudoCQRS.PropertyValueProviders
{
	public class PropertyValueProviderFactory : IPropertyValueProviderFactory
	{
		private readonly List<IPropertyValueProvider> _propertyValueProviders = new List<IPropertyValueProvider>()
		{
			new SessionPropertyValueProvider(),
			new RouteDataPropertyValueProvider(),
			new QueryStringPropertyValueProvider(),
			new FormDataPropertyValueProvider()
		};

		public IEnumerable<IPropertyValueProvider> GetPropertyValueProviders()
		{
			return _propertyValueProviders;
		}
	}
}