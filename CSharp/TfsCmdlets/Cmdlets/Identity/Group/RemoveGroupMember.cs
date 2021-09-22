using Microsoft.VisualStudio.Services.Identity.Client;
using System.Management.Automation;
using TfsCmdlets.Extensions;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.Identity.Group
{
    /// <summary>
    /// Removes a member from an Azure DevOps group.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsGroupMember", SupportsShouldProcess = true)]
    public class RemoveGroupMember : CmdletBase
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
        //    var member = GetItem<Models.Identity>(new
        //    {
        //        Identity = Member
        //    });

        //    ErrorUtil.ThrowIfNotFound(member, nameof(member), Member);

        //    var group = GetItem<Models.Identity>(new
        //    {
        //        Identity = Group
        //    });

        //    ErrorUtil.ThrowIfNotFound(group, nameof(group), Group);

        //    var client = Data.GetClient<IdentityHttpClient>(parameters);

        //    if (!PowerShell.ShouldProcess(group.DisplayName,
        //        $"Remove member '{member.DisplayName} ({member.UniqueName})'"))
        //    {
        //        return;
        //    }

        //    this.Log($"Removing '{member.DisplayName} ({member.UniqueName}))' from group '{group.DisplayName}'");

        //    client.RemoveMemberFromGroupAsync(group.Descriptor, member.Descriptor)
        //        .GetResult($"Error removing '{member.DisplayName} ({member.UniqueName}))' from group '{group.DisplayName}'");
        //}
    }
}