using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Models;
using OsProcess = System.Diagnostics.Process;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IPowerShellService))]
    public class PowerShellServiceImpl : IPowerShellService
    {
        private System.Management.Automation.PowerShell PowerShell { get; } =
            System.Management.Automation.PowerShell.Create(RunspaceMode.CurrentRunspace);

        private IRuntimeUtil RuntimeUtil { get; set; }

        private static PropertyInfo _ExecutionContextProperty;
        private static PropertyInfo _CurrentCommandProcessor;
        private static PropertyInfo _Command;

        public CmdletBase CurrentCmdlet
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

                return (CmdletBase)command;
            }
        }

        public string CurrentCommand => CurrentCmdlet.CmdletDisplayName;

        public string CurrentCommandLine => CurrentCmdlet.MyInvocation.Line.Trim();

        public string WindowTitle { get => CurrentCmdlet.Host.UI.RawUI.WindowTitle; set => CurrentCmdlet.Host.UI.RawUI.WindowTitle = value; }

        public PSModuleInfo Module => CurrentCmdlet.MyInvocation.MyCommand.Module;

        public void WriteObject(object items, bool enumerateCollection = true)
            => CurrentCmdlet.WriteObject(items, enumerateCollection);

        public void WriteVerbose(string message)
            => CurrentCmdlet.WriteVerbose(message);

        public void WriteWarning(string message)
            => CurrentCmdlet.WriteWarning(message);

        public void WriteError(string message)
            => WriteError(new Exception(message));

        public void WriteError(Exception ex)
            => WriteError(new ErrorRecord(ex, "", ErrorCategory.NotSpecified, null));

        public void WriteError(ErrorRecord errorRecord)
            => CurrentCmdlet.WriteError(errorRecord);

        public IReadOnlyDictionary<string, object> GetBoundParameters()
            => new Dictionary<string, object>(CurrentCmdlet.MyInvocation.BoundParameters);

        public object GetVariableValue(string name)
            => CurrentCmdlet.GetVariableValue(name);

        // public void SetVariableValue(string name, object value)
        //   => ...;

        public bool ShouldProcess(string target, string action)
            => CurrentCmdlet.ShouldProcess(target, action);

        public bool ShouldProcess(Connection collection, string action)
            => ShouldProcess($"{(collection.IsHosted ? "Organization" : "Team Project Collection")} '{collection.DisplayName}'", action);

        public bool ShouldProcess(WebApiTeamProject tp, string action)
            => ShouldProcess($"Team Project '{tp.Name}'", action);

        public bool ShouldProcess(WebApiTeam t, string action)
            => ShouldProcess($"Team '{t.Name}'", action);

        public bool ShouldContinue(string query, string caption = null)
            => CurrentCmdlet.ShouldContinue(query, caption);

        /// <summary>
        /// Executes a PowerShell script in the current session context
        /// </summary>
        /// <param name="script">A string containing a valid PS script</param>
        /// <param name="arguments">Arguments passed to the script, represented as an array named <c>$args</c></param>
        /// <returns>The output of the script, if any</returns>
        public virtual object InvokeScript(string script, params object[] arguments)
            => CurrentCmdlet.InvokeCommand.InvokeScript(script, arguments);

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
            => (T)CurrentCmdlet.InvokeCommand.InvokeScript(script, arguments)?.FirstOrDefault()?.BaseObject;

        public object InvokeScript(string script, bool useNewScope, PipelineResultTypes writeToPipeline, IList input, params object[] args)
            => CurrentCmdlet.InvokeCommand.InvokeScript(script, useNewScope, writeToPipeline, input, args);

        /// <summary>
        /// Gets the current directory in PowerShell
        /// </summary>
        public string GetCurrentDirectory()
        {
            return CurrentCmdlet.SessionState.Path.CurrentFileSystemLocation.Path;
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

        public void StartPipeline()
        {
            if (!IsInteractive) return;

            try
            {
                Console.TreatControlCAsInput = true;
            }
            catch { }
        }

        public void EndPipeline()
        {
            if (!IsInteractive) return;

            try
            {
                Console.TreatControlCAsInput = false;
            }
            catch { }
        }

        public bool CtrlCIsPressed()
        {
            if (!IsInteractive) return false;

            try
            {
                if (!Console.KeyAvailable) return false;
                var key = Console.ReadKey(true);

                return key.Key == ConsoleKey.C && key.Modifiers == ConsoleModifiers.Control;
            }
            catch
            {
                return false;
            }
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

        public bool IsInteractive => Environment.UserInteractive &&
            !Environment.GetCommandLineArgs().Any(x => x.Equals("-NonInteractive", StringComparison.OrdinalIgnoreCase));

        public string Edition => RuntimeUtil.Platform;

        
        public IntPtr WindowHandle {
            get
            {
                if(!IsInteractive || !RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return IntPtr.Zero;

                var process = OsProcess.GetCurrentProcess();

                return process.WindowHandleRecursive();
            }
        }

        [ImportingConstructor]
        public PowerShellServiceImpl(IRuntimeUtil runtimeUtil)
        {
            RuntimeUtil = runtimeUtil;
        }
    }
}