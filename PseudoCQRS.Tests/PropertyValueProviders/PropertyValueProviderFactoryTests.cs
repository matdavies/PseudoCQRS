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
	}
}
