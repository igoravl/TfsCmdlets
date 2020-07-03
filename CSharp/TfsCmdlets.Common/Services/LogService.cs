﻿using System;
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