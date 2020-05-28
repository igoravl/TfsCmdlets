using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.ServiceProvider;
using TfsCmdlets.Services;

namespace TfsCmdlets.Extensions
{
    internal static class ServiceExtensions
    {
        private static ICmdletServiceProvider _provider;

        internal static T GetService<T>(this Cmdlet cmdlet) where T : IService
        {
            return _provider.GetService<T>(cmdlet);
        }

        internal static T GetOne<T>(this Cmdlet cmdlet, ParameterDictionary overriddenParameters = null, object userState = null) where T : class
        {
            return _provider.GetOne<T>(cmdlet, overriddenParameters, userState);
        }

        internal static IEnumerable<T> GetMany<T>(this Cmdlet cmdlet, ParameterDictionary overriddenParameters = null, object userState = null) where T : class
        {
            return _provider.GetMany<T>(cmdlet, overriddenParameters, userState);
        }

        internal static T GetClient<T>(this Cmdlet cmdlet) where T : VssHttpClientBase
        {
            return GetClient(cmdlet, typeof(T)) as T;
        }

        //internal static object GetClient(this Cmdlet cmdlet, string typeName)
        //{
        //    return GetClient(cmdlet, Type.GetType(typeName));
        //}

        internal static object GetClient(this Cmdlet cmdlet, Type type, string scope = "Collection", ParameterDictionary overriddenParameters = null)
        {
            return GetOne<VssConnection>(cmdlet, overriddenParameters, scope).GetClient(type);
        }

        internal static void Register(ICmdletServiceProvider provider)
        {
            _provider = provider;
        }
    }
}
