using System;
using System.Web;

namespace PseudoCQRS.Controllers
{
	public class SessionBasedMessageManager : IMessageManager
	{
		private const string ErrorKey = "SessionBasedMessageManager_ErrorKey";
		private const string SuccessKey = "SessionBasedMessageManager_SuccessKey";

		public void SetErrorMessage( string message )
		{
			SetMessage( ErrorKey, message );
		}

		public string GetErrorMessage()
		{
			return GetMessage( ErrorKey );
		}

		public void SetSuccessMessage( string message )
		{
			SetMessage( SuccessKey, message );
		}

		public string GetSuccessMessage()
		{
			return GetMessage( SuccessKey );
		}

		private static void SetMessage( string key, string message )
		{
			if ( HttpContext.Current != null )
				HttpContext.Current.Session[ key ] = message;
		}

		private static string GetMessage( string key )
		{
			if ( HttpContext.Current == null || HttpContext.Current.Session[ key ] == null )
				return String.Empty;

			var retVal = HttpContext.Current.Session[ key ].ToString();
			HttpContext.Current.Session.Remove( key );
			return retVal;
		}

	}
}
