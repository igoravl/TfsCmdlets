﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Services
{
    internal class ServiceLocator
    {
        [ThreadStatic]
        private static ServiceLocator _instance;
        private Dictionary<Type, IList<Func<object>>> Factories { get; } = new Dictionary<Type, IList<Func<object>>>();

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

        public void Inject(Cmdlets.CmdletBase cmdlet)
        {
            var type = cmdlet.GetType();

            var properties = type
                .GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(p => p.GetCustomAttribute<InjectAttribute>() != null);

            foreach (var prop in properties)
            {
                if (prop.GetValue(cmdlet) != null) continue;

                var typeInfo = prop.PropertyType.GetTypeInfo();
                var types = typeInfo.ImplementedInterfaces?.ToList();
                var isEnumerable = types.Any(t => t.IsAssignableFrom(typeof(IEnumerable)));

                if (isEnumerable && typeInfo.IsGenericType)
                {
                    var services = GetServices(typeInfo.GenericTypeArguments[0]);
                    var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(typeInfo.GenericTypeArguments[0]));
                    services.ForEach(s => list.Add(s));
                    
                    prop.SetValue(cmdlet, list);
                }
                else
                {
                    prop.SetValue(cmdlet, GetService(prop.PropertyType));
                }
            }
        }

        private object Resolve(Type type)
        {
            var ctor = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).First();

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
            return GetServices(serviceType).First();
        }

        public IEnumerable<T> GetServices<T>()
        {
            return (IEnumerable<T>) GetServices(typeof(T));
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (!Factories.ContainsKey(serviceType))
            {
                throw new ArgumentException($"Invalid service {serviceType.FullName}. Are you missing an [Exports] attribute?");
            }

            var factories = Factories[serviceType];

            return factories.Select(svcFunc => svcFunc()).ToList();
        }

        private void RegisterFactories()
        {
            foreach (var concreteType in GetType().Assembly.GetTypes()
                .Where(t => t.GetCustomAttributes<ExportsAttributeBase>().Any()))
            {
                var attrs = concreteType.GetCustomAttributes<ExportsAttributeBase>();

                Type key;
                Func<object> value;

                foreach (var attr in attrs)
                {
                    switch (attr)
                    {
                        case ExportsAttribute {Singleton: true}:
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

                    if (!Factories.ContainsKey(key))
                    {
                        Factories[key] = new List<Func<object>>();
                    }

                    Factories[key].Add(value);
                }
            }
        }

        private Type GetControllerInterface(Type concreteType) =>
            concreteType.GetTypeInfo().ImplementedInterfaces.First(i => i.FullName.StartsWith("TfsCmdlets.Services.IController`1"));
    }
}