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

        public CmdletBase Cmdlet { get; set; }

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
            return Provider.GetDataService<TObj>(Cmdlet, MergeParameters(parameters)).GetItem();
        }

        protected IEnumerable<TObj> GetItems<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetDataService<TObj>(Cmdlet, MergeParameters(parameters)).GetItems();
        }

        protected virtual TObj NewItem<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetDataService<TObj>(Cmdlet, MergeParameters(parameters)).NewItem();
        }

        protected virtual bool TestItem<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetDataService<TObj>(Cmdlet, MergeParameters(parameters)).TestItem();
        }
        
        protected virtual TObj RenameItem<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetDataService<TObj>(Cmdlet, MergeParameters(parameters)).RenameItem();
        }
        
        protected virtual void RemoveItem<TObj>(object parameters = null) where TObj : class
        {
            Provider.GetDataService<TObj>(Cmdlet, MergeParameters(parameters)).RemoveItem();
        }
        
        protected virtual TObj SetItem<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetDataService<TObj>(Cmdlet, MergeParameters(parameters)).SetItem();
        }

        private ParameterDictionary MergeParameters(object overridingParameters)
        {
            return ParameterDictionary.From(this.Parameters, overridingParameters);
        }
        
        /// <summary>
        /// Returns an instance of the specified service
        /// </summary>
        /// <typeparam name="T">The type of the requested service.static Must derive from IService</typeparam>
        /// <returns>An instance of T, as provided by the current service provider</returns>
        protected virtual T GetService<T>() where T : IService
        {
            return Provider.GetService<T>(Cmdlet);
        }

        
        protected Models.Connection GetServer(ParameterDictionary parameters = null)
        {
            return Provider.GetServer(Cmdlet, MergeParameters(parameters));
        }

        protected Models.Connection GetCollection(ParameterDictionary parameters = null)
        {
            return Provider.GetCollection(Cmdlet, MergeParameters(parameters));
        }

        protected (Models.Connection, TeamProject) GetCollectionAndProject(ParameterDictionary parameters = null)
        {
            return Provider.GetCollectionAndProject(Cmdlet, MergeParameters(parameters));
        }

        protected (Models.Connection, TeamProject, WebApiTeam) GetCollectionProjectAndTeam(ParameterDictionary parameters = null)
        {
            return Provider.GetCollectionProjectAndTeam(Cmdlet, MergeParameters(parameters));
        }

        protected TClient GetClient<TClient>(ClientScope scope = ClientScope.Collection, ParameterDictionary parameters = null)
            where TClient : VssHttpClientBase
        {
            var pd = ParameterDictionary.From(this.Parameters, parameters);
            pd["ConnectionType"] = scope;

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

        private bool yesToAll, noToAll;

        protected bool ShouldContinue(string query, string caption = "Confirm", bool hasSecurityImpact = false)
        {
            return Cmdlet.ShouldContinue(query, caption, hasSecurityImpact, ref yesToAll, ref noToAll);
        }

        protected bool ShouldContinue(string query, ref bool yesToAll, ref bool noToAll, string caption = "Confirm", bool hasSecurityImpact = false)
        {
            return Cmdlet.ShouldContinue(query, caption, hasSecurityImpact, ref yesToAll, ref noToAll);
        }

        protected void Log(string message, string commandName = null, bool force = false)
        {
            Logger.Log(message, commandName, force);
        }
    }
}