using System.Composition;
using Microsoft.Win32;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IRegistryService))]
    internal class RegistryServiceImpl: IRegistryService
    {
        public object GetValue(string key, string name)
        {
            return Registry.GetValue(key, name, null);
        }

        public bool HasValue(string key, string value)
        {
            return Registry.GetValue(key, value, null) != null;
        }
    }
}
