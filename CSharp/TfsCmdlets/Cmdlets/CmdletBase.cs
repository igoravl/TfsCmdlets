using System;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Models;
using TfsCmdlets.Util;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
using Microsoft.VisualStudio.Services.WebApi;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TfsCmdlets.Services;
using System.IO;

namespace TfsCmdlets.Cmdlets
{
    /// <summary>
    /// Abstract class from which and TfsCmdlets cmdlets derive
    /// </summary>
    public abstract class CmdletBase : PSCmdlet
    {
        /// <summary>
        /// The service provider injected in this cmdlet instance
        /// </summary>
        private protected static ICmdletServiceProvider Provider => ServiceManager.Provider;

        /// <summary>
        /// Returns the PowerShell command name of this cmdlet
        /// </summary>
        /// <value>The name of the this, as defined by the [Cmdlet] attribute. If the attribute is missing, returns the class name.</value>
        internal virtual string CommandName
        {
            get
            {
                var attr = GetType().GetCustomAttribute<CmdletAttribute>();
                return attr == null ? GetType().Name : $"{attr.VerbName}-{attr.NounName}";
            }
        }

        /// <summary>
        /// Performs initialization of the command execution, logs the supplied parameters and check whether the current 
        /// this is tagged as "Windows-only". If so, throws an exception
        /// </summary>
        protected override void BeginProcessing()
        {
            CheckWindowsOnly();
            CheckRequiredVersion();
            LogParameters();
        }

        /// <summary>
        /// Performs execution of the command. 
        /// </summary>
        protected sealed override void ProcessRecord()
        {
            DoProcessRecord();
        }

        /// <summary>
        /// Executes a PowerShell script in the current session context
        /// </summary>
        /// <param name="script">A string containing a valid PS script</param>
        /// <param name="arguments">Arguments passed to the script, represented as an array named <c>$args</c></param>
        /// <returns>The output of the script, if any</returns>
        protected virtual object InvokeScript(string script, params object[] arguments)
        {
            return InvokeCommand.InvokeScript(script, arguments);
        }

        /// <summary>
        /// Executes a PowerShell script in the current session context
        /// </summary>
        /// <param name="script">A string containing a valid PS script</param>
        /// <param name="variables">Variables passed to the script</param>
        /// <returns>The output of the script, if any</returns>
        protected virtual object InvokeScript(string script, Dictionary<string, object> variables)
        {
            var sb = ScriptBlock.Create(script);

            return sb.InvokeWithContext(null, variables.Select(kvp => new PSVariable(kvp.Key, kvp.Value)).ToList());
        }

        /// <summary>
        /// Executes a PowerShell script in the current session context
        /// </summary>
        /// <param name="script">A string containing a valid PS script</param>
        /// <param name="arguments">Arguments passed to the script, represented as an array named <c>$args</c></param>
        /// <typeparam name="T">The expected type of the objects outputted by the script</typeparam>
        /// <returns>The output of the script, if any</returns>
        protected virtual T InvokeScript<T>(string script, params object[] arguments)
        {
            var obj = InvokeCommand.InvokeScript(script, arguments)?.FirstOrDefault()?.BaseObject;

            return (T)obj;
        }

        /// <summary>
        /// Returns a "server" Connection object built from the arguments currently supplied to this cmdlet
        /// </summary>
        /// <param name="parameters">If specified, the values in this parameter will override the values originally supplied to the this</param>
        /// <returns>An instance of Connection containing either a TfsConfigurationServer (Windows) or VssConnection (Core) object</returns>
        internal virtual Models.Connection GetServer(ParameterDictionary parameters = null)
        {
            return Provider.GetServer(this, parameters);
        }

        /// <summary>
        /// Returns a "collection" Connection object built from the arguments currently supplied to this cmdlet
        /// </summary>
        /// <param name="parameters">If specified, the values in this parameter will override the values originally supplied to the this</param>
        /// <returns>An instance of Connection containing either a TfsTeamProjectCollection (Windows) or VssConnection (Core) object</returns>
        internal virtual Models.Connection GetCollection(ParameterDictionary parameters = null)
        {
            return Provider.GetCollection(this, parameters);
        }

