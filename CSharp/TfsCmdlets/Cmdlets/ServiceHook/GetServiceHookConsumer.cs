using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using WebApiConsumer = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer;

namespace TfsCmdlets.Cmdlets.ServiceHook
{
    /// <summary>
    /// Gets one or more service hook consumers.
    /// </summary>
    /// <remarks>
    /// Service hook consumers are the services that can consume (receive) notifications triggered by 
    /// Azure DevOps. Examples of consumers available out-of-box with Azure DevOps are Microsoft Teams, 
    /// Slack, Trello ou the generic WebHook consumer. Use this cmdlet to list the available consumers and get 
    /// the ID of the desired one to be able to manage service hook subscriptions.
    /// </remarks>
    [Cmdlet(VerbsCommon.Get, "TfsServiceHookConsumer")]
    [OutputType(typeof(WebApiConsumer))]
    [TfsCmdlet(CmdletScope.Collection)]
    partial class GetServiceHookConsumer
    {
        /// <summary>
        /// Specifies the name or ID of the service hook consumer to return. Wildcards are supported. 
        /// When omitted, all service hook consumers registered in the given project collection/organization 
        /// are returned.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name", "Id")]
        public string Consumer { get; set; } = "*";
    }
}