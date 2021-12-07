using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Identity.Group
{
    /// <summary>
    /// Removes a member from an Azure DevOps group.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsGroupMember", SupportsShouldProcess = true)]
    [TfsCmdlet(CmdletScope.Collection)]
    partial class RemoveGroupMember 
    {
        /// <summary>
        /// Specifies the member (user or group) to remove from the given group.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public object Member { get; set; }

        /// <summary>
        /// Specifies the group from which the member is removed.
        /// </summary>
        [Parameter(Position = 1, Mandatory = true)]
        public object Group { get; set; }
    }
}