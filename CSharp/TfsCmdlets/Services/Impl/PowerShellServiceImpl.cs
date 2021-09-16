using System;
using System.Collections.Generic;
using System.Composition;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IPowerShellService))]
    internal class PowerShellServiceImpl : IPowerShellService
    {
        private ICmdletContextManager ContextManager { get; }

        private CmdletBase Cmdlet  => ContextManager.Current;

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

        public bool ShouldProcess(TpcConnection collection, string action)
            => ShouldProcess($"Team Project Collection '{collection.DisplayName}'", action);

        public bool ShouldContinue(string query, string caption = null)
            => Cmdlet.ShouldContinue(query, caption);

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
        public PowerShellServiceImpl(ICmdletContextManager contextManager)
        {
            ContextManager = contextManager;
        }
    }
}