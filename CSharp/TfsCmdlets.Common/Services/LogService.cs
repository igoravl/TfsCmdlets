using System;
using System.Management.Automation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Services
{
    internal interface ILogger
    {
        void Log(string message, string commandName = null, bool force = false);

        void LogWarn(string message);

        void LogError(Exception ex, string errorId = null, ErrorCategory category = ErrorCategory.NotSpecified, object targetObject = null);

        void LogParameters();
    }

    internal class LoggerImpl : ILogger
    {
        private CmdletBase Cmdlet { get; set; }
        
        internal LoggerImpl(CmdletBase cmdlet)
        {
            Cmdlet = cmdlet;
        }

        public void Log(string message, string commandName = null, bool force = false)
        {
            if (!Cmdlet.IsVerbose) return;

            if (string.IsNullOrEmpty(commandName)) commandName = Cmdlet.CommandName;

            Cmdlet.WriteVerbose($"[{DateTime.Now:HH:mm:ss.ffff}] [{commandName}] {message}");
        }

        public void LogWarn(string message)
        {
            Cmdlet.WriteWarning(message);
        }

        public void LogError(Exception ex, string errorId = null, ErrorCategory category = ErrorCategory.NotSpecified, object targetObject = null)
        {
            var innerException = ex.InnerException ?? ex;
            var id = errorId?? innerException.GetType().Name;

            Cmdlet.WriteError(new ErrorRecord(ex, id, category, targetObject));
        }

        public void LogParameters()
        {
            if (!Cmdlet.IsVerbose) return;

            var parms = new ParameterDictionary(Cmdlet);

            if (parms.ContainsKey("Password") && parms["Password"] != null) parms["Password"] = "***";

            Log($"Running cmdlet with parameter set '{parms.Get<string>("ParameterSetName")}' and the following arguments:");

            Log(JObject.FromObject(parms)
                    .ToString(Formatting.None)
                    .Replace("\":", "\" = ")
                    .Replace(",\"", "; \"")
                    .Trim('{', '}')
            );
        }
    }
}