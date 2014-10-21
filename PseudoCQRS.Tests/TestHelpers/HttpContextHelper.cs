using System;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using System.Linq;

namespace PseudoCQRS.Tests.Helpers
{
	public static class HttpContextHelper
	{
		public static void SetRequestForm( NameValueCollection form )
		{
			BindingFlags Flags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
			var request = HttpContext.Current.Request;

			MethodInfo writableMethod = request.Form.GetType().GetMethod( "MakeReadWrite", Flags );
			MethodInfo readOnlyMethod = request.Form.GetType().GetMethod( "MakeReadOnly", Flags );
			FieldInfo formField = request.GetType().GetField( "_form", Flags );

			writableMethod.Invoke( request.Form, null );
			foreach ( var key in form.AllKeys )
				request.Form[ key ] = form[ key ];

			formField.SetValue( request, request.Form );
			readOnlyMethod.Invoke( request.Form, null );
		}


		public static HttpContext GetHttpContext( string queryString = "" )
		{
			HttpRequest request = new HttpRequest( "", "http://www.test.ing", queryString );

			var httpContext = new HttpContext( request, new HttpResponse( new StringWriter() ) );

			var field = request.GetType().GetField( "_referrer", BindingFlags.Instance | BindingFlags.NonPublic );
			field.SetValue( request, new Uri( "http://localhost" ) );


			var sessionContainer = new HttpSessionStateContainer( "id", new SessionStateItemCollection(),
			                                                      new HttpStaticObjectsCollection(), 10, true,
			                                                      HttpCookieMode.AutoDetect,
			                                                      SessionStateMode.InProc, false );

			httpContext.Items[ "AspSession" ] = typeof( HttpSessionState )
				.GetConstructor(
				                BindingFlags.NonPublic | BindingFlags.Instance,
				                null, CallingConventions.Standard,
				                new[]
				                {
					                typeof( HttpSessionStateContainer )
				                },
				                null )
				.Invoke( new object[]
				{
					sessionContainer
				} );


			return httpContext;
		}
	}
}