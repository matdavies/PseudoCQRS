namespace PseudoCQRS
{
    public interface IHttpContextWrapper
    {
        string GetUrlReferrerAbsoluteUri();

		void SetSessionItem( string key, object value );
	    object GetSessionItem( string key );
	    void SessionRemoveItem( string key );

	    bool RequestContainsCookie( string cookieName );
	    string RequestGetCookieValue( string cookieName );
	    void ResponseSetCookie( string name, string value );

	    bool ContainsFormItem( string key );
	    object GetFormItem( string key );
	    bool ContainsRouteDataItem( string key );
	    object GetRouteDataItem( string key );
	    bool ContainsSessionItem( string key );
	    string GetQueryStringItem( string key );
	    bool ContainsQueryStringItem( string key );
    }
}
