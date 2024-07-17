using System.Management.Automation;
using Microsoft.VisualStudio.Services.Identity;
using Microsoft.VisualStudio.Services.Identity.Client;
using TfsCmdlets.Cmdlets.Identity.Group;

namespace TfsCmdlets.Cmdlets.Identity.Group
{
    /// <summary>
    /// Adds group members to an Azure DevOps group.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true)]
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

    [CmdletController]
    partial class AddGroupMemberController
    {
        protected override IEnumerable Run()
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
