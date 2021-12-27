using Microsoft.VisualStudio.Services.Graph.Client;
using TfsCmdlets.Cmdlets.Identity.Group;

namespace TfsCmdlets.Controllers.Identity.Group
{
    [CmdletController(typeof(GraphGroup))]
    partial class RemoveGroupController
    {
        [Import]
        private IDescriptorService DescriptorService { get; set; }

        public override IEnumerable<GraphGroup> Invoke()
        {
            var groups = Data.GetItems<GraphGroup>();
            var scope = Parameters.Get<GroupScope>(nameof(RemoveGroup.Scope));

            var client = Data.GetClient<GraphHttpClient>();

            foreach (var group in groups)
            {
                if (!PowerShell.ShouldProcess($"Group '{group.PrincipalName}'", "Remove group")) continue;

                client.DeleteGroupAsync(group.Descriptor)
                    .Wait($"Error removing group '{group.PrincipalName}'");
            }

            return null;
        }
    }
}
