using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Extensions;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Services
{
    internal abstract class BaseService : IService
    {
        public ICmdletServiceProvider Provider { get; set; }

        public BaseCmdlet Cmdlet { get; set; }

        public ParameterDictionary Parameters { get; set; }

        // Protected members

        private ILogger _logger;

        protected ILogger Logger => _logger ??= new LoggerImpl(Cmdlet);

        protected TParam GetParameter<TParam>(string name, TParam defaultValue = default(TParam))
        {
            if (Parameters == null) return defaultValue;

            return Parameters.Get<TParam>(name, defaultValue) ?? defaultValue;
        }

        protected bool HasParameter(string name)
        {
            if (Parameters == null) return false;

            return Parameters.ContainsKey(name) && (Cmdlet == null || Cmdlet.MyInvocation.BoundParameters.ContainsKey(name));
        }

        protected TObj GetItem<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetDataService<TObj>(Cmdlet, parameters).GetItem();
        }

        protected IEnumerable<TObj> GetItems<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetDataService<TObj>(Cmdlet, parameters).GetItems();
        }

        protected virtual TObj NewItem<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetDataService<TObj>(Cmdlet, parameters).NewItem();
        }

        protected virtual bool TestItem<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetDataService<TObj>(Cmdlet, parameters).TestItem();
        }
        
        protected virtual TObj RenameItem<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetDataService<TObj>(Cmdlet, parameters).RenameItem();
        }
        
        protected virtual TObj SetItem<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetDataService<TObj>(Cmdlet, parameters).SetItem();
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

            var conn = Provider.GetDataService<Models.Connection>(Cmdlet, pd).GetItem();

            if (conn == null)
            {
                throw new ArgumentException($"No TFS connection information available. Either supply a valid -{scope} argument or use one of the Connect-Tfs* cmdlets prior to invoking this cmdlet.");
            }

            return conn.GetClient<TClient>();
        }

        protected bool ShouldProcess(string target, string action)
        {
            return Cmdlet.ShouldProcess(target, action);
        }

        protected bool ShouldProcess(Models.Connection target, string action)
        {
            return Cmdlet.ShouldProcess($"Team Project Collection '{target.DisplayName}'", action);
        }

        protected bool ShouldProcess(WebApiTeamProject target, string action)
        {
            return Cmdlet.ShouldProcess($"Team Project '{target.Name}'", action);
        }

        protected bool ShouldProcess(WebApiTeam target, string action)
        {
            return Cmdlet.ShouldProcess($"Team '{target.Name}'", action);
        }

        protected bool ShouldContinue(string query, string caption = "Confirm")
        {
            return Cmdlet.ShouldContinue(query, caption);
        }

        protected void Log(string message, string commandName = null, bool force = false)
        {
            Logger.Log(message, commandName, force);
        }
    }
}