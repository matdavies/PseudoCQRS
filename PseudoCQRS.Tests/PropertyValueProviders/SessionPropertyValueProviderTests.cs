using System;
using NUnit.Framework;
using PseudoCQRS.PropertyValueProviders;
using Rhino.Mocks;

namespace PseudoCQRS.Tests.PropertyValueProviders
{
	[TestFixture]
	public class SessionPropertyValueProviderTests
	{
		private IHttpContextWrapper _httpContextWrapper;
		private SessionPropertyValueProvider _valueProvider;

		[SetUp]
		public void Setup()
		{
			_httpContextWrapper = MockRepository.GenerateMock<IHttpContextWrapper>();
			_valueProvider = new SessionPropertyValueProvider( _httpContextWrapper );
		}

		[Test]
		public void HasValueShouldReturnTrueWhenValueExists()
		{
			const string testKey = "MyTestKey";
			const string fullKey = "System.String:" + testKey;
			_httpContextWrapper
				.Stub( x => x.ContainsSessionItem( fullKey ) )
				.Return( true );

			Assert.IsTrue( _valueProvider.HasValue<string>( testKey ) );
		}

		[Test]
		public void HasValueShouldReturnFalseWhenValueDoNotExists()
		{
			const string testKey = "MyTestKey";
			Assert.IsFalse( _valueProvider.HasValue<string>( testKey ) );
		}

		[Test]
		public void GetValueShouldReturnValue()
		{
			const string testKey = "MyTestKey";
			const string fullKey = "System.Object:" + testKey;
			const string value = "12345";

			_httpContextWrapper
				.Stub( x => x.GetSessionItem( fullKey ) )
				.Return( value );

			var retVal = _valueProvider.GetValue<Object>( testKey, typeof( string ) );

			Assert.AreEqual( value, retVal );
		}

		[Test]
		public void SetValueShouldSetValue()
		{
			const string testKey = "MyTestKey";
			const string fullKey = "System.String:" + testKey;
			const string value = "12345";

			_httpContextWrapper.Stub( x => x.SetSessionItem( fullKey, value ) );

			_valueProvider.SetValue<string>( testKey, value );

			_httpContextWrapper.AssertWasCalled( x => x.SetSessionItem( fullKey, value ) );
		}
	}
}