using System.Web;

namespace PseudoCQRS.Controllers
{
	public class ReferrerProvider : IReferrerProvider
	{
		public string GetAbsoluteUri()
		{
			return HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
		}
	}
}