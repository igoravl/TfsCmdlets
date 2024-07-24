using System.Management.Automation;
using TfsCmdlets.Cmdlets.Identity.Group;

namespace TfsCmdlets.Cmdlets.Identity.Group
{
    /// <summary>
    /// Removes a member from an Azure DevOps group.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true)]
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

    [CmdletController]
    partial class RemoveGroupMemberController
    {
        protected override IEnumerable Run()
        {
            var member = Parameters.Get<object>(nameof(AddGroupMember.Member));
            var group = Parameters.Get<object>(nameof(AddGroupMember.Group));

            var m = Data.GetItem<Models.Identity>(new
            {
                Identity = member
            });

            var g = Data.GetItem<Models.Identity>(new
            {
                Identity = group
            });

            var client = Data.GetClient<Microsoft.VisualStudio.Services.Identity.Client.IdentityHttpClient>();

            Logger.Log($"Adding {m.IdentityType} '{m.DisplayName} ({m.UniqueName})' to group '{g.DisplayName}'");

            if (!PowerShell.ShouldProcess($"[Group: {g.DisplayName}]/[Member: '{m.DisplayName} ({m.UniqueName})']", "Remove member")) return null;

            Logger.Log($"Removing '{m.DisplayName} ({m.UniqueName})' from group '{g.DisplayName}'");

            client.RemoveMemberFromGroupAsync(g.Descriptor, m.Descriptor)
                .GetResult($"Error removing '{m.DisplayName} ({m.UniqueName}))' from group '{g.DisplayName}'");

            return null;
        }
    }
}