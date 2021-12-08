using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using WebApiPublisher = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher;

namespace TfsCmdlets.Cmdlets.ServiceHook
{
    /// <summary>
    /// Gets one or more service hook publishers.
    /// </summary>
    /// <remarks>
    /// Service hook publishers are the components inside of Azure DevOps that can publish (send) notifications triggered by 
    /// event such as "work item changed" or "build queued". Use this cmdlet to list the available publishers and get 
    /// the ID of the desired one to be able to manage service hook subscriptions.
    /// </remarks>
    [Cmdlet(VerbsCommon.Get, "TfsServiceHookPublisher")]
    [OutputType(typeof(WebApiPublisher))]
    [TfsCmdlet(CmdletScope.Collection)]
    partial class GetServiceHookPublisher
    {
        /// <summary>
        /// Specifies the name or ID of the service hook publisher to return. Wildcards are supported. 
        /// When omitted, returns all service hook consumers currently supported the current by Azure DevOps organization / 
        /// TFS collection.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name", "Id")]
        public object Publisher { get; set; } = "*";
    }
}