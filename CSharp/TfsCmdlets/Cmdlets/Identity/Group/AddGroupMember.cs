using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Identity.Group
{
    /// <summary>
    /// Adds group members to an Azure DevOps group.
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "TfsGroupMember", SupportsShouldProcess = true)]
    [TfsCmdlet(CmdletScope.Collection)]
    partial class AddGroupMember 
    {
        /// <summary>
        /// Specifies the member (user or group) to add to the given group.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public object Member { get; set; }

        /// <summary>
        /// Specifies the group to which the member is added.
        /// </summary>
        [Parameter(Position = 1, Mandatory = true)]
        public object Group { get; set; }
    }
}
