using System;
using System.Collections.Generic;

namespace PseudoCQRS.PropertyValueProviders
{
    public abstract class BasePropertyValueProvider
    {
        public object ConvertValue( string value, Type propertyType )
        {
            //AM-notes: if type is IEnumerable instead of List then it will throw an error. may be we should raise an exception explaining whats the issue? or just convert this code to check for IEnumerable?
            if ( propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof ( List<> ) )
            {
                var containedType = propertyType.GetGenericArguments()[ 0 ];
                var returnValue = (System.Collections.IList)Activator.CreateInstance( ( typeof ( List<> ).MakeGenericType( containedType ) ) );
                var valueArray = value.Split( new[]
                {
                    ','
                }, StringSplitOptions.RemoveEmptyEntries );
                foreach ( var v in valueArray )
                    returnValue.Add( Convert.ChangeType( v, containedType ) );
                return returnValue;
            }

            if ( propertyType == typeof ( DateTime ) )
            {
                DateTime outVal = DateTime.MinValue;
                var possibleFormats = new string[]
                {
                    "dd/MM/yyyy", "dd MMM yyyy", "HH:mm", "hh:mm tt", "h:mm tt", "dd/MM/yyyy HH:mm", "dd/MM/yyyy hh:mm tt", "dd/MM/yyyy h:mm tt", "dd/MM/yyyy HH:mm:ss"
                };
                if ( DateTime.TryParseExact( value, possibleFormats, System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat, System.Globalization.DateTimeStyles.None, out outVal ) )
                    return outVal;
                else
                    return null;
            }

            return Convert.ChangeType( value, propertyType );
        }
    }
}