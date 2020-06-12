using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Services
{
    internal abstract class BaseService : IService
    {
        public ParameterDictionary Parameters {get;set;}

        private ILogger _logger;

        public ICmdletServiceProvider Provider { get; set; }

        public BaseCmdlet Cmdlet { get; set; }

        protected ILogger Logger => _logger ??= new LoggerImpl(Cmdlet);

        protected TParam GetParameter<TParam>(string name, TParam defaultValue = default(TParam))
        {
            if (Parameters == null) return defaultValue;

            return Parameters.Get<TParam>(name, defaultValue) ?? defaultValue;
        }

        protected TObj GetItem<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetItem<TObj>(Cmdlet, parameters);
        }

        protected IEnumerable<TObj> GetItems<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetItems<TObj>(Cmdlet, parameters);
        }

        protected Models.Connection GetServer(ParameterDictionary parameters = null)
        {
            return Provider.GetServer(Cmdlet, parameters);
        }

        protected Models.Connection GetCollection(ParameterDictionary parameters = null)
        {
            return Provider.GetCollection(Cmdlet, parameters);
        }

        protected (Models.Connection, TeamProject) GetCollectionAndProject(ParameterDictionary parameters = null)
        {
            return Provider.GetCollectionAndProject(Cmdlet, parameters);
        }

        protected (Models.Connection, TeamProject, WebApiTeam) GetCollectionProjectAndTeam(ParameterDictionary parameters = null)
        {
            return Provider.GetCollectionProjectAndTeam(Cmdlet, parameters);
        }

        protected TClient GetClient<TClient>(ClientScope scope = ClientScope.Collection, ParameterDictionary parameters = null)
            where TClient : VssHttpClientBase
        {
            var pd = new ParameterDictionary(parameters)
            {
                ["ConnectionType"] = scope
            };

            var conn = Provider.GetItem<Models.Connection>(Cmdlet, pd);

            if (conn == null)
            {
                throw new ArgumentException($"No TFS connection information available. Either supply a valid -{scope} argument or use one of the Connect-Tfs* cmdlets prior to invoking this cmdlet.");
            }

            return conn.GetClient<TClient>();
        }
    }

    internal abstract class BaseDataService<T> : BaseService, IDataService<T> where T : class
    {
        protected abstract IEnumerable<T> DoGetItems();

        public T GetItem()
        {
            var items = GetItems()?.ToList() ?? new List<T>();
            if (items == null || items.Count == 0) return null;

            return items[0];
        }

        public IEnumerable<T> GetItems()
        {
            return DoGetItems();
        }
    }
}
