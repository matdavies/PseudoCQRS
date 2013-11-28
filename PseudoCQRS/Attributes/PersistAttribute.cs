using System;
using PseudoCQRS.PropertyValueProviders;

namespace PseudoCQRS.Attributes
{
    [AttributeUsage( AttributeTargets.Property, Inherited = false, AllowMultiple = false )]
    public class PersistAttribute : Attribute
    {
        internal PersistanceLocation PersistanceLocation { get; set; }

        public PersistAttribute( PersistanceLocation persistanceLocation )
        {
            PersistanceLocation = persistanceLocation;
        }
    }
}