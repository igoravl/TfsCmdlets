namespace TfsCmdlets.Services
{
    internal static class ServiceManager
    {
        private static ICmdletServiceProvider _provider;

        internal static ICmdletServiceProvider Provider => _provider;

        internal static void Register(ICmdletServiceProvider provider) => _provider = provider;
    }
}