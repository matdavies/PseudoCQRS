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
		private const string KeyFormat = @"{0}:{1}";

		private CookiePropertyValueProvider _valueProvider;

		[SetUp]
		public void Setup()
		{
			HttpContext.Current = HttpContextHelper.GetHttpContext();
			_valueProvider = new CookiePropertyValueProvider();
		}

		private class TempClass
		{
		}

		[Test]
		public void GetKeyShouldCreateCorrectly()
		{
			var klassType = typeof ( TempClass );
			const string propertyName = "Testing";
			var result = _valueProvider.GetKey( klassType , propertyName );
			var expected = String.Format( KeyFormat, klassType.FullName, propertyName );

			Assert.AreEqual( expected, result );
		}

		[Test]
		public void HasValueShouldReturnTrueWhenValueExists()
		{
			const string testKey = "MyTestKey";
			HttpContext.Current.Request.Cookies.Add( new HttpCookie( testKey ) );

			Assert.IsTrue( _valueProvider.HasValue( testKey ) );
		}

		[Test]
		public void HasValueShouldReturnFalseWhenValueDoNotExists()
		{
			const string testKey = "MyTestKey";
			Assert.IsFalse( _valueProvider.HasValue( testKey ) );			
		}

		[Test]
		public void GetValueShouldReturnValue()
		{
			const string testKey = "MyTestKey";
			const string value = "12345";
			HttpContext.Current.Request.Cookies.Add( new HttpCookie( testKey, value ) );
			var retVal = _valueProvider.GetValue( typeof ( string ), testKey );

			Assert.AreEqual( value, retVal );
		}

		[Test]
		public void SetValueShouldSetValueInResponse()
		{
			const string testKey = "MyTestKey";
			const string value = "12345";
			_valueProvider.SetValue( testKey, value );

			Assert.IsTrue( HttpContext.Current.Response.Cookies.AllKeys.Contains( testKey ) );
			Assert.AreEqual( value, HttpContext.Current.Response.Cookies[testKey].Value );

		}
	}
}
