using System;
using System.Web;
using NUnit.Framework;
using PseudoCQRS.PropertyValueProviders;
using PseudoCQRS.Tests.Helpers;
using HttpContextWrapper = PseudoCQRS.Mvc.HttpContextWrapper;

namespace PseudoCQRS.Tests.PropertyValueProviders
{
	[TestFixture]
	public class QueryStringPropertyValueProviderTests
	{
		private QueryStringPropertyValueProvider _valueProvider;
		private IHttpContextWrapper _httpContextWrapper;

		[SetUp]
		public void Setup()
		{
			HttpContext.Current = HttpContextHelper.GetHttpContext();
			_httpContextWrapper = new HttpContextWrapper();
			_valueProvider = new QueryStringPropertyValueProvider( _httpContextWrapper );
		}


		[Test]
		public void HasValueShouldReturnTrueWhenQueryStringContainsKey()
		{
			const string key = "id";
			const string value = "1324";
			HttpContext.Current = HttpContextHelper.GetHttpContext( String.Format( "{0}={1}", key, value ) );

			Assert.IsTrue( _valueProvider.HasValue<string>( key ) );
		}

		[Test]
		public void HasValueShouldReturnFalseWhenQueryStringDoesNotContainKey()
		{
			const string key = "id";

			HttpContext.Current = HttpContextHelper.GetHttpContext();

			Assert.IsFalse( _valueProvider.HasValue<string>( key ) );
		}

		[Test]
		public void GetValueShouldReturnValue()
		{
			const string key = "id";
			const string value = "1324";
			HttpContext.Current = HttpContextHelper.GetHttpContext( String.Format( "{0}={1}", key, value ) );

			Assert.AreEqual( value, _valueProvider.GetValue<Object>( key, typeof( string ) ) );
		}
	}
}