        /// <summary>
        /// Returns a tuple containing a "collection" Connection and a TeamProject objects, built from the arguments currently supplied to this cmdlet
        /// </summary>
        /// <param name="parameters">If specified, the values in this parameter will override the values originally supplied to the this</param>
        /// <returns>A tuple consisting of an instance of Connection (containing either a TfsTeamProjectCollection (Windows) 
        ///     or VssConnection (Core) object) and an instance of TeamProject</returns>
        internal virtual (Models.Connection, WebApiTeamProject) GetCollectionAndProject(ParameterDictionary parameters = null)
        {
            return Provider.GetCollectionAndProject(this, parameters);
        }

        /// <summary>
        /// Returns a tuple containing a "collection" Connectionn, a TeamProject and a WebApiTeam objects, built from the arguments 
        /// currently supplied to this cmdlet
        /// </summary>
        /// <param name="parameters">If specified, the values in this parameter will override the values originally supplied to the this</param>
        /// <returns>A tuple consisting of an instance of Connection (containing either a TfsTeamProjectCollection (Windows) 
        ///     or VssConnection (Core) object), an instance of TeamProject and an instance of WebApiTeam</returns>
        internal virtual (Models.Connection, WebApiTeamProject, WebApiTeam) GetCollectionProjectAndTeam(ParameterDictionary parameters = null)
        {
            return Provider.GetCollectionProjectAndTeam(this, parameters);
        }

        /// <summary>
        /// Returns an API Client from the underlying connection
        /// </summary>
        /// <param name="scope">The scope from which to retrieve the client. Supported scopes are Server, Collection</param>
        /// <param name="parameters">If specified, the values in this parameter will override the values originally supplied to the this</param>
        /// <typeparam name="T">The type of the API client</typeparam>
        /// <returns>An instance of the requested API client</returns>
        private protected virtual T GetClient<T>(ClientScope scope = ClientScope.Collection, ParameterDictionary parameters = null)
            where T : VssHttpClientBase
        {
            var pd = new ParameterDictionary(parameters)
            {
                ["ConnectionType"] = scope
            };

            return Provider.GetDataService<Models.Connection>(this, pd).GetItem().GetClient<T>();
        }

        /// <summary>
        /// Returns an instance of the specified service
        /// </summary>
        /// <typeparam name="T">The type of the requested service.static Must derive from IService</typeparam>
        /// <returns>An instance of T, as provided by the current service provider</returns>
        private protected virtual T GetService<T>() where T : IService
        {
            return Provider.GetService<T>(this);
        }

        /// <summary>
        /// Gets one item of the specified type
        /// </summary>
        protected virtual TObj GetItem<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetDataService<TObj>(this, parameters).GetItem();
        }

        /// <summary>
        /// Checks if specified item exists
        /// </summary>
        protected virtual bool TestItem<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetDataService<TObj>(this, parameters).TestItem();
        }

        /// <summary>
        /// Gets one or more items of the specified type
        /// </summary>
        protected virtual IEnumerable<TObj> GetItems<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetDataService<TObj>(this, parameters).GetItems();
        }

        /// <summary>
        /// Creates a new item of the specified type
        /// </summary>
        protected virtual TObj NewItem<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetDataService<TObj>(this, parameters).NewItem();
        }

        /// <summary>
        /// Removes an item of the specified type
        /// </summary>
        protected virtual void RemoveItem<TObj>(object parameters = null) where TObj : class
        {
            Provider.GetDataService<TObj>(this, parameters).RemoveItem();
        }

        /// <summary>
        /// Renames an item of the specified type
        /// </summary>
        protected virtual TObj RenameItem<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetDataService<TObj>(this, parameters).RenameItem();
        }
        /// <summary>
        /// Renames an item of the specified type
        /// </summary>
        protected virtual TObj SetItem<TObj>(object parameters = null) where TObj : class
        {
            return Provider.GetDataService<TObj>(this, parameters).SetItem();
        }

        /// <summary>
        /// Gets the current directory in PowerShell
        /// </summary>
        protected virtual string GetCurrentDirectory()
        {
            return this.SessionState.Path.CurrentFileSystemLocation.Path;
        }

        /// <summary>
        /// Gets the current directory in PowerShell
        /// </summary>
        protected virtual string ResolvePath(string basePath, string path = "")
        {
            var relativePath = Path.Combine(basePath, path);

            if (!Path.IsPathRooted(relativePath))
            {
                relativePath = Path.Combine(GetCurrentDirectory(), relativePath);
            }

            return Path.GetFullPath(relativePath);
        }

        /// <summary>
        /// Outputs items to PowerShell
        /// </summary>
        protected void WriteItems<T>(object parameters = null) where T : class
        {
            WriteObject(GetItems<T>(parameters), true);
        }

        /// <summary>
        /// Log a message
        /// </summary>
        protected void Log(string message, string commandName = null, bool force = false)
        {
            if (!IsVerbose) return;

            if (string.IsNullOrEmpty(commandName))
            {
                commandName = this.CommandName;
            }

            this.WriteVerbose($"[{DateTime.Now:HH:mm:ss.ffff}] [{commandName}] {message}");
        }

        /// <summary>
        /// Log the parameters passed to the cmdlet
        /// </summary>
        protected void LogParameters()
        {
            if (!this.IsVerbose) return;

            var parms = new ParameterDictionary(this);

            if (parms.ContainsKey("Password") && parms["Password"] != null) parms["Password"] = "***";

            Log($"Running cmdlet with parameter set '{parms.Get<string>("ParameterSetName")}' and the following arguments:");

            Log(JObject.FromObject(parms)
                    .ToString(Formatting.None)
                    .Replace("\":", "\" = ")
                    .Replace(",\"", "; \"")
                    .Trim('{', '}')
            );
        }

        internal bool IsVerbose
        {
            get
            {
                var containsVerbose = this.MyInvocation.BoundParameters.ContainsKey("Verbose");

                if (containsVerbose)
                {
                    return ((SwitchParameter)this.MyInvocation.BoundParameters["Verbose"]).ToBool();
                }

                return (ActionPreference)this.GetVariableValue("VerbosePreference") != ActionPreference.SilentlyContinue;
            }
        }

        /// <summary>
        /// Check whether the currently executing environment is Windows PowerShell
        /// </summary>
        /// <throws>For cmdlets which are "Windows-only", a call to this method will throw a 
        ///     NotSupportedException when running on PowerShell Core.</throws>
        private void CheckWindowsOnly()
        {
            if (EnvironmentUtil.PSEdition.Equals("Desktop") || GetType().GetCustomAttribute<DesktopOnlyAttribute>() == null)
            {
                return;
            }

            throw new NotSupportedException($"This cmdlet requires Windows PowerShell. It will not work on PowerShell Core.{Environment.NewLine}");
        }

        /// <summary>
        /// Check whether the currently connected server is of a minimum version
        /// </summary>
        /// <throws>
        /// For cmdlets which require a certain version of TFS, a call to 
        /// this method will throw a NotSupportedException when connected to 
        /// an older server.
        /// </throws>
        private void CheckRequiredVersion()
        {
            var attr = GetType().GetCustomAttribute<RequiresVersionAttribute>();

            if (attr == null)
            {
                return;
            }

            var version = GetItem<ServerVersion>();

            if (version.YearVersion < attr.Version || version.Update < attr.Update)
            {
                throw new NotSupportedException($"This cmdlet requires Team Foundation Server " +
                    $"{attr.Version}{(attr.Update > 0 ? " Update" + attr.Update : "")} or later.{Environment.NewLine}");
            }
        }

        private bool yesToAll, noToAll;

        /// <inheritdoc/>
        protected bool ShouldContinue(string query, string caption = "Confirm", bool hasSecurityImpact = false)
        {
            return ShouldContinue(query, caption, hasSecurityImpact, ref yesToAll, ref noToAll);
        }

        /// <inheritdoc/>
        protected bool ShouldContinue(string query, ref bool yesToAll, ref bool noToAll, string caption = "Confirm", bool hasSecurityImpact = false)
        {
            return ShouldContinue(query, caption, hasSecurityImpact, ref yesToAll, ref noToAll);
        }

        /// <summary>
        /// Performs execution of the command. Must be overriden in derived classes.
        /// </summary>
        protected virtual void DoProcessRecord() => throw new InvalidOperationException("You must override DoProcessRecord");
    }
}