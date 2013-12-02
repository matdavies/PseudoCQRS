using System.Collections.Generic;
using System.Reflection;

namespace PseudoCQRS
{
    public interface IAssemblyListProvider
    {
        IEnumerable<Assembly> GetAssemblies();
    }
}