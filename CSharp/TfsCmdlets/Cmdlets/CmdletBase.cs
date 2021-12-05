using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using TfsCmdlets.Services;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets
{
    /// <summary>
    /// Abstract class from which all TfsCmdlets cmdlets derive
    /// </summary>
    public abstract class CmdletBase : PSCmdlet
    {
        [Import] protected IPowerShellService PSService { get; set; }
        [Import] protected IParameterManager Parameters { get; set; }
        [Import] protected ILogger Logger { get; set; }
        [ImportMany] protected IEnumerable<Lazy<IController>> Controllers { get; set; }

        internal IController GetController()
            => Controllers.FirstOrDefault(c =>
                c.Value.CommandName.Equals(CommandName, StringComparison.OrdinalIgnoreCase) ||
                c.Value.CommandName.Equals(CommandName + "Controller", StringComparison.OrdinalIgnoreCase))?.Value;

        protected string Verb { get; private set; }

        public CmdletBase()
        {
            Verb = GetVerb();
        }

        /// <summary>
        /// Returns the PowerShell command name of this cmdlet
        /// </summary>
        /// <value>The name of the this, as defined by the [Cmdlet] attribute. If the attribute is missing, returns the class name.</value>
        internal virtual string DisplayName
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

            CheckWindowsOnly();
            InjectDependencies();
            // CheckRequiredVersion();
            LogParameters();

            DoBeginProcessing();
        }

        /// <inheritdoc/>
        protected sealed override void EndProcessing()
        {
            DoEndProcessing();

            Parameters.Reset();

            base.EndProcessing();
        }

        /// <inheritdoc/>
        protected sealed override void ProcessRecord() => DoProcessRecord();

        protected virtual void DoBeginProcessing() { }

        protected virtual void DoEndProcessing() { }

        /// <summary>
        /// Performs execution of the command.
        /// </summary>
        protected virtual void DoProcessRecord()
        {
            try
            {
                var result = DoInvokeCommand();

                if (result == null) return;

                if (!ReturnsValue)
                {
                    foreach (var _ in result) { } // forces enumeration of iterator

                    return;
                }

                WriteObject(result, true);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }

        private IEnumerable<object> DoInvokeCommand()
        {
            var command = GetController();

            if (command == null) throw new Exception($"Controller '{CommandName}' not found. Are you missing a [CmdletController] attribute?");

            Parameters.Initialize(this);

            var result = command.InvokeCommand();

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
            Logger.LogParameters();
        }

        private string GetVerb()
        {
            var attr = GetType().GetCustomAttribute<CmdletAttribute>();
            if (attr == null) throw new InvalidOperationException($"CmdletAttribute not found on cmdlet {GetType().FullName}");

            return attr.VerbName;
        }

        protected virtual bool ReturnsValue 
            => IsPassthru || GetVerb().Equals("Get", StringComparison.OrdinalIgnoreCase);

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
        /// Check whether the currently executing environment is Windows PowerShell
        /// </summary>
        /// <throws>For cmdlets which are "Windows-only", a call to this method will throw a 
        ///     NotSupportedException when running on PowerShell Core.</throws>
        private void CheckWindowsOnly()
        {
#if NETCOREAPP3_1_OR_GREATER
            const bool isDesktop = false;
#elif NET471_OR_GREATER
            const bool isDesktop = true;
#else
#error Unsupported platform
#endif

            if (isDesktop || GetType().GetCustomAttribute<DesktopOnlyAttribute>() == null)
            {
                return;
            }

            ErrorUtil.ThrowDesktopOnlyCmdlet();
        }
    }
}