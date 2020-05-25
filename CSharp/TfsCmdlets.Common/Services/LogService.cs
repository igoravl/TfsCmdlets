using System;
using System.Management.Automation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Services
{
    internal interface ILogService
    {
        void Log(string message, string commandName = null, bool force = false);

        void LogParameters();
    }

    [Exports(typeof(ILogService))]
    internal class LogServiceImpl : IService<ILogService>, ILogService
    {
        ILogService IService<ILogService>.Get(object userState)
        {
            return this;
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

            var parms = Cmdlet.GetParameters();

            if (parms.ContainsKey("Password") && parms["Password"] != null) parms["Password"] = "***";

            Log("ARGS: " + JObject.FromObject(parms)
                    .ToString(Formatting.None)
                    .Replace("\":", "\" = ")
                    .Replace(",\"", "; \"")
                    .Trim('{', '}')
            );
        }

        //private static bool IsVerbose(Cmdlet cmdlet)
        //{
        //    var containsVerbose = cmdlet.MyInvocation.BoundParameters.ContainsKey("Verbose");

        //    if (containsVerbose)
        //    {
        //        return ((SwitchParameter)cmdlet.MyInvocation.BoundParameters["Verbose"]).ToBool();
        //    }

        //    return (ActionPreference)cmdlet.GetVariableValue("VerbosePreference") != ActionPreference.SilentlyContinue;
        //}
        public object Get(object userState = null)
        {
            return null;
        }

        public ICmdletServiceProvider Provider { get; set; }

        public Cmdlet Cmdlet { get; set; }

    }
}