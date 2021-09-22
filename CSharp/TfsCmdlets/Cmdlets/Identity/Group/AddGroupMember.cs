using System.Management.Automation;
using Microsoft.VisualStudio.Services.Identity;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Identity.Group
{
    /// <summary>
    /// Adds group members to an Azure DevOps group.
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "TfsGroupMember", SupportsShouldProcess = true)]
    public class AddGroupMember : CmdletBase
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

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        // TODO

        ///// <summary>
        ///// Performs execution of the command
        ///// </summary>
        //protected override void DoProcessRecord()
        //{
        //    var member = GetItem<Models.Identity>(new {
        //        Identity = Member
        //    });

        //    var group = GetItem<Models.Identity>(new {
        //        Identity = Group
        //    });

        //    var client = Data.GetClient<Microsoft.VisualStudio.Services.Identity.Client.IdentityHttpClient>(parameters);

        //    this.Log($"Adding {member.IdentityType} '{member.DisplayName} ({member.UniqueName})' to group '{group.DisplayName}'");

        //    if (!PowerShell.ShouldProcess($"Group '{group.DisplayName}'", 
        //        $"Add member '{member.DisplayName} ({member.UniqueName})'")) return;

        //    client.AddMemberToGroupAsync(
        //        (IdentityDescriptor)group.Descriptor, 
        //        (IdentityDescriptor)member.Descriptor)
        //        .GetResult($"Error adding member '{member.DisplayName}' to group '{group.DisplayName}'");
        //}
    }
}
