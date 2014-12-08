using System;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using PseudoCQRS.Controllers;
using PseudoCQRS.ViewHelpers;
using Rhino.Mocks;

namespace PseudoCQRS.Tests.ViewHelpers
{
	[TestFixture]
	public class MessageUtilitiesTests
	{
		private IMessageManager _messageManager;
		private IServiceLocator _serviceLocator;

		[SetUp]
		public void Setup()
		{
			_messageManager = MockRepository.GenerateMock<IMessageManager>();
			_serviceLocator = MockRepository.GenerateMock<IServiceLocator>();
			ServiceLocator.SetLocatorProvider( () => null );
		}

		private void Arrange( IMessageManager messageManager = null )
		{
			ServiceLocator.SetLocatorProvider( () => _serviceLocator );
			if ( messageManager != null )
				_serviceLocator
					.Stub( x => x.GetInstance<IMessageManager>() )
					.Return( _messageManager );
		}

		[Test]
		public void ShouldRaiseApplicationExceptionIfServiceLocatorNotRegisterred()
		{
			Assert.Throws<ApplicationException>( () => MessageUtilities.GetErrorMessage( new HtmlHelper( new ViewContext(), new ViewPage() ) ) );
		}

		[Test]
		public void ShouldReturnErrorWhenMessageManagerIsNotRegisteredInDiContainer()
		{
			const string errorMessage = "Error MEssage";
			Arrange();
			_messageManager
				.Stub( x => x.GetErrorMessage() )
				.Return( errorMessage );

			var retVal = MessageUtilities.GetErrorMessage( new HtmlHelper( new ViewContext(), new ViewPage() ) );
			Assert.AreNotEqual( errorMessage, retVal.ToHtmlString() );
		}


		[Test]
		public void GetErrorMessageShouldReturnErrorMessage()
		{
			const string errorMessage = "Error MEssage";
			Arrange( _messageManager );
			_messageManager
				.Stub( x => x.GetErrorMessage() )
				.Return( errorMessage );

			var retVal = MessageUtilities.GetErrorMessage( new HtmlHelper( new ViewContext(), new ViewPage() ) );
			Assert.AreEqual( errorMessage, retVal.ToHtmlString() );
		}
	}
}