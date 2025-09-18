using System.Composition.Hosting;
using System.Reflection;

namespace TfsCmdlets.Services
{
    public static class ServiceLocator
    {
        private static CompositionHost _instance;
        private static object _site;

        public static void SetSite(object site)
        {
            _site = site;
        }

        public static CompositionHost Instance
        {
            get
            {
                return _instance ??= new ContainerConfiguration()
                    .WithAssembly(Assembly.GetAssembly(_site.GetType()))
                    .CreateContainer();
            }
        }
    }
}
