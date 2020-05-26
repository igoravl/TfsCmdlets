using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.ServiceProvider;
using TfsCmdlets.Services;

namespace TfsCmdlets.Extensions
{
    internal static class ServiceExtensions
    {
        private static ICmdletServiceProvider _provider;

        internal static IService GetService(this Cmdlet cmdlet, Type type)
        {
            return _provider.GetService(cmdlet, type);
        }

        internal static IService<T> GetService<T>(this Cmdlet cmdlet) where T : class
        {
            return (IService<T>)GetService(cmdlet, typeof(T));
        }

        internal static T GetOne<T>(this Cmdlet cmdlet, object userState = null) where T : class
        {
            return GetService<T>(cmdlet).GetOne(userState);
        }

        internal static IEnumerable<T> GetMany<T>(this Cmdlet cmdlet, object userState = null) where T : class
        {
            return GetService<T>(cmdlet).GetMany(userState);
        }

        internal static T GetClient<T>(this Cmdlet cmdlet) where T : VssHttpClientBase
        {
            return GetClient(cmdlet, typeof(T)) as T;
        }

        internal static object GetClient(this Cmdlet cmdlet, string typeName)
        {
            return GetClient(cmdlet, Type.GetType(typeName));
        }

        internal static object GetClient(this Cmdlet cmdlet, Type type, string scope = "Collection")
        {
            return GetService<VssConnection>(cmdlet).GetOne(scope).GetClient(type);
        }

        internal static void Register(ICmdletServiceProvider provider)
        {
            _provider = provider;
        }
    }
}
