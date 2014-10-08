using System;
using System.Web;
using NUnit.Framework;
using PseudoCQRS.Controllers;
using PseudoCQRS.Tests.Helpers;

namespace PseudoCQRS.Tests.Controllers
{
	[TestFixture]
	public class SessionBasedMessageManagerTests
	{
		private const string ErrorKey = "SessionBasedMessageManager_ErrorKey";
		private const string SuccessKey = "SessionBasedMessageManager_SuccessKey";

		private const string ErrorMessage = @"Error";
		private const string SuccessMessage = @"Success";

		private SessionBasedMessageManager _sessionBasedMessageManager;

		[SetUp]
		public void Setup()
		{
			HttpContext.Current = HttpContextHelper.GetHttpContext();
			_sessionBasedMessageManager = new SessionBasedMessageManager();
		}

		[Test]
		public void SetErrorMessage_ShouldSaveMessageUsingErrorKey()
		{
			_sessionBasedMessageManager.SetErrorMessage( ErrorMessage );

			Assert.AreEqual( ErrorMessage, HttpContext.Current.Session[ ErrorKey ] );
		}

		[Test]
		public void SetSuccessMessage_ShouldSaveMessageUsingSuccessKey()
		{
			_sessionBasedMessageManager.SetSuccessMessage( SuccessMessage );

			Assert.AreEqual( SuccessMessage, HttpContext.Current.Session[ SuccessKey ] );
		}

		[Test]
		public void GetErrorMessage_ShouldGetMessageUsingErrorKey()
		{
			_sessionBasedMessageManager.SetSuccessMessage( SuccessMessage );
			_sessionBasedMessageManager.SetErrorMessage( ErrorMessage );

			Assert.AreEqual( ErrorMessage, _sessionBasedMessageManager.GetErrorMessage() );
		}

		[Test]
		public void GetSuccessMessage_ShouldGetMessageUsingSuccessKey()
		{
			_sessionBasedMessageManager.SetSuccessMessage( SuccessMessage );
			_sessionBasedMessageManager.SetErrorMessage( ErrorMessage );

			Assert.AreEqual( SuccessMessage, _sessionBasedMessageManager.GetSuccessMessage() );
		}

		[Test]
		public void GetErrorMessageShouldReturnEmptyStringWhenNoErrorMessageExists()
		{
			Assert.AreEqual( String.Empty, _sessionBasedMessageManager.GetErrorMessage() );
		}

		[Test]
		public void GetSuccessMessage_ShouldReturnEmptyStringWhenNoErrorMessageExists()
		{
			Assert.AreEqual( String.Empty, _sessionBasedMessageManager.GetSuccessMessage() );
		}
	}
}