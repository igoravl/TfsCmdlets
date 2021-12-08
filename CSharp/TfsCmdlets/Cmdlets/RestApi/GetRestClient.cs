using System;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.RestApi
{
    /// <summary>
    /// Gets an Azure DevOps HTTP Client object instance.
    /// </summary>
    /// <remarks>
    /// Connection objects (Microsoft.VisualStudio.Services.Client.VssConnection in PowerShell Core, 
    /// Microsoft.TeamFoundation.Client.TfsTeamProjectCollection in Windows PowerShell) provide access to 
    /// many HTTP client objects such as Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient 
    /// that wrap many of the REST APIs exposed by Azure DevOps. Those clients inherit the authentication 
    /// information supplied by their parent connection object and can be used as a more convenient mechanism 
    /// to issue API calls.
    /// </remarks>
    [Cmdlet(VerbsCommon.Get, "TfsRestClient", DefaultParameterSetName = "Get by collection")]
    [OutputType(typeof(VssHttpClientBase))]
    [TfsCmdlet(CmdletScope.Collection)]
    partial class GetRestClient
    {
        /// <summary>
        /// Specifies the full type name (optionally including its assembly name) of the HTTP Client 
        /// class to return.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [Alias("Type")]
        public string TypeName { get; set; }
    }
}