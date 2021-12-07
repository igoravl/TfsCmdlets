using System.Composition.Hosting;
using System.Reflection;

namespace TfsCmdlets.Services
{
    internal class ServiceLocator
    {
        private static CompositionHost _instance;

        public static CompositionHost Instance
        {
            get
            {
                return _instance ??= new ContainerConfiguration()
                    .WithAssembly(Assembly.GetExecutingAssembly())
                    .CreateContainer();
            }
        }
    }
}
