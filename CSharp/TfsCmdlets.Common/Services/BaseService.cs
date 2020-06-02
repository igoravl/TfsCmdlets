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
        
        protected abstract IEnumerable<T> DoGetItems(object userState);

        public T GetInstanceOf(ParameterDictionary parameters = null, object userState = null) 
        {
            var items = GetCollectionOf(parameters, userState)?.ToList()?? new List<T>();
            if(items == null || items.Count == 0) return null;//throw new Exception($"Invalid or non-existent {ItemName} '{ItemFilter}'");

            return items[0];
        }

        public IEnumerable<T> GetCollectionOf(ParameterDictionary parameters = null, object userState = null)
        {
            _parameters = new ParameterDictionary(parameters, Cmdlet);
            return DoGetItems(userState);
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

        protected TObj GetInstanceOf<TObj>(ParameterDictionary parameters = null, object userState = null) where TObj: class
        {
            return Provider.GetInstanceOf<TObj>(Cmdlet, parameters, userState);
        }

        protected IEnumerable<TObj> GetCollectionOf<TObj>(ParameterDictionary parameters = null, object userState = null) where TObj: class
        {
            return Provider.GetCollectionOf<TObj>(Cmdlet, parameters, userState);
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
            return Provider.GetInstanceOf<TfsConnection>(Cmdlet, parameters, scope.ToString()).GetClient<TClient>();
        }
    }
}
