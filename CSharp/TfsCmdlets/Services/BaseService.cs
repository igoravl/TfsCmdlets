using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private ILogger _logger;

        private bool yesToAll, noToAll;

        public ICmdletServiceProvider Provider { get; set; }

        public CmdletBase Cmdlet { get; set; }

        public ParameterDictionary Parameters { get; set; }

        protected ILogger Logger => _logger ??= new LoggerImpl(Cmdlet);

        protected TParam GetParameter<TParam>(string name, TParam defaultValue = default(TParam))
            => (Parameters == null) ? defaultValue :
                Parameters.Get<TParam>(name, defaultValue) ?? defaultValue;

        protected bool HasParameter(string name)
            => (Parameters == null) ? false :
                Parameters.ContainsKey(name) && (Cmdlet == null || Cmdlet.MyInvocation.BoundParameters.ContainsKey(name));

        protected TObj GetItem<TObj>(object parameters = null) where TObj : class 
            => Provider.GetDataService<TObj>(Cmdlet, MergeParameters(parameters)).GetItem();

        protected IEnumerable<TObj> GetItems<TObj>(object parameters = null) where TObj : class 
            => Provider.GetDataService<TObj>(Cmdlet, MergeParameters(parameters)).GetItems();

        protected virtual TObj NewItem<TObj>(object parameters = null) where TObj : class 
            => Provider.GetDataService<TObj>(Cmdlet, MergeParameters(parameters)).NewItem();

        protected virtual bool TestItem<TObj>(object parameters = null) where TObj : class 
            => Provider.GetDataService<TObj>(Cmdlet, MergeParameters(parameters)).TestItem();
        
        protected virtual TObj RenameItem<TObj>(object parameters = null) where TObj : class 
            => Provider.GetDataService<TObj>(Cmdlet, MergeParameters(parameters)).RenameItem();
        
        protected virtual void RemoveItem<TObj>(object parameters = null) where TObj : class 
            => Provider.GetDataService<TObj>(Cmdlet, MergeParameters(parameters)).RemoveItem();
        
        protected virtual TObj SetItem<TObj>(object parameters = null) where TObj : class 
            => Provider.GetDataService<TObj>(Cmdlet, MergeParameters(parameters)).SetItem();

        private ParameterDictionary MergeParameters(object overridingParameters) 
            => ParameterDictionary.From(this.Parameters, overridingParameters);
        
        protected virtual T GetService<T>() where T : IService 
            => Provider.GetService<T>(Cmdlet);
        
        protected Models.Connection GetServer(ParameterDictionary parameters = null) 
            => Provider.GetServer(Cmdlet, MergeParameters(parameters));

        protected Models.Connection GetCollection(ParameterDictionary parameters = null) 
            => Provider.GetCollection(Cmdlet, MergeParameters(parameters));

        protected (Models.Connection, TeamProject) GetCollectionAndProject(ParameterDictionary parameters = null) 
            => Provider.GetCollectionAndProject(Cmdlet, MergeParameters(parameters));

        protected (Models.Connection, TeamProject, WebApiTeam) GetCollectionProjectAndTeam(ParameterDictionary parameters = null) 
            => Provider.GetCollectionProjectAndTeam(Cmdlet, MergeParameters(parameters));

        protected bool ShouldProcess(string target, string action)
            => Cmdlet.ShouldProcess(target, action);

        protected bool ShouldProcess(Models.Connection target, string action)
            => Cmdlet.ShouldProcess($"Team Project Collection '{target.DisplayName}'", action);

        protected bool ShouldProcess(WebApiTeamProject target, string action)
            => Cmdlet.ShouldProcess($"Team Project '{target.Name}'", action);

        protected bool ShouldProcess(WebApiTeam target, string action)
            => Cmdlet.ShouldProcess($"Team '{target.Name}'", action);

        protected bool ShouldContinue(string query, string caption = "Confirm", bool hasSecurityImpact = false)
            => Cmdlet.ShouldContinue(query, caption, hasSecurityImpact, ref yesToAll, ref noToAll);

        protected bool ShouldContinue(string query, ref bool yesToAll, ref bool noToAll, string caption = "Confirm", bool hasSecurityImpact = false)
            => Cmdlet.ShouldContinue(query, caption, hasSecurityImpact, ref yesToAll, ref noToAll);

        protected void Log(string message, string commandName = null, bool force = false)
            => Logger.Log(message, commandName, force);
 
         protected TClient GetClient<TClient>(ClientScope scope = ClientScope.Collection, ParameterDictionary parameters = null) where TClient : VssHttpClientBase
            => Provider.GetClient<TClient>(Cmdlet, scope, MergeParameters(parameters));
   }
}