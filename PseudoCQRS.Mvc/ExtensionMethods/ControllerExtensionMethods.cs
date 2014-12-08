using System;
using System.Web.Mvc;

namespace PseudoCQRS.Controllers.ExtensionMethods
{
	public static class ControllerExtensionMethods
	{
		public static RazorView GetView( this Controller controller )
		{
			string path = ( (IViewPath)controller ).ViewPath;
			return new RazorView( controller.ControllerContext, String.Format( "~/Views/{0}.cshtml", path ), "", true, null );
		}
	}
}