using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using NUnit.Framework;
using PseudoCQRS.Mvc;
using PseudoCQRS.PropertyValueProviders;
using PseudoCQRS.Tests.Helpers;
using HttpContextWrapper = PseudoCQRS.Mvc.HttpContextWrapper;

namespace PseudoCQRS.Tests.PropertyValueProviders
{
	[TestFixture]
	public class FormDataPropertyValueProviderTests
	{
		private FormDataPropertyValueProvider _valueProvider;

		[SetUp]
		public void Setup()
		{
			_valueProvider = new FormDataPropertyValueProvider(new HttpContextWrapper());
			HttpContext.Current = HttpContextHelper.GetHttpContext();
		}


		[Test]
		public void HasValueShouldReturnTrueWhenFormContainsKey()
		{
			const string key = "id";
			const string value = "1324";

			var form = new NameValueCollection();
			form.Add( key, value );
			HttpContextHelper.SetRequestForm( form );

			Assert.IsTrue( _valueProvider.HasValue<string>( key ) );
		}

		[Test]
		public void HasValueShouldReturnFalseWhenFormDoesNotContainKey()
		{
			const string key = "id";
			Assert.IsFalse( _valueProvider.HasValue<string>( key ) );
		}

		[Test]
		public void HasValueShouldReturnTrueWhen_KeyNotFoundButKeyWithExistsFound()
		{
			// if key do not exist but there is another key with same {name}_Exists then it exist
			const string keyName = "MyTestKey";

			var form = new NameValueCollection();
			form.Add( keyName + "_Exists", String.Empty );
			HttpContextHelper.SetRequestForm( form );

			Assert.IsTrue( _valueProvider.HasValue<string>( keyName ) );
		}

		[Test]
		public void GetValueShouldReturnValue()
		{
			const string key = "id";
			const string value = "1324";

			var form = new NameValueCollection();
			form.Add( key, value );
			HttpContextHelper.SetRequestForm( form );

			Assert.AreEqual( value, _valueProvider.GetValue<Object>( key, typeof( string ) ) );
		}

		[Test]
		public void GetValueShouldReturnEmptyListWhenListIsNull()
		{
			const string key = "MyKey";
			var form = new NameValueCollection();
			form.Add( key, null );
			HttpContextHelper.SetRequestForm( form );

			var retVal = _valueProvider.GetValue<Object>( key, typeof( List<string> ) );
			Assert.IsNotNull( retVal );
		}
	}
}