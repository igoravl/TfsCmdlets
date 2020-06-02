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
        protected abstract IEnumerable<T> DoGetItems(object userState);

        public ParameterDictionary Parameters { get; set; }

        public T GetOne(ParameterDictionary overriddenParameters = null, object userState = null) 
        {
            Parameters = (overriddenParameters ?? new ParameterDictionary());
            Parameters.Merge(new ParameterDictionary(Cmdlet));

            var items = GetMany(overriddenParameters, userState)?.ToList()?? new List<T>();
            if(items == null || items.Count == 0) return null;//throw new Exception($"Invalid or non-existent {ItemName} '{ItemFilter}'");

            return items[0];
        }

        public IEnumerable<T> GetMany(ParameterDictionary overriddenParameters = null, object userState = null)
        {
            Parameters = (overriddenParameters ?? new ParameterDictionary());
            Parameters.Merge(new ParameterDictionary(Cmdlet));

            return DoGetItems(userState);
        }

        protected TParam GetParameter<TParam>(string name, TParam defaultValue = default(TParam))
        {
            return Parameters.Get<TParam>(name, defaultValue);
        }

        protected TObj GetOne<TObj>(ParameterDictionary overriddenParameters = null, object userState = null) where TObj: class
        {
            return Provider.GetOne<TObj>(Cmdlet, overriddenParameters, userState);
        }

        protected IEnumerable<TObj> GetMany<TObj>(ParameterDictionary overriddenParameters = null, object userState = null) where TObj: class
        {
            return Provider.GetMany<TObj>(Cmdlet, overriddenParameters, userState);
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
            return Provider.GetOne<TfsConnection>(Cmdlet, parameters, scope.ToString()).GetClient<TClient>();
        }
    }
}
