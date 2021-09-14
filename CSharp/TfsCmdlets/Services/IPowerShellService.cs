using System;
using System.Collections.Generic;
using System.Management.Automation;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Services
{
    public interface IPowerShellService
    {
        void WriteObject(object item, bool enumerateCollection = true);

        void WriteVerbose(string message);

        void WriteWarning(string message);

        void WriteError(string message);

        void WriteError(Exception ex);

        void WriteError(ErrorRecord errorRecord);   

        bool IsVerbose { get; }

        IReadOnlyDictionary<string, object> GetBoundParameters();
        
        object GetVariableValue(string name);

        void SetVariableValue(string name, object value);

        string Edition {get;}

        bool ShouldProcess(string target, string action);
    }
}