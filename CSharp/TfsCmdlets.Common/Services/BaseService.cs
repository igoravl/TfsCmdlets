using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Extensions;
using TfsConnection = TfsCmdlets.Services.Connection;

namespace TfsCmdlets.Services
{
    internal abstract class BaseService : IService
    {
        private ILogger _logger;

        public ICmdletServiceProvider Provider { get; set; }

        public BaseCmdlet Cmdlet { get; set; }

        protected ILogger Logger => _logger ??= new LoggerImpl(Cmdlet);
    }

    internal abstract class BaseDataService<T> : BaseService, IDataService<T> where T: class
    {
        private ParameterDictionary _parameters;
        
        protected abstract IEnumerable<T> DoGetItems();

        public T GetInstanceOf(object parameters = null) 
        {
            var items = GetCollectionOf(parameters)?.ToList()?? new List<T>();
            if(items == null || items.Count == 0) return null;//throw new Exception($"Invalid or non-existent {ItemName} '{ItemFilter}'");

            return items[0];
        }

        public IEnumerable<T> GetCollectionOf(object parameters = null)
        {
            _parameters = new ParameterDictionary(parameters, Cmdlet);
            return DoGetItems();
        }

        protected TParam GetParameter<TParam>(string name, TParam defaultValue = default(TParam))
        {
            if(_parameters == null) return defaultValue;

            return _parameters.Get<TParam>(name, defaultValue) ?? defaultValue;
        }

        protected void OverrideParameter(string name, object value)
        {
            if(_parameters == null) return;

            _parameters[name] = value;
        }

        protected TObj GetInstanceOf<TObj>(ParameterDictionary parameters = null) where TObj: class
        {
            return Provider.GetInstanceOf<TObj>(Cmdlet, parameters);
        }

        protected IEnumerable<TObj> GetCollectionOf<TObj>(ParameterDictionary parameters = null) where TObj: class
        {
            return Provider.GetCollectionOf<TObj>(Cmdlet, parameters);
        }

        protected Connection GetServer(ParameterDictionary parameters = null)
        {
            return Provider.GetServer(Cmdlet, parameters);
        }

        protected Connection GetCollection(ParameterDictionary parameters = null)
        {
            return Provider.GetCollection(Cmdlet, parameters);
        }

        protected (Connection, TeamProject) GetCollectionAndProject(ParameterDictionary parameters = null)
        {
            return Provider.GetCollectionAndProject(Cmdlet, parameters);
        }

        protected (Connection, TeamProject, WebApiTeam) GetCollectionProjectAndTeam(ParameterDictionary parameters = null)
        {
            return Provider.GetCollectionProjectAndTeam(Cmdlet, parameters);
        }

        protected TClient GetClient<TClient>(ClientScope scope = ClientScope.Collection, ParameterDictionary parameters = null)
            where TClient : VssHttpClientBase
        {
            var pd = new ParameterDictionary(parameters) {
                ["ConnectionType"] = scope
            };

            var conn = Provider.GetInstanceOf<TfsConnection>(Cmdlet, pd);

            if(conn == null)
            {
                throw new ArgumentException($"No TFS connection information available. Either supply a valid -{scope} argument or use one of the Connect-Tfs* cmdlets prior to invoking this cmdlet.");
            }

            return conn.GetClient<TClient>();
        }
    }
}
