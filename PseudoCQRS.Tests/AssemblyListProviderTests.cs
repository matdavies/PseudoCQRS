using System.Linq;
using NUnit.Framework;

namespace PseudoCQRS.Tests
{
    [TestFixture]
    public class AssemblyListProviderTests
    {
        [Test]
        public void GetAssemblies_ReturnsAssembliesInAppDomain()
        {
            var provider = new AssemblyListProvider();
            Assert.Greater( provider.GetAssemblies().Count(), 1 );
        }
    }
}