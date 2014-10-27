using System.Web;
using NUnit.Framework;
using PseudoCQRS.Controllers;
using PseudoCQRS.Tests.Helpers;
using Rhino.Mocks;
using HttpContextWrapper = PseudoCQRS.Mvc.HttpContextWrapper;

namespace PseudoCQRS.Tests.Controllers
{
	[TestFixture]
	public class ReferrerProviderTests
	{
		[Test]
		public void GetAbsoluteUri_Always_ReturnsUrlReferrerAbsoluteUri()
		{
			HttpContext.Current = HttpContextHelper.GetHttpContext();
			var httpContextWrapper = new HttpContextWrapper();

			const string expected = "http://localhost/";

			var uri = new ReferrerProvider( httpContextWrapper ).GetAbsoluteUri();
			Assert.AreEqual( expected, uri );
		}
	}
}