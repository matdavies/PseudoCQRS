using System;
using System.Linq.Expressions;
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
            var properties = _propertyInfoCacher.GetProperties( typeof ( TArg ) );
            var propertyValueProviders = _propertyValueProviderFactory.GetPropertyValueProviders();
            foreach ( var propertyValueProvider in propertyValueProviders )
            {
                foreach ( var kvp in properties )
                {
                    if ( propertyValueProvider.HasValue<TArg>( kvp.Key ) )
                        kvp.Value.SetValue( retVal, propertyValueProvider.GetValue<TArg>( kvp.Key,kvp.Value.PropertyType  ), null );
                }
            }
            return retVal;
        }

        public void Persist<TArg>( Expression<Func<TArg, object>> expression, PersistanceLocation location ) where TArg : new()
        {
            var persistablePropertyKeyValueProvider = _propertyValueProviderFactory.GetPersistablePropertyValueProvider( location );

            var propertyName = expression.GetMemberNameFromExpression();
            var val = expression.Compile()( GetArguments<TArg>() );

            persistablePropertyKeyValueProvider.SetValue<TArg>( propertyName, val );
        }
    }
}