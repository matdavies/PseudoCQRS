using System;
using System.Web;
using NUnit.Framework;
using PseudoCQRS.PropertyValueProviders;
using PseudoCQRS.Tests.Helpers;

namespace PseudoCQRS.Tests.PropertyValueProviders
{
	[TestFixture]
	public class QueryStringPropertyValueProviderTests
	{
		private QueryStringPropertyValueProvider _valueProvider;

		[SetUp]
		public void Setup()
		{
			_valueProvider = new QueryStringPropertyValueProvider();
		}

		[Test]
		public void GetKeyShouldReturnPropertyNameAsKey()
		{
			CommonPropertyValueProviderTests.GetKeyShouldReturnPropertyNameAsKey( _valueProvider );
		}

		[Test]
		public void HasValueShouldReturnTrueWhenQueryStringContainsKey()
		{
			const string key = "id";
			const string value = "1324";
			HttpContext.Current = HttpContextHelper.GetHttpContext( String.Format( "{0}={1}", key, value ) );

			Assert.IsTrue( _valueProvider.HasValue( key ) );
		}

		[Test]
		public void HasValueShouldReturnFalseWhenQueryStringDoesNotContainKey()
		{
			const string key = "id";

			HttpContext.Current = HttpContextHelper.GetHttpContext();

			Assert.IsFalse( _valueProvider.HasValue( key ) );
		}

		[Test]
		public void GetValueShouldReturnValue()
		{
			const string key = "id";
			const string value = "1324";
			HttpContext.Current = HttpContextHelper.GetHttpContext( String.Format( "{0}={1}", key, value ) );

			Assert.AreEqual( value, _valueProvider.GetValue( typeof(string), key ) );
			
		}
	}
}
