using System.Web;
using NUnit.Framework;
using PseudoCQRS.PropertyValueProviders;
using PseudoCQRS.Tests.Helpers;
using HttpContextWrapper = PseudoCQRS.Mvc.HttpContextWrapper;

namespace PseudoCQRS.Tests.PropertyValueProviders
{
	[TestFixture]
	public class RouteDataPropertyValueProviderTests
	{
		private RouteDataPropertyValueProvider _valueProvider;
		private IHttpContextWrapper _wrapper;

		[SetUp]
		public void Setup()
		{
			_wrapper = new HttpContextWrapper();
			_valueProvider = new RouteDataPropertyValueProvider( _wrapper );
			HttpContext.Current = HttpContextHelper.GetHttpContext();
		}


		[Test]
		public void HasValueShouldReturnTrueWhenFormContainsKey()
		{
			const string key = "id";
			const string value = "1324";
			HttpContext.Current.Request.RequestContext.RouteData.Values.Add( key, value );

			Assert.IsTrue( _valueProvider.HasValue<string>( key ) );
		}

		[Test]
		public void HasValueShouldReturnFalseWhenFormDoesNotContainKey()
		{
			const string key = "id";
			Assert.IsFalse( _valueProvider.HasValue<string>( key ) );
		}


		[Test]
		public void GetValueShouldReturnValue()
		{
			const string key = "id";
			const string value = "1324";
			HttpContext.Current.Request.RequestContext.RouteData.Values.Add( key, value );

			Assert.AreEqual( value, _valueProvider.GetValue<object>( key, typeof(string) ) );
		}
	}
}