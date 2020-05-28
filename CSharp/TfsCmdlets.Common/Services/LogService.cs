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

        void LogParameters();
    }

    internal class LoggerImpl : ILogger
    {
        private Cmdlet Cmdlet { get; set; }
        
        internal LoggerImpl(Cmdlet cmdlet)
        {
            Cmdlet = cmdlet;
        }

        public void Log(string message, string commandName = null, bool force = false)
        {
            // TODO: if (!IsVerbose(cmdlet)) return;

            if (string.IsNullOrEmpty(commandName)) commandName = Cmdlet.GetCommandName();

            Cmdlet.WriteVerbose($"[{DateTime.Now:HH:mm:ss.ffff}] [{commandName}] {message}");
        }

        public void LogParameters()
        {
            // TODO: if (!IsVerbose(cmdlet)) return;

            var parms = new ParameterDictionary(Cmdlet);

            if (parms.ContainsKey("Password") && parms["Password"] != null) parms["Password"] = "***";

            Log("ARGS: " + JObject.FromObject(parms)
                    .ToString(Formatting.None)
                    .Replace("\":", "\" = ")
                    .Replace(",\"", "; \"")
                    .Trim('{', '}')
            );
        }
    }
}