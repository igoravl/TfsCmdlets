using System.Management.Automation;
using System.Reflection;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Extensions
{
    internal static class CmdletExtensions
    {
        internal static object InvokeScript(this PSCmdlet cmdlet, string script, params object[] arguments)
        {
            return cmdlet.InvokeCommand.InvokeScript(script, arguments);
        }

        internal static VssClientCredentials GetCredentials(this Cmdlet cmdlet)
        {
            return cmdlet.GetService<VssClientCredentials>().Get();
        }

        internal static VssConnection GetCollection(this Cmdlet cmdlet)
        {
            return Get(cmdlet, "Collection");
        }

        internal static VssConnection GetServer(this Cmdlet cmdlet)
        {
            return Get(cmdlet, "Server");
        }

        internal static string GetCommandName(this Cmdlet cmdlet)
        {
            var attr = cmdlet.GetType().GetCustomAttribute<CmdletAttribute>();

            return attr == null ? cmdlet.GetType().Name : $"{attr.VerbName}-{Consts.DEFAULT_PREFIX}{attr.NounName}";
        }


        private static VssConnection Get(Cmdlet cmdlet, string connectionType)
        {
            return cmdlet.GetService<VssConnection>().Get(connectionType);
        }


    }
}