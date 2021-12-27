using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Management.Automation;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(ILogger)), Shared]
    public class LoggerImpl : ILogger
    {
        private IPowerShellService PowerShell { get; set; }

        private readonly string[] _hiddenParameters = new[]{"Password", "PersonalAccessToken"};

        public void Log(string message, string commandName = null)
        {
            if(!PowerShell.IsVerbose) return;
            
            PowerShell.WriteVerbose($"[{DateTime.Now:HH:mm:ss.ffff}] [{(commandName?? PowerShell.CurrentCommand)}] {message}");
        }

        public void LogError(string message)
        {
            LogError(new Exception(message));
        }

        public void LogError(Exception ex, string errorId = null, ErrorCategory category = ErrorCategory.NotSpecified, object targetObject = null)
        {
            var innerException = ex.InnerException ?? ex;
            var id = errorId ?? innerException.GetType().Name;

            PowerShell.WriteError(new ErrorRecord(ex, id, category, targetObject));
        }

        public void LogWarn(string message)
        {
            PowerShell.WriteWarning(message);
        }

        
        public void LogParameters(IParameterManager parameters)
        {
            if (!PowerShell.IsVerbose) return;

            var parms = new Dictionary<string, object>();

            foreach (var key in parameters.Keys) 
            {
                var value = parameters[key];
                
                if (_hiddenParameters.Any(p => p.Equals(key, StringComparison.OrdinalIgnoreCase)))
                {
                    parms[key] = "*****";
                    continue;
                }

                if(value is SwitchParameter switchParameter)
                {
                    parms[key] = switchParameter.IsPresent;
                    continue;
                }

                parms[key] = value;
            }

            var hasParameterSetName = parms.Keys.Contains("parametersetName");
            var args = Newtonsoft.Json.Linq.JObject.FromObject(parms)
                    .ToString(Newtonsoft.Json.Formatting.None)
                    .Replace("\":", "\" = ")
                    .Replace(",\"", "; \"");

            Log($"Running cmdlet with {(hasParameterSetName? $"parameter set '{parms["parametersetName"]}' and ": "")}the following arguments: {args}");

        }

        [ImportingConstructor]
        public LoggerImpl(IPowerShellService powerShell)
        {
            PowerShell = powerShell;
        }
     }
}
