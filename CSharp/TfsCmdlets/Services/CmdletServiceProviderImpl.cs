using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Cmdlets.Team;
using TfsCmdlets.Extensions;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Services
{
    internal class CmdletServiceProviderImpl : ICmdletServiceProvider
    {
        private readonly Dictionary<Type, Func<ICmdletServiceProvider, CmdletBase, object, IService>> _factories =
            new Dictionary<Type, Func<ICmdletServiceProvider, CmdletBase, object, IService>>();

        internal CmdletServiceProviderImpl() => RegisterFactories();

        public T GetService<T>(CmdletBase CmdletBase, object parameters = null) where T : IService
        {
            var serviceType = typeof(T);

            if (!_factories.ContainsKey(serviceType))
            {
                throw new ArgumentException($"Invalid service {serviceType.FullName}. Are you missing an [Exports] attribute?");
            }

            return (T)_factories[serviceType](this, CmdletBase, parameters);
        }

        public IDataService<TObj> GetDataService<TObj>(CmdletBase CmdletBase, object overriddenParameters) where TObj : class
        {
            var serviceType = typeof(TObj);

            if (!_factories.ContainsKey(serviceType))
            {
                throw new ArgumentException($"Invalid service {serviceType.FullName}. Are you missing an [Exports] attribute?");
            }

            var dataService = ((IDataService<TObj>)_factories[serviceType](this, CmdletBase, overriddenParameters));

            return dataService;
        }

        public Models.Connection GetServer(CmdletBase cmdlet, ParameterDictionary parameters = null)
        {
            var parms = ParameterDictionary.From(parameters, new {
                ConnectionType = ClientScope.Server
            });

            var srv = GetDataService<Models.Connection>(cmdlet, parms).GetItem();

            if (srv == null)
            {
                throw new ArgumentException("No TFS connection information available. Either supply a valid -Server argument or use Connect-TfsConfigurationServer prior to invoking this cmdlet.");
            }

            return srv;
        }

        public Models.Connection GetCollection(CmdletBase cmdlet, ParameterDictionary parameters = null)
        {
            var pd = ParameterDictionary.From(cmdlet, parameters);
            pd["ConnectionType"] = ClientScope.Collection;

            var tpc = GetDataService<Models.Connection>(cmdlet, pd).GetItem();

            if (tpc == null)
            {
                throw new ArgumentException("No TFS connection information available. Either supply a valid -Collection argument or use Connect-TfsTeamProjectCollection prior to invoking this cmdlet.");
            }

            return tpc;
        }

        public (Models.Connection, WebApiTeamProject) GetCollectionAndProject(CmdletBase cmdlet, ParameterDictionary parameters = null)
        {
            var tpc = GetCollection(cmdlet, parameters);

            var pd = ParameterDictionary.From(cmdlet, parameters);
            pd["Collection"] = tpc;

            var tp = GetDataService<WebApiTeamProject>(cmdlet, pd).GetItem();

            if (tp == null)
            {
                throw new ArgumentException("No TFS team project information available. Either supply a valid -Project argument or use Connect-TfsTeamProject prior to invoking this cmdlet.");
            }

            return (tpc, tp);
        }

        public (Models.Connection, WebApiTeamProject, WebApiTeam) GetCollectionProjectAndTeam(CmdletBase cmdlet, ParameterDictionary parameters = null)
        {
            var parms = ParameterDictionary.From(cmdlet, parameters);

            if (parms.Get<object>("Team") is WebApiTeam t)
            {
                parms["Project"] = t.ProjectId;
            }

            var (tpc, tp) = GetCollectionAndProject(cmdlet, parms);

            parms["Collection"] = tpc;
            parms["Project"] = tp;

            var team = GetDataService<Models.Team>(cmdlet, parms).GetItem();

            if (team == null)
            {
                throw new ArgumentException("No TFS team information available. Either supply a valid -Team argument or use Connect-TfsTeam prior to invoking this cmdlet.");
            }

            return (tpc, tp, team);
        }

        public TClient GetClient<TClient>(CmdletBase cmdlet, ClientScope scope = ClientScope.Collection, ParameterDictionary parameters = null) where TClient : VssHttpClientBase
        {
            var pd = ParameterDictionary.From(cmdlet, parameters);
            pd["ConnectionType"] = scope;

            var conn = GetDataService<Models.Connection>(cmdlet, pd).GetItem();

            if (conn == null)
            {
                throw new ArgumentException($"No TFS connection information available. Either supply a valid -{scope} argument or use one of the Connect-Tfs* cmdlets prior to invoking this cmdlet.");
            }

            return conn.GetClient<TClient>();
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
                        _factories.Add(attr.Exports, delegate (ICmdletServiceProvider prv, CmdletBase ctx, object overriddenParameters)
                        {
                            if (!(Activator.CreateInstance(type) is IService svc))
                                throw new Exception($"Error instantiating {type.FullName}");

                            svc.Provider = prv;
                            svc.Parameters = ParameterDictionary.From(ctx, overriddenParameters);
                            svc.Cmdlet = ctx;
                            return svc;

                        });
                    }
                }
            }
        }
    }
}