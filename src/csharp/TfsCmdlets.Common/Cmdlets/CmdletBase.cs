using System.Reflection;
using System.Runtime.Versioning;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets
{
    /// <summary>
    /// Abstract class from which all TfsCmdlets cmdlets derive
    /// </summary>
    public abstract class CmdletBase : PSCmdlet
    {
        private static readonly IEnumerable<string> _valueReturningVerbs = new[] { "Get", "Test", "Invoke", "Export", "Search" };

        [Import] protected IPowerShellService PowerShell { get; set; }
        [Import] protected IParameterManager Parameters { get; set; }
        [Import] protected ILogger Logger { get; set; }
        [Import] protected IRuntimeUtil RuntimeUtil { get; set; }
        [ImportMany] protected IEnumerable<Lazy<IController>> Controllers { get; set; }

        public IController GetController()
            => Controllers.FirstOrDefault(c =>
                    c.Value.CommandName.Equals(CommandName, StringComparison.OrdinalIgnoreCase) ||
                    c.Value.CommandName.Equals(CommandName + "Controller", StringComparison.OrdinalIgnoreCase) ||
                    (c.Value.Verb.Equals(Verb) && c.Value.DataType == GetDataType())
                )?.Value;

        public Type GetDataType()
        {
            var attr = GetType().GetCustomAttribute<TfsCmdletAttribute>();
            return attr.DataType ?? attr.OutputType ?? typeof(object);
        }

        protected string Verb { get; private set; }

        public CmdletBase()
        {
            Verb = GetVerb();
        }

        /// <summary>
        /// Returns the PowerShell command name of this cmdlet
        /// </summary>
        /// <value>The name of the this, as defined by the [Cmdlet] attribute. If the attribute is missing, returns the class name.</value>
        public virtual string DisplayName
        {
            get
            {
                var attr = GetType().GetCustomAttribute<CmdletAttribute>();
                return attr == null ? GetType().Name : $"{attr.VerbName}-{attr.NounName}";
            }
        }

        /// <summary>
        /// Returns the type name for the underlying IController implementing the logic of this cmdlet
        /// </summary>
        /// <value>The name of the class. If not overridden in derived classes, 
        /// returns the name of the cmdlet class (by convention, cmdlet and command have the same type name).</value>
        protected virtual string CommandName => GetType().Name;

        /// <summary>
        /// Performs initialization of the command execution, logs the supplied parameters and check whether the current 
        /// this is tagged as "Windows-only". If so, throws an exception
        /// </summary>
        protected sealed override void BeginProcessing()
        {
            base.BeginProcessing();

            CheckHostingEnvironment();
            InjectDependencies();
            // CheckRequiredVersion();

            DoBeginProcessing();
        }

        /// <inheritdoc/>
        protected sealed override void ProcessRecord() => DoProcessRecord();

        /// <inheritdoc/>
        protected sealed override void EndProcessing()
        {
            DoEndProcessing();

            // Parameters.Reset();

            base.EndProcessing();
        }

        protected virtual void DoBeginProcessing() { Logger.Log("BeginProcessing"); }

        protected virtual void DoEndProcessing() { Logger.Log("EndProcessing"); }

        /// <summary>
        /// Performs execution of the command.
        /// </summary>
        protected virtual void DoProcessRecord()
        {
            try
            {
                Logger.Log("ProcessRecord");

                var results = DoInvokeCommand(); //.ToList();

                if (results == null) return;

                PowerShell.StartPipeline();

                foreach (var result in results)
                {
                    if (PowerShell.CtrlCIsPressed()) throw new PipelineStoppedException();

                    if (!ReturnsValue || result == null) continue;

                    WriteObject(result, true);
                }
            }
            catch (PipelineStoppedException)
            {
                throw; // Bubble up
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
            finally
            {
                PowerShell.EndPipeline();
            }
        }

        private IEnumerable<object> DoInvokeCommand()
        {
            var controller = GetController();

            if (controller == null) throw new Exception($"Controller '{CommandName}Controller' not found. Are you missing a [CmdletController] attribute?");

            Parameters.Initialize(this);
            LogParameters();

            var result = controller.InvokeCommand();

            if (result is IEnumerable<object> objList) return objList;

            return new[] { result };
        }

        private void InjectDependencies()
        {
            var locator = ServiceLocator.Instance;
            locator.SatisfyImports(this);
        }

        private void LogParameters()
        {
            Logger.LogParameters(DisplayName, Parameters);
        }

        private string GetVerb()
        {
            var attr = GetType().GetCustomAttribute<CmdletAttribute>();
            if (attr == null) throw new InvalidOperationException($"CmdletAttribute not found on cmdlet {GetType().FullName}");

            return attr.VerbName;
        }

        protected virtual bool ReturnsValue
            => IsPassthru || _valueReturningVerbs.Any(v => v.Equals(GetVerb(), StringComparison.OrdinalIgnoreCase));

        private bool IsPassthru
        {
            get
            {
                var parms = this.MyInvocation.BoundParameters;
                var hasPassthru = parms.Keys.Any(k => k.Equals("Passthru", StringComparison.OrdinalIgnoreCase));
                var isPassthru = hasPassthru && ((SwitchParameter)parms["Passthru"]).IsPresent;

                return isPassthru;
            }
        }


        /// <summary>
        /// Check whether the currently executing environment supports the cmdlet's requirements.
        /// </summary>
        /// <throws>For cmdlets which are "Windows-only", a call to this method will throw a 
        ///     NotSupportedException when running on PowerShell Core.</throws>
        private void CheckHostingEnvironment()
        {
            var requiresDesktop = (bool)GetType().GetCustomAttribute<TfsCmdletAttribute>()?.DesktopOnly;
            var requiresWindows = (bool)GetType().GetCustomAttribute<TfsCmdletAttribute>()?.WindowsOnly;

            if (requiresDesktop && !RuntimeUtil.Platform.Equals("Desktop")) ErrorUtil.ThrowDesktopOnlyCmdlet();
            if (requiresWindows && !RuntimeUtil.OperatingSystem.Equals("Windows")) ErrorUtil.ThrowWindowsOnlyCmdlet();
        }
    }
}