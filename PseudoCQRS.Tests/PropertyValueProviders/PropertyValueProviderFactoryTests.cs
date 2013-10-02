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
			var factroy = new PropertyValueProviderFactory();
			Assert.AreEqual( 4, factroy.GetPropertyValueProviders().Count() );
		}
	}
}
