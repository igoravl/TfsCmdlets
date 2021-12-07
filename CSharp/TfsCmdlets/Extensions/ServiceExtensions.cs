namespace TfsCmdlets.Extensions
{
    internal static class ServiceExtensions
    {
        // private static ICmdletServiceProvider _provider;

        // internal static T GetService<T>(this Cmdlet cmdlet) where T : IService
        // {
        //     return _provider.GetService<T>(cmdlet);
        // }

        // internal static T GetItem<T>(this Cmdlet cmdlet, ParameterDictionary overriddenParameters = null) where T : class
        // {
        //     return _provider.GetItem<T>(cmdlet, overriddenParameters);
        // }

        // internal static IEnumerable<T> GetItems<T>(this Cmdlet cmdlet, ParameterDictionary overriddenParameters = null) where T : class
        // {
        //     return _provider.GetItems<T>(cmdlet, overriddenParameters);
        // }

        // internal static T GetClient<T>(this Cmdlet cmdlet) where T : VssHttpClientBase
        // {
        //     return GetClient(cmdlet, typeof(T)) as T;
        // }

        // //internal static object GetClient(this Cmdlet cmdlet, string typeName)
        // //{
        // //    return GetClient(cmdlet, Type.GetType(typeName));
        // //}

        // internal static object GetClient(this Cmdlet cmdlet, Type type, string scope = "Collection", ParameterDictionary overriddenParameters = null)
        // {
        //     return GetItem<VssConnection>(cmdlet, overriddenParameters, scope).GetClient(type);
        // }

        // internal static void Register(ICmdletServiceProvider provider)
        // {
        //     _provider = provider;
        // }
    }
}
