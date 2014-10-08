using System;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using PseudoCQRS.Controllers;

namespace PseudoCQRS.ViewHelpers
{
	public static class MessageUtilities
	{
		public static MvcHtmlString GetErrorMessage( this HtmlHelper helper )
		{
			return GetMessage( true );
		}

		private static MvcHtmlString GetMessage( bool error )
		{
			IMessageManager messageManager = null;
			try
			{
				messageManager = ServiceLocator.Current.GetInstance<IMessageManager>();
			}
			catch
			{
				throw new ApplicationException( "ServiceLocator is null. Did you call ServiceLocator.SetLocatorProvider( {arg} ) in application initialization code? " );
			}

			if ( messageManager == null )
				return new MvcHtmlString( "Instance of IMessageManager could not be found.  Don't forget to register it in your DI configuration" );

			var message = error ? messageManager.GetErrorMessage() : messageManager.GetSuccessMessage();
			return message == String.Empty ? MvcHtmlString.Empty : new MvcHtmlString( message );
		}
	}
}