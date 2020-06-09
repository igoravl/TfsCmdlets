using System;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Client;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using TfsCmdlets.Util;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
using TfsConnection = TfsCmdlets.Services.Connection;
using Microsoft.VisualStudio.Services.WebApi;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TfsCmdlets.Cmdlets
{
    /// <summary>
    /// Abstract class from which and TfsCmdlets cmdlets derive
    /// </summary>
    public abstract class BaseCmdlet : PSCmdlet
    {
        /// <summary>
        /// The service provider injected in this cmdlet instance
        /// </summary>
        protected static ICmdletServiceProvider Provider => ServiceManager.Provider;

        /// <summary>
        /// Returns the PowerShell command name of this cmdlet
        /// </summary>
        /// <value>The name of the this, as defined by the [Cmdlet] attribute. If the attribute is missing, returns the class name.</value>
        internal virtual string CommandName
        {
            get
            {
                var attr = GetType().GetCustomAttribute<CmdletAttribute>();
                return attr == null ? GetType().Name : $"{attr.VerbName}-{Consts.DEFAULT_PREFIX}{attr.NounName}";
            }
        }

        /// <summary>
        /// Performs initialization of the command execution, logs the supplied parameters and check whether the current 
        /// this is tagged as "Windows-only". If so, throws an exception
        /// </summary>
        protected override void BeginProcessing()
        {
            CheckWindowsOnly();
            LogParameters();
        }

        /// <summary>
        /// Performs execution of the command. Must be overriden in derived classes.
        /// </summary>
        protected override void ProcessRecord()
        {
            throw new NotImplementedException("You must override ProcessRecord");
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
        internal virtual TfsConnection GetServer(ParameterDictionary parameters = null)
        {
           return Provider.GetServer(this, parameters);
        }

        /// <summary>
        /// Returns a "collection" Connection object built from the arguments currently supplied to this cmdlet
        /// </summary>
        /// <param name="parameters">If specified, the values in this parameter will override the values originally supplied to the this</param>
        /// <returns>An instance of Connection containing either a TfsTeamProjectCollection (Windows) or VssConnection (Core) object</returns>
        internal virtual TfsConnection GetCollection(ParameterDictionary parameters = null)
        {
           return Provider.GetCollection(this, parameters);
        }

        /// <summary>
        /// Returns a tuple containing a "collection" Connection and a TeamProject objects, built from the arguments currently supplied to this cmdlet
        /// </summary>
        /// <param name="parameters">If specified, the values in this parameter will override the values originally supplied to the this</param>
        /// <returns>A tuple consisting of an instance of Connection (containing either a TfsTeamProjectCollection (Windows) 
        ///     or VssConnection (Core) object) and an instance of TeamProject</returns>
        internal virtual (TfsConnection, WebApiTeamProject) GetCollectionAndProject(ParameterDictionary parameters = null)
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
        internal virtual (TfsConnection, WebApiTeamProject, WebApiTeam) GetCollectionProjectAndTeam(ParameterDictionary parameters = null)
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
        protected virtual T GetClient<T>(ClientScope scope = ClientScope.Collection, ParameterDictionary parameters = null)
            where T : VssHttpClientBase
        {
            return Provider.GetInstanceOf<TfsConnection>(this, parameters, scope.ToString()).GetClient<T>();
        }

        /// <summary>
        /// Returns an instance of the specified service
        /// </summary>
        /// <typeparam name="T">The type of the requested service.static Must derive from IService</typeparam>
        /// <returns>An instance of T, as provided by the current service provider</returns>
        protected virtual T GetService<T>() where T : IService
        {
            return Provider.GetService<T>(this);
        }

        protected virtual TObj GetInstanceOf<TObj>(ParameterDictionary parameters = null, object userState = null) where TObj : class
        {
            return Provider.GetInstanceOf<TObj>(this, parameters, userState);
        }

        protected virtual IEnumerable<TObj> GetCollectionOf<TObj>(ParameterDictionary parameters = null, object userState = null) where TObj : class
        {
            return Provider.GetCollectionOf<TObj>(this, parameters, userState);
        }

        protected virtual string GetCurrentDirectory()
        {
            return this.SessionState.Path.CurrentFileSystemLocation.Path;
        }

        protected void Log(string message, string commandName = null, bool force = false)
        {
            if (!IsVerbose) return;

            if (string.IsNullOrEmpty(commandName))
            {
                commandName = this.CommandName;
            }

            this.WriteVerbose($"[{DateTime.Now:HH:mm:ss.ffff}] [{commandName}] {message}");
        }

        protected void LogParameters()
        {
            if (!IsVerbose) return;

            var parms = new ParameterDictionary(this);

            if (parms.ContainsKey("Password") && parms["Password"] != null)
            {
                parms["Password"] = "***";
            }

            Log("ARGS: " + JObject.FromObject(parms)
                .ToString(Formatting.None)
                .Replace("\":", "\" = ")
                .Replace(",\"", "; \"")
                .Trim('{', '}')
            );
        }

        private bool IsVerbose
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
    }
}