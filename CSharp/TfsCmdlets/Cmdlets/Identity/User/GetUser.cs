using System.Management.Automation;
using Microsoft.VisualStudio.Services.Licensing;

namespace TfsCmdlets.Cmdlets.Identity.User
{
    /// <summary>
    /// Gets information about one or more Azure DevOps users.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(AccountEntitlement), NoAutoPipeline = true, DefaultParameterSetName = "Get by ID or Name")]
    partial class GetUser
    {
        /// <summary>
        /// Specifies the user or group to be retrieved. Supported values are: 
        /// User/group name, email, or ID
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by ID or Name", ValueFromPipeline = true)]
        [Alias("UserId")]
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