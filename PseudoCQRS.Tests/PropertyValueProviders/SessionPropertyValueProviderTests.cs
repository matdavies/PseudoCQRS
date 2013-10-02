using System.Web;
using NUnit.Framework;
using PseudoCQRS.PropertyValueProviders;
using PseudoCQRS.Tests.Helpers;

namespace PseudoCQRS.Tests.PropertyValueProviders
{
	[TestFixture]
	public class SessionPropertyValueProviderTests
	{
		private SessionPropertyValueProvider _valueProvider;

		[SetUp]
		public void Setup()
		{
			HttpContext.Current = HttpContextHelper.GetHttpContext();
			_valueProvider = new SessionPropertyValueProvider();
		}

		[Test]
		public void GetKeyShouldCreateCorrectly()
		{
			CommonPropertyValueProviderTests.GetKeyShouldReturnAccordingToFormat( _valueProvider );
		}

		[Test]
		public void HasValueShouldReturnTrueWhenValueExists()
		{
			const string testKey = "MyTestKey";
			HttpContext.Current.Session[testKey] = "";

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
			HttpContext.Current.Session[ testKey ] = value;

			var retVal = _valueProvider.GetValue( typeof( string ), testKey );

			Assert.AreEqual( value, retVal );
		}

		[Test]
		public void SetValueShouldSetValue()
		{
			const string testKey = "MyTestKey";
			const string value = "12345";
			_valueProvider.SetValue( testKey, value );

			Assert.IsTrue( (string) HttpContext.Current.Session[testKey] == value );

		}


	}
}
