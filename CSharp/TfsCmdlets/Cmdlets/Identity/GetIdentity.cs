using System.Management.Automation;
using WebApiIdentity = Microsoft.VisualStudio.Services.Identity.Identity;
using TfsQueryMembership = Microsoft.VisualStudio.Services.Identity.QueryMembership;

namespace TfsCmdlets.Cmdlets.Identity
{
    /// <summary>
    /// Gets one or more identities that represents either users or groups in Azure DevOps. 
    /// This cmdlets resolves legacy identity information for use with older APIs such as the Security APIs
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(WebApiIdentity))]
    partial class GetIdentity 
    {
        /// <summary>
        /// Specifies the user or group to be retrieved. Supported values are: 
        /// User/group name, email, or ID
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "Get Identity")]
        public object Identity { get; set; }

        /// <summary>
        /// Specifies how group membership information should be processed  
        /// when the returned identity is a group. "Direct" fetches direct members (both users 
        /// and groups) of the group. "Expanded" expands contained groups recursively and returns 
        /// their contained users. "None" is the fastest option as it fetches no membership 
        /// information. When omitted, defaults to Direct.
        /// </summary>
        [Parameter(ParameterSetName = "Get Identity")]
        public TfsQueryMembership QueryMembership { get; set; } = TfsQueryMembership.Direct;

        /// <summary>
        /// Returns an identity representing the user currently logged in to
        /// the Azure DevOps / TFS instance
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current user")]
        public SwitchParameter Current { get; set; }
    }
}