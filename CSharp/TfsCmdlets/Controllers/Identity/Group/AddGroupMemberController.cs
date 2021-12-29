using Microsoft.VisualStudio.Services.Identity;
using Microsoft.VisualStudio.Services.Identity.Client;
using TfsCmdlets.Cmdlets.Identity.Group;

namespace TfsCmdlets.Controllers.Identity.Group
{
    [CmdletController]
    partial class AddGroupMemberController
    {
        public override object InvokeCommand()
        {
            var member = Parameters.Get<object>(nameof(AddGroupMember.Member));
            var group = Parameters.Get<object>(nameof(AddGroupMember.Group));

            var identities = Data.GetItems<Models.Identity>(new
            {
                Identity = member
            }).ToList();

            var g = Data.GetItem<Models.Identity>(new
            {
                Identity = group
            });

            var client = Data.GetClient<IdentityHttpClient>();

            foreach (var m in identities)
            {
                if (!PowerShell.ShouldProcess($"Group '{g.DisplayName}'",
                    $"Add member '{m.DisplayName} ({m.UniqueName})'")) continue;

                client.AddMemberToGroupAsync(
                    (IdentityDescriptor)g.Descriptor,
                    (IdentityDescriptor)m.Descriptor)
                    .GetResult($"Error adding member '{m.DisplayName}' to group '{g.DisplayName}'");
            }

            return identities;
        }
    }
}
