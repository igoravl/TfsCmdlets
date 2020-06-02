using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Services;

namespace TfsCmdlets.Extensions
{
    internal static class ServiceExtensions
    {
        // private static ICmdletServiceProvider _provider;

        // internal static T GetService<T>(this Cmdlet cmdlet) where T : IService
        // {
        //     return _provider.GetService<T>(cmdlet);
        // }

        // internal static T GetInstanceOf<T>(this Cmdlet cmdlet, ParameterDictionary overriddenParameters = null, object userState = null) where T : class
        // {
        //     return _provider.GetInstanceOf<T>(cmdlet, overriddenParameters, userState);
        // }

        // internal static IEnumerable<T> GetCollectionOf<T>(this Cmdlet cmdlet, ParameterDictionary overriddenParameters = null, object userState = null) where T : class
        // {
        //     return _provider.GetCollectionOf<T>(cmdlet, overriddenParameters, userState);
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
        //     return GetInstanceOf<VssConnection>(cmdlet, overriddenParameters, scope).GetClient(type);
        // }

        // internal static void Register(ICmdletServiceProvider provider)
        // {
        //     _provider = provider;
        // }
    }
}
