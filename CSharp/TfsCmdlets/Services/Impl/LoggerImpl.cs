using System;
using System.Composition;
using System.Linq;
using System.Management.Automation;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(ILogger)), Shared]
    internal class LoggerImpl : ILogger
    {
        private IPowerShellService PowerShell { get; set; }
        public IParameterManager ParameterManager { get; }

        private readonly string[] _hiddenParameters = new[]{"Password", "PersonalAccessToken"};

        public void Log(string message, string commandName = null)
        {
            if(!PowerShell.IsVerbose) return;
            
            PowerShell.WriteVerbose($"[{DateTime.Now:HH:mm:ss.ffff}] [{(commandName?? PowerShell.CurrentCommand)}] {message}");
        }

        public void LogError(string message)
        {
            throw new NotImplementedException();
        }

        public void LogError(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void LogWarn(string message)
        {
            PowerShell.WriteWarning(message);
        }

        
        public void LogParameters()
        {
            if (!PowerShell.IsVerbose) return;

            var parms = ParameterManager.GetParameters();

            foreach (var key in parms.Keys.ToList()) 
            {
                if (_hiddenParameters.Any(p => p.Equals(key, StringComparison.OrdinalIgnoreCase)))
                {
                    parms[key] = "*****";
                }

                if(parms[key] is SwitchParameter switchParameter)
                {
                    parms[key] = switchParameter.IsPresent;
                }
            }

            foreach(var hidden in _hiddenParameters)
            {
            }

            var hasParameterSetName = parms.ContainsKey("ParameterSetName");
            var args = Newtonsoft.Json.Linq.JObject.FromObject(parms)
                    .ToString(Newtonsoft.Json.Formatting.None)
                    .Replace("\":", "\" = ")
                    .Replace(",\"", "; \"");

            Log($"Running cmdlet with {(hasParameterSetName? $"parameter set '{parms.Get<string>("ParameterSetName")}' and ": "")}the following arguments: {args}");

        }

        [ImportingConstructor]
        public LoggerImpl(IPowerShellService powerShell, IParameterManager parameterManager)
        {
            PowerShell = powerShell;
            ParameterManager = parameterManager;
        }
     }
}
