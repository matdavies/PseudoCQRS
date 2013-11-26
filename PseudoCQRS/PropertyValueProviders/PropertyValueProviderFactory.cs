using System.Collections.Generic;

namespace PseudoCQRS.PropertyValueProviders
{
    public class PropertyValueProviderFactory : IPropertyValueProviderFactory
    {
        private readonly List<IPropertyValueProvider> _propertyValueProviders = new List<IPropertyValueProvider>()
        {
            new CookiePropertyValueProvider(),
            new SessionPropertyValueProvider(),
            new RouteDataPropertyValueProvider(),
            new QueryStringPropertyValueProvider(),
            new FormDataPropertyValueProvider()
        };

        public IEnumerable<IPropertyValueProvider> GetPropertyValueProviders()
        {
            return _propertyValueProviders;
        }

        public IPersistablePropertyValueProvider GetPersistablePropertyValueProvider( PersistanceLocation location )
        {
            IPersistablePropertyValueProvider result = null;
            switch ( location )
            {
                case PersistanceLocation.Cookie:
                    result = new CookiePropertyValueProvider();
                    break;
                case PersistanceLocation.Session:
                    result = new SessionPropertyValueProvider();
                    break;
            }

            return result;
        }
    }
}