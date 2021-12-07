using System.Management.Automation;
using TfsCmdlets.Cmdlets.Identity.Group;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Controllers.Identity.Group
{
    [CmdletController]
    partial class RemoveGroupMemberController
    {
        public override object InvokeCommand()
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

            if (!PowerShell.ShouldProcess($"Group '{g.DisplayName}'",
                    $"Remove member '{m.DisplayName} ({m.UniqueName})'")) return null;

            Logger.Log($"Removing '{m.DisplayName} ({m.UniqueName}))' from group '{g.DisplayName}'");

            client.RemoveMemberFromGroupAsync(g.Descriptor, m.Descriptor)
                .GetResult($"Error removing '{m.DisplayName} ({m.UniqueName}))' from group '{g.DisplayName}'");

            return null;
        }
    }
}