using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using TfsCmdlets.Services;

namespace TfsCmdlets.ServiceProvider
{
    internal class CmdletServiceProviderImpl : ICmdletServiceProvider
    {
        private readonly Dictionary<Type, Func<ICmdletServiceProvider, Cmdlet, IService>> _factories =
            new Dictionary<Type, Func<ICmdletServiceProvider, Cmdlet, IService>>();

        internal CmdletServiceProviderImpl()
        {
            RegisterFactories();
        }

        public IService GetService(Cmdlet cmdlet, Type serviceType)
        {
            if (!_factories.ContainsKey(serviceType))
                throw new ArgumentException($"Invalid service {serviceType.FullName}");

            return _factories[serviceType](this, cmdlet);
        }

        public IService<T> GetService<T>(Cmdlet cmdlet)
        {
            return (IService<T>) GetService(cmdlet, typeof(T));
        }

        public T GetInstanceOf<T>(Cmdlet cmdlet, object userState = null)
        {
            return GetService<T>(cmdlet).GetOne(userState);
        }

        private void RegisterFactories()
        {
            foreach (var type in GetType().Assembly.GetTypes()
                .Where(t => t.GetCustomAttributes<ExportsAttribute>().Any()))
            {
                var attr = type.GetCustomAttribute<ExportsAttribute>();

                if (attr.Singleton)
                {
                    var svc = Activator.CreateInstance(type) as IService;
                    svc.Provider = this;

                    _factories.Add(attr.Exports, (prv, ctx) => svc);
                }
                else
                {
                    _factories.Add(attr.Exports, delegate(ICmdletServiceProvider prv, Cmdlet ctx)
                    {
                        var svc = Activator.CreateInstance(type) as IService;
                        svc.Provider = prv;
                        svc.Cmdlet = ctx;

                        return svc;
                    });
                }
            }
        }
    }
}