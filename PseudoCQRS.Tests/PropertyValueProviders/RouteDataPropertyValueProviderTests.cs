using System.Web;
using NUnit.Framework;
using PseudoCQRS.PropertyValueProviders;
using PseudoCQRS.Tests.Helpers;

namespace PseudoCQRS.Tests.PropertyValueProviders
{
	[TestFixture]
	public class RouteDataPropertyValueProviderTests
	{
		private RouteDataPropertyValueProvider _valueProvider;

		[SetUp]
		public void Setup()
		{
			_valueProvider = new RouteDataPropertyValueProvider();
			HttpContext.Current = HttpContextHelper.GetHttpContext();
		}

		[Test]
		public void GetKeyShouldReturnPropertyName()
		{
			CommonPropertyValueProviderTests.GetKeyShouldReturnPropertyNameAsKey( _valueProvider );
		}

		[Test]
		public void HasValueShouldReturnTrueWhenFormContainsKey()
		{
			const string key = "id";
			const string value = "1324";
			HttpContext.Current.Request.RequestContext.RouteData.Values.Add(key, value);

			Assert.IsTrue( _valueProvider.HasValue( key ) );
		}

		[Test]
		public void HasValueShouldReturnFalseWhenFormDoesNotContainKey()
		{
			const string key = "id";
			Assert.IsFalse( _valueProvider.HasValue( key ) );
		}


		[Test]
		public void GetValueShouldReturnValue()
		{
			const string key = "id";
			const string value = "1324";
			HttpContext.Current.Request.RequestContext.RouteData.Values.Add( key, value );

			Assert.AreEqual( value, _valueProvider.GetValue( typeof( string ), key ) );

		}


	}
}
