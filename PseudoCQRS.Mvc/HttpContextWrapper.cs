using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PseudoCQRS.Mvc
{
	public class HttpContextWrapper : IHttpContextWrapper
	{
		public string GetUrlReferrerAbsoluteUri()
		{
			return HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
		}


		public bool ContainsSessionItem( string key )
		{
			return HttpContext.Current.Session[ key ] != null;
		}

		public string GetQueryStringItem( string key )
		{
			return HttpContext.Current.Request.QueryString[ key ];
		}

		public bool ContainsQueryStringItem( string key )
		{
			return HttpContext.Current.Request.QueryString[ key ] != null;
		}

		public void SetSessionItem( string key, object value )
		{
			if ( HttpContext.Current != null )
				HttpContext.Current.Session[key] = value;
		}

		public object GetSessionItem( string key )
		{
			var result = HttpContext.Current.Session[key];
			return result;
		}

		public void SessionRemoveItem( string key )
		{
			HttpContext.Current.Session.Remove( key );
		}

		public bool RequestContainsCookie( string cookieName )
		{
			return HttpContext.Current.Request.Cookies[cookieName] != null;
		}

		public string RequestGetCookieValue( string cookieName )
		{
			return HttpContext.Current.Request.Cookies[cookieName].Value;
		}

		public void ResponseSetCookie( string name, string value )
		{
			HttpContext.Current.Response.Cookies.Set( new HttpCookie( name, value ) );
		}

		public bool ContainsFormItem( string key )
		{
			return HttpContext.Current.Request.Form[ key ] != null;
		}

		public object GetFormItem( string key )
		{
			return HttpContext.Current.Request.Form[ key ];
		}

		public bool ContainsRouteDataItem( string key )
		{
			return HttpContext.Current.Request.RequestContext.RouteData.Values.ContainsKey( key );
		}

		public object GetRouteDataItem( string key )
		{
			return HttpContext.Current.Request.RequestContext.RouteData.Values[ key ];
		}
	}
}