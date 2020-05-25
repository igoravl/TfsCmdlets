using System;
using System.Management.Automation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TfsCmdlets.Extensions
{
    internal static class LogExtensions
    {
        internal static void Log(this PSCmdlet cmdlet, string message, string commandName = null, bool force = false)
        {
            if (!IsVerbose(cmdlet, force)) return;

            if (string.IsNullOrEmpty(commandName))
            {
                commandName = cmdlet.GetCommandName();
            }

            cmdlet.WriteVerbose($"[{DateTime.Now:HH:mm:ss.ffff}] [{commandName}] {message}");
        }

        internal static void LogParameters(this PSCmdlet cmdlet)
        {
            if (!IsVerbose(cmdlet)) return;

            var parms = cmdlet.GetParameters();

            if (parms.ContainsKey("Password") && parms["Password"] != null)
            {
                parms["Password"] = "***";
            }

            Log(cmdlet, "ARGS: " + JObject.FromObject(parms)
                            .ToString(Formatting.None)
                            .Replace("\":", "\" = ")
                            .Replace(",\"", "; \"") 
                            .Trim('{', '}')
                        );
        }

        //private static string GetLogSiteName(PSCmdlet cmdlet)
        //{
            //var cs = new Stack();
            //var callStack = cmdlet.InvokeCommand.InvokeScript("Get-PSCallStack").Select(o => o.BaseObject as CallStackFrame).Skip(1);

            //foreach (var frame in callStack)
            //{
            //    var cmd = GetCommandName(frame);

            //    cs.Push(cmd.Trim('_'));

            //    //if (cmd.Contains("-"))
            //    //{
            //    //    break;
            //    //}
            //}

        //    return cmdlet.MyInvocation.InvocationName;
        //}

        //private static string GetCommandName(CallStackFrame frame)
        //{
        //    if (frame.InvocationInfo == null)
        //    {
        //        return frame.FunctionName;
        //    }

        //    var commandInfo = frame.InvocationInfo.MyCommand;

        //    if (commandInfo == null)
        //    {
        //        return !string.IsNullOrEmpty(frame.InvocationInfo.InvocationName) ? 
        //            frame.InvocationInfo.InvocationName : 
        //            frame.Position.Text;
        //    }

        //    return !string.IsNullOrEmpty(commandInfo.Name) ? commandInfo.Name : frame.FunctionName;
        //}

        private static bool IsVerbose(PSCmdlet cmdlet, bool force = false)
        {
            if (force) return true;

            var containsVerbose = cmdlet.MyInvocation.BoundParameters.ContainsKey("Verbose");

            if (containsVerbose)
            {
                return ((SwitchParameter)cmdlet.MyInvocation.BoundParameters["Verbose"]).ToBool();
            }

            return (ActionPreference) cmdlet.GetVariableValue("VerbosePreference") != ActionPreference.SilentlyContinue;
        }
    }
}
