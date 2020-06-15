using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Cmdlets.Team;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Services
{
    internal class CmdletServiceProviderImpl : ICmdletServiceProvider
    {
        private readonly Dictionary<Type, Func<ICmdletServiceProvider, BaseCmdlet, object, IService>> _factories =
            new Dictionary<Type, Func<ICmdletServiceProvider, BaseCmdlet, object, IService>>();

        internal CmdletServiceProviderImpl()
        {
            RegisterFactories();
        }

        public T GetService<T>(BaseCmdlet BaseCmdlet, object parameters = null) where T : IService
        {
            var serviceType = typeof(T);

            if (!_factories.ContainsKey(serviceType))
            {
                throw new ArgumentException($"Invalid service {serviceType.FullName}");
            }

            return (T)_factories[serviceType](this, BaseCmdlet, parameters);
        }

        public IDataService<TObj> GetDataService<TObj>(BaseCmdlet baseCmdlet, object overriddenParameters) where TObj : class
        {
            var serviceType = typeof(TObj);

            if (!_factories.ContainsKey(serviceType))
            {
                throw new ArgumentException($"Invalid service {serviceType.FullName}");
            }

            var dataService = ((IDataService<TObj>)_factories[serviceType](this, baseCmdlet, overriddenParameters));
            return dataService;
        }

        public TObj GetItem<TObj>(BaseCmdlet baseCmdlet, object overriddenParameters)
            where TObj : class
        {
            var dataService = GetDataService<TObj>(baseCmdlet, overriddenParameters);
            return dataService.GetItem();
        }

        public bool TestItem<TObj>(BaseCmdlet baseCmdlet, object overriddenParameters)
            where TObj : class
        {
            var dataService = GetDataService<TObj>(baseCmdlet, overriddenParameters);

            return dataService.TestItem();
        }

        public TObj NewItem<TObj>(BaseCmdlet cmdlet, object parameters = null) where TObj : class
        {
            var dataService = GetDataService<TObj>(cmdlet, parameters);
            return dataService.NewItem();
        }

        public IEnumerable<TObj> GetItems<TObj>(BaseCmdlet baseCmdlet, object overriddenParameters)
            where TObj : class
        {
            var dataService = GetDataService<TObj>(baseCmdlet, overriddenParameters);
            return dataService.GetItems();
        }

        public Models.Connection GetServer(BaseCmdlet cmdlet, ParameterDictionary parameters = null)
        {
            var pd = new ParameterDictionary(parameters)
            {
                ["ConnectionType"] = ClientScope.Server
            };

            var srv = GetItem<Models.Connection>(cmdlet, pd);

            if (srv == null)
            {
                throw new ArgumentException("No TFS connection information available. Either supply a valid -Server argument or use Connect-TfsConfigurationServer prior to invoking this cmdlet.");
            }

            return srv;
        }

        public Models.Connection GetCollection(BaseCmdlet cmdlet, ParameterDictionary parameters = null)
        {
            var pd = new ParameterDictionary(parameters)
            {
                ["ConnectionType"] = ClientScope.Collection
            };

            var tpc = GetItem<Models.Connection>(cmdlet, pd);

            if (tpc == null)
            {
                throw new ArgumentException("No TFS connection information available. Either supply a valid -Collection argument or use Connect-TfsTeamProjectCollection prior to invoking this cmdlet.");
            }

            return tpc;
        }

        public (Models.Connection, WebApiTeamProject) GetCollectionAndProject(BaseCmdlet cmdlet, ParameterDictionary parameters = null)
        {
            var tpc = GetCollection(cmdlet, parameters);

            var pd = new ParameterDictionary(parameters)
            {
                ["Collection"] = tpc
            };

            var tp = GetItem<WebApiTeamProject>(cmdlet, pd);

            if (tp == null)
            {
                throw new ArgumentException("No TFS team project information available. Either supply a valid -Project argument or use Connect-TfsTeamProject prior to invoking this cmdlet.");
            }

            return (tpc, tp);
        }

        public (Models.Connection, WebApiTeamProject, WebApiTeam) GetCollectionProjectAndTeam(BaseCmdlet cmdlet, ParameterDictionary parameters = null)
        {
            var (tpc, tp) = GetCollectionAndProject(cmdlet, parameters);

            var pd = new ParameterDictionary(parameters)
            {
                ["Collection"] = tpc,
                ["Project"] = tp
            };

            var team = GetItem<Models.Team>(cmdlet, pd);

            if (team == null)
            {
                throw new ArgumentException("No TFS team information available. Either supply a valid -Team argument or use Connect-TfsTeam prior to invoking this cmdlet.");
            }

            return (tpc, tp, team);
        }

        private void RegisterFactories()
        {
            foreach (var type in GetType().Assembly.GetTypes()
                .Where(t => t.GetCustomAttributes<ExportsAttribute>().Any()))
            {
                var attrs = type.GetCustomAttributes<ExportsAttribute>();

                foreach (var attr in attrs)
                {
                    if (attr.Singleton)
                    {
                        if (!(Activator.CreateInstance(type) is IService svc)) continue;
                        svc.Provider = this;
                        _factories.Add(attr.Exports, (prv, ctx, op) => svc);
                    }
                    else
                    {
                        _factories.Add(attr.Exports, delegate (ICmdletServiceProvider prv, BaseCmdlet ctx, object overriddenParameters)
                        {
                            if (!(Activator.CreateInstance(type) is IService svc))
                                throw new Exception($"Error instantiating {type.FullName}");

                            svc.Provider = prv;
                            svc.Parameters = new ParameterDictionary(overriddenParameters, ctx);
                            svc.Cmdlet = ctx;
                            return svc;

                        });
                    }
                }
            }
        }
    }
}