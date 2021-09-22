using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
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

        string Edition { get; }

        string WindowTitle { get; set; }

        PSModuleInfo Module { get; }

        string CurrentCommand {get;}

        bool ShouldProcess(string target, string action);

        bool ShouldProcess(TeamProject tp, string action);

        bool ShouldProcess(Connection collection, string action);

        bool ShouldContinue(string query, string caption = null);

        T InvokeScript<T>(string script, params object[] arguments);

        object InvokeScript(string script, params object[] arguments);

        object InvokeScript(string script, Dictionary<string, object> variables);

        object InvokeScript(string script, bool useNewScope, PipelineResultTypes writeToPipeline, IList input, params object[] args);
    }
}