using System;
using System.Collections.Generic;
using System.Management.Automation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TfsCmdlets.Extensions;
using TfsCmdlets.ServiceProvider;

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

        public ICmdletServiceProvider Provider { get; set; }
        public Cmdlet Cmdlet { get; set; }
        public ParameterDictionary Parameters { get; set; }
        ILogService IService<ILogService>.GetOne(object userState) => this;
        public IEnumerable<ILogService> GetMany(object userState = null) => throw new NotImplementedException();
    }
}