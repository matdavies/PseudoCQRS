using System.Linq;
using NUnit.Framework;
using PseudoCQRS.PropertyValueProviders;

namespace PseudoCQRS.Tests.PropertyValueProviders
{
	[TestFixture]
	public class PropertyValueProviderFactoryTests
	{
		[Test]
		public void ShouldReturnAllPropertyValueProviders()
		{
			var factory = new PropertyValueProviderFactory();
			Assert.AreEqual( 5, factory.GetPropertyValueProviders().Count() );
		}

		[Test]
		public void GetPersistablePropertyValueProviders_PersistanceLocationIsCookie_ReturnsCookiePersistablePropertyValueProvider()
		{
			var factory = new PropertyValueProviderFactory();
			Assert.IsInstanceOf<CookiePropertyValueProvider>( factory.GetPersistablePropertyValueProvider( PersistanceLocation.Cookie ) );
		}

		[Test]
		public void GetPersistablePropertyValueProvider_PersistanceLocationIsSession_ReturnsSessionPropertyValueProvider()
		{
			var factory = new PropertyValueProviderFactory();
			Assert.IsInstanceOf<SessionPropertyValueProvider>( factory.GetPersistablePropertyValueProvider( PersistanceLocation.Session ) );
		}
	}
}