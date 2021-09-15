using System;
using System.Collections.Generic;
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
        [Inject] protected IPowerShellService PSService { get; set; }
        [Inject] protected IParameterManager ParameterManager { get; set; }
        [Inject] protected IEnumerable<ICommand> Commands { get; set; }

        // internal ServiceLocator Locator { get; private set; }

        protected string Verb { get; private set; }

        public CmdletBase()
        {
            Verb = GetVerb();
        }

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
        protected sealed override void BeginProcessing()
        {
            base.BeginProcessing();

            CheckWindowsOnly();
            InjectDependencies();
            // CheckRequiredVersion();
            // LogParameters();
            DoBeginProcessing();
        }

        /// <inheritdoc/>
        protected sealed override void EndProcessing()
        {
            DoEndProcessing();
            CleanUpContext();

            base.EndProcessing();
        }

        /// <inheritdoc/>
        protected sealed override void ProcessRecord()
        {
            DoProcessRecord();
        }

        protected virtual void DoBeginProcessing() { }

        protected virtual void DoEndProcessing() { }

        /// <summary>
        /// Performs execution of the command.
        /// </summary>
        protected virtual void DoProcessRecord()
        {
            var result = DoInvokeCommand();

            if (result != null)
            {
                WriteObject(result, true);
            }
        }

        private IEnumerable<object> DoInvokeCommand()
        {
            var command = Commands.FirstOrDefault(c => c.CommandName.Equals(CommandName, StringComparison.OrdinalIgnoreCase));

            if (command == null) throw new Exception($"Command '{CommandName}' not found. Are you missing a [Command] attribute?");

            var parameters = ParameterManager.GetParameters(this);

            return command.InvokeCommand(parameters) as IEnumerable<object>;
        }

        private void InjectDependencies()
        {
            var locator = ServiceLocator.Instance;

            locator.PushContext(this);
            locator.Inject(this);
        }

        private void CleanUpContext()
        {
            ServiceLocator.Instance.PopContext();
        }

        private string GetVerb()
        {
            var attr = GetType().GetCustomAttribute<CmdletAttribute>();
            if (attr == null) throw new InvalidOperationException($"CmdletAttribute not found on cmdlet {GetType().FullName}");

            return attr.VerbName;
        }

        /// <summary>
        /// Check whether the currently executing environment is Windows PowerShell
        /// </summary>
        /// <throws>For cmdlets which are "Windows-only", a call to this method will throw a 
        ///     NotSupportedException when running on PowerShell Core.</throws>
        private void CheckWindowsOnly()
        {
            const bool isDesktop = 
#if NETCOREAPP3_1_OR_GREATER
                false;
#elif NET471_OR_GREATER
                true;
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