using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCQRS
{
    public interface IHttpContextWrapper
    {
        string GetUrlReferrerAbsoluteUri();
	    void SessionSetItem( string key, object value );
	    object SessionGetItem( string key );
	    void SessionRemoveItem( string key );
	    bool RequestContainsCookie( string cookieName );
	    string RequestGetCookieValue( string cookieName );
	    void ResponseSetCookie( string name, string value );
    }
}
