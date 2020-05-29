using System;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Services;

namespace TfsCmdlets.Extensions
{
    internal static class CmdletExtensions
    {
        internal static object InvokeScript(this PSCmdlet cmdlet, string script, params object[] arguments)
        {
            return cmdlet.InvokeCommand.InvokeScript(script, arguments);
        }

        internal static T InvokeScript<T>(this PSCmdlet cmdlet, string script, params object[] arguments)
        {
            var obj = cmdlet.InvokeCommand.InvokeScript(script, arguments)?.FirstOrDefault()?.BaseObject;

            return (T) obj;
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

        internal static (Connection, TeamProject) GetCollectionAndProject(this Cmdlet cmdlet)
        {
            return (cmdlet.GetCollection(), cmdlet.GetOne<TeamProject>());
        }

        internal static (Connection, TeamProject, WebApiTeam) GetCollectionProjectAndTeam(this Cmdlet cmdlet)
        {
            return (cmdlet.GetCollection(), cmdlet.GetOne<TeamProject>(), cmdlet.GetOne<WebApiTeam>());
        }

        internal static string GetCommandName(this Cmdlet cmdlet)
        {
            var attr = cmdlet.GetType().GetCustomAttribute<CmdletAttribute>();

            return attr == null ? cmdlet.GetType().Name : $"{attr.VerbName}-{Consts.DEFAULT_PREFIX}{attr.NounName}";
        }

        private static Connection GetConnection(Cmdlet cmdlet, string connectionType, ParameterDictionary overriddenParameters = null)
        {
            return cmdlet.GetOne<Connection>(overriddenParameters, connectionType);
        }
    }
}