using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using NUnit.Framework;
using PseudoCQRS.Controllers;
using PseudoCQRS.Tests.Helpers;

namespace PseudoCQRS.Tests.Controllers
{
    [TestFixture]
    public class ReferrerProviderTests
    {
        [Test]
        public void GetAbsoluteUri_Always_ReturnsUrlReferrerAbsoluteUri()
        {
            HttpContext.Current = HttpContextHelper.GetHttpContext();

            var uri = new ReferrerProvider().GetAbsoluteUri();
            Assert.AreEqual( "http://localhost/", uri );
        }
    }
}
