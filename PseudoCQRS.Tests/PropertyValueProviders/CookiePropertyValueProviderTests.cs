using System;
using System.Linq;
using System.Web;
using NUnit.Framework;
using PseudoCQRS.PropertyValueProviders;
using PseudoCQRS.Tests.Helpers;

namespace PseudoCQRS.Tests.PropertyValueProviders
{
	[TestFixture]
	public class CookiePropertyValueProviderTests
	{
		private CookiePropertyValueProvider _valueProvider;

		[SetUp]
		public void Setup()
		{
			HttpContext.Current = HttpContextHelper.GetHttpContext();
			_valueProvider = new CookiePropertyValueProvider();
		}

		[Test]
		public void HasValueShouldReturnTrueWhenValueExists()
		{
			const string testKey = "MyTestKey";
			const string fullKey = "System.String:" + testKey;
			HttpContext.Current.Request.Cookies.Add( new HttpCookie( fullKey ) );

			Assert.IsTrue( _valueProvider.HasValue<string>( testKey ) );
		}

		[Test]
		public void HasValueShouldReturnFalseWhenValueDoNotExists()
		{
			const string testKey = "MyTestKey";
			const string fullKey = "System.String:" + testKey;
			Assert.IsFalse( _valueProvider.HasValue<string>( fullKey ) );
		}

		[Test]
		public void GetValueShouldReturnValue()
		{
			const string testKey = "MyTestKey";
			const string fullKey = "System.Object:" + testKey;
			const string value = "12345";
			HttpContext.Current.Request.Cookies.Add( new HttpCookie( fullKey, value ) );
			var retVal = _valueProvider.GetValue<Object>( testKey, typeof( string ) );

			Assert.AreEqual( value, retVal );
		}

		[Test]
		public void SetValueShouldSetValueInResponse()
		{
			const string testKey = "MyTestKey";
			const string fullKey = "System.String:" + testKey;
			const string value = "12345";
			_valueProvider.SetValue<string>( testKey, value );

			Assert.IsTrue( HttpContext.Current.Response.Cookies.AllKeys.Contains( fullKey ) );
			Assert.AreEqual( value, HttpContext.Current.Response.Cookies[ fullKey ].Value );
		}
	}
}