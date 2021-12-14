using System.Management.Automation;
using WebApiIdentity = Microsoft.VisualStudio.Services.Identity.Identity;
using TfsQueryMembership = Microsoft.VisualStudio.Services.Identity.QueryMembership;
using Microsoft.VisualStudio.Services.Licensing;

namespace TfsCmdlets.Cmdlets.Identity
{
    /// <summary>
    /// Gets information about an Azure DevOps user.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(AccountEntitlement), DefaultParameterSetName = "Get by ID or Name")]
    partial class GetUser
    {
        /// <summary>
        /// Specifies the user or group to be retrieved. Supported values are: 
        /// User/group name, email, or ID
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by ID or Name")]
        [SupportsWildcards]
        public object User { get; set; } = "*";

        /// <summary>
        /// Returns an identity representing the user currently logged in to
        /// the Azure DevOps / TFS instance
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current user")]
        public SwitchParameter Current { get; set; }
    }
}