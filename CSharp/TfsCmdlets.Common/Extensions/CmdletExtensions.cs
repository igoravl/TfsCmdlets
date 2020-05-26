using System.Management.Automation;
using System.Reflection;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Services;

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
            return cmdlet.GetOne<VssClientCredentials>();
        }

        internal static Connection GetServer(this Cmdlet cmdlet)
        {
            return GetConnection(cmdlet, "Server");
        }

        internal static Connection GetCollection(this Cmdlet cmdlet)
        {
            return GetConnection(cmdlet, "Collection");
        }

        internal static TeamProject GetProject(this Cmdlet cmdlet)
        {
            return cmdlet.GetOne<TeamProject>();
        }

        internal static string GetCommandName(this Cmdlet cmdlet)
        {
            var attr = cmdlet.GetType().GetCustomAttribute<CmdletAttribute>();

            return attr == null ? cmdlet.GetType().Name : $"{attr.VerbName}-{Consts.DEFAULT_PREFIX}{attr.NounName}";
        }

        private static Connection GetConnection(Cmdlet cmdlet, string connectionType)
        {
            return cmdlet.GetOne<Connection>(connectionType);
        }
    }
}