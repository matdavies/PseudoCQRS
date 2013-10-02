using System;
using NUnit.Framework;
using PseudoCQRS.PropertyValueProviders;

namespace PseudoCQRS.Tests.PropertyValueProviders
{
	public static class CommonPropertyValueProviderTests
	{
		public static void GetKeyShouldReturnPropertyNameAsKey<T>( T valueProvider ) where T : IPropertyValueProvider
		{
			const string propertyName = "testProperty";
			var retVal = valueProvider.GetKey( typeof( string ), propertyName );

			Assert.AreEqual( propertyName, retVal );
		}

		public static void GetKeyShouldReturnAccordingToFormat<T>( T valueProvider ) where T : IPropertyValueProvider
		{
			const string keyFormat = @"{0}:{1}";
			const string propertyName = "Testing";
			var result = valueProvider.GetKey( typeof( string ), propertyName );
			var expected = String.Format( keyFormat, typeof( string ).FullName, propertyName );

			Assert.AreEqual( expected, result );

		}
	}
}
