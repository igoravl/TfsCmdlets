using System;
using System.Collections;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IPowerShellService))]
    internal class PowerShellServiceImpl : IPowerShellService
    {
        private System.Management.Automation.PowerShell PowerShell { get; } =
            System.Management.Automation.PowerShell.Create(RunspaceMode.CurrentRunspace);

        private static PropertyInfo _ExecutionContextProperty;
        private static PropertyInfo _CurrentCommandProcessor;
        private static PropertyInfo _Command;

        private CmdletBase Cmdlet
        {
            get
            {
                var executionContext = (_ExecutionContextProperty ??= PowerShell.Runspace.GetType()
                    .GetProperty("ExecutionContext", BindingFlags.Instance | BindingFlags.NonPublic))
                    .GetValue(PowerShell.Runspace);

                var commandProcessor = (_CurrentCommandProcessor ??= executionContext.GetType()
                    .GetProperty("CurrentCommandProcessor", BindingFlags.Instance | BindingFlags.NonPublic))
                    .GetValue(executionContext);

                var command = (_Command ??= commandProcessor.GetType()
                    .GetProperty("Command", BindingFlags.Instance | BindingFlags.NonPublic))
                    .GetValue(commandProcessor);

                return (CmdletBase) command;
            }
        }

        public string CurrentCommand => Cmdlet.DisplayName;

        public string WindowTitle { get => Cmdlet.Host.UI.RawUI.WindowTitle; set => Cmdlet.Host.UI.RawUI.WindowTitle = value; }

        public PSModuleInfo Module => Cmdlet.MyInvocation.MyCommand.Module;

        public void WriteObject(object items, bool enumerateCollection = true)
            => Cmdlet.WriteObject(items, enumerateCollection);

        public void WriteVerbose(string message)
            => Cmdlet.WriteVerbose(message);

        public void WriteWarning(string message)
            => Cmdlet.WriteWarning(message);

        public void WriteError(string message)
            => WriteError(new Exception(message));

        public void WriteError(Exception ex)
            => WriteError(new ErrorRecord(ex, "", ErrorCategory.NotSpecified, null));

        public void WriteError(ErrorRecord errorRecord)
            => Cmdlet.WriteError(errorRecord);

        public IReadOnlyDictionary<string, object> GetBoundParameters()
            => new Dictionary<string, object>(Cmdlet.MyInvocation.BoundParameters);

        public object GetVariableValue(string name)
            => Cmdlet.GetVariableValue(name);

        public void SetVariableValue(string name, object value)
            => throw new NotImplementedException();

        public bool ShouldProcess(string target, string action)
            => Cmdlet.ShouldProcess(target, action);

        public bool ShouldProcess(TeamProject tp, string action)
            => ShouldProcess($"Team Project '{tp.Name}'", action);

        public bool ShouldProcess(Connection collection, string action)
            => ShouldProcess($"Team Project Collection '{collection.DisplayName}'", action);

        public bool ShouldContinue(string query, string caption = null)
            => Cmdlet.ShouldContinue(query, caption);

        /// <summary>
        /// Executes a PowerShell script in the current session context
        /// </summary>
        /// <param name="script">A string containing a valid PS script</param>
        /// <param name="arguments">Arguments passed to the script, represented as an array named <c>$args</c></param>
        /// <returns>The output of the script, if any</returns>
        public virtual object InvokeScript(string script, params object[] arguments)
            => Cmdlet.InvokeCommand.InvokeScript(script, arguments);

        /// <summary>
        /// Executes a PowerShell script in the current session context
        /// </summary>
        /// <param name="script">A string containing a valid PS script</param>
        /// <param name="variables">Variables passed to the script</param>
        /// <returns>The output of the script, if any</returns>
        public virtual object InvokeScript(string script, Dictionary<string, object> variables)
            => ScriptBlock.Create(script).InvokeWithContext(null, variables.Select(kvp => new PSVariable(kvp.Key, kvp.Value)).ToList());

        /// <summary>
        /// Executes a PowerShell script in the current session context
        /// </summary>
        /// <param name="script">A string containing a valid PS script</param>
        /// <param name="arguments">Arguments passed to the script, represented as an array named <c>$args</c></param>
        /// <typeparam name="T">The expected type of the objects outputted by the script</typeparam>
        /// <returns>The output of the script, if any</returns>
        public T InvokeScript<T>(string script, params object[] arguments)
            => (T)Cmdlet.InvokeCommand.InvokeScript(script, arguments)?.FirstOrDefault()?.BaseObject;

        public object InvokeScript(string script, bool useNewScope, PipelineResultTypes writeToPipeline, IList input, params object[] args)
            => Cmdlet.InvokeCommand.InvokeScript(script, useNewScope, writeToPipeline, input, args);

 /// <summary>
        /// Gets the current directory in PowerShell
        /// </summary>
        public string GetCurrentDirectory()
        {
            return Cmdlet.SessionState.Path.CurrentFileSystemLocation.Path;
        }

        /// <summary>
        /// Gets the current directory in PowerShell
        /// </summary>
        public string ResolvePath(string basePath, string path = "")
        {
            var relativePath = Path.Combine(basePath ?? GetCurrentDirectory(), path);

            if (!Path.IsPathRooted(relativePath))
            {
                relativePath = Path.Combine(GetCurrentDirectory(), relativePath);
            }

            return Path.GetFullPath(relativePath);
        }
        public bool IsVerbose
        {
            get
            {

                var parms = GetBoundParameters();
                var containsVerbose = parms.ContainsKey("Verbose");

                return containsVerbose ?
                    ((SwitchParameter)parms["Verbose"]).ToBool() :
                    ((ActionPreference)GetVariableValue("VerbosePreference")) != ActionPreference.SilentlyContinue;
            }
        }

        public string Edition =>
#if NETCOREAPP3_1_OR_GREATER
            "Core";
#elif NET47_OR_GREATER
            "Desktop";
#else
#error Unsupported platform
#endif

        [ImportingConstructor]
        public PowerShellServiceImpl()
        {
        }
    }
}