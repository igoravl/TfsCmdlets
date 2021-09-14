using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Services
{
    internal class ServiceLocator
    {
        [ThreadStatic]
        private static ServiceLocator _instance;
        private Dictionary<Type, Func<object>> Factories { get; } = new Dictionary<Type, Func<object>>();
        private readonly Stack<PSCmdlet> _cmdlets = new Stack<PSCmdlet>();
        private PSCmdlet Context => _cmdlets.Count > 0 ? _cmdlets.Peek() : null;
        public static ServiceLocator Instance => _instance ??= new ServiceLocator();

        private ServiceLocator() => RegisterFactories();

        public void PushContext(PSCmdlet cmdlet)
        {
            _cmdlets.Push(cmdlet);
        }

        public void PopContext()
        {
            _cmdlets.Pop();
        }

        public void Inject(object cmdlet)
        {
            var type = cmdlet.GetType();

            var properties = type
                .GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(p => p.GetCustomAttribute<InjectAttribute>() != null);

            foreach (var prop in properties)
            {
                if (prop.GetValue(cmdlet) != null) continue;

                prop.SetValue(cmdlet, GetService(prop.PropertyType));
            }
        }

        private object Resolve(Type type)
        {
            var ctor = type.GetConstructors().First();

            var parmValues = new List<object>();

            foreach (var parm in ctor.GetParameters())
            {
                var service = GetService(parm.ParameterType);

                if (service is IFactory factory)
                {
                    factory.SetContext(parm.GetCustomAttribute<InjectAttribute>());
                    service = factory.Create();
                }

                if (service is IPowerShellSite site) site.SetCmdlet(Context);

                parmValues.Add(service);
            }

            return ctor.Invoke(parmValues.ToArray());
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public object GetService(Type serviceType)
        {
            if (!Factories.ContainsKey(serviceType))
            {
                throw new ArgumentException($"Invalid service {serviceType.FullName}. Are you missing an [Exports] attribute?");
            }

            var service = Factories[serviceType]();

            return service;
        }

        private void RegisterFactories()
        {
            foreach (var concreteType in GetType().Assembly.GetTypes()
                .Where(t =>
                    t.GetCustomAttributes<BaseExportsAttribute>().Any()))
            {
                var attrs = concreteType.GetCustomAttributes<BaseExportsAttribute>();

                Type key;
                Func<object> value;

                foreach (var attr in attrs)
                {
                    switch (attr)
                    {
                        case ExportsAttribute exportsAttr when exportsAttr.Singleton:
                            {
                                var svc = Resolve(concreteType);

                                key = attr.Exports;
                                value = () => svc;

                                break;
                            }
                        case ControllerAttribute controllerAttr:
                            {
                                key = GetControllerInterface(concreteType);
                                value = () => Resolve(concreteType);

                                break;
                            }
                        default:
                            {
                                key = attr.Exports;
                                value = () => Resolve(concreteType);

                                break;
                            }
                    }

                    if (Factories.ContainsKey(key))
                    {
                        throw new InvalidOperationException($"Service {key.FullName} already registered");
                    }

                    Factories.Add(key, value);
                }
            }
        }

        private Type GetControllerInterface(Type concreteType) =>
            concreteType.GetTypeInfo().ImplementedInterfaces.First(i => i.FullName.StartsWith("TfsCmdlets.Services.IController`1"));
    }
}