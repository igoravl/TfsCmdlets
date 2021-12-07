using System.Management.Automation;
using Microsoft.VisualStudio.Services.Identity;
using TfsCmdlets.Cmdlets.Identity.Group;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Controllers.Identity.Group
{
    [CmdletController]
    partial class AddGroupMemberController 
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
                $"Add member '{m.DisplayName} ({m.UniqueName})'")) return null;

            client.AddMemberToGroupAsync(
                (IdentityDescriptor)g.Descriptor,
                (IdentityDescriptor)m.Descriptor)
                .GetResult($"Error adding member '{m.DisplayName}' to group '{g.DisplayName}'");

            return null;
        }
    }
}
