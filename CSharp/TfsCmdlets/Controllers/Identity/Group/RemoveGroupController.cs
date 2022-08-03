using Microsoft.VisualStudio.Services.Graph.Client;
using TfsCmdlets.Cmdlets.Identity.Group;

namespace TfsCmdlets.Controllers.Identity.Group
{
    [CmdletController(typeof(GraphGroup))]
    partial class RemoveGroupController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<GraphHttpClient>();

            foreach (var group in Items)
            {
                if (!PowerShell.ShouldProcess(group.PrincipalName, "Remove group")) continue;

                client.DeleteGroupAsync(group.Descriptor)
                    .Wait($"Error removing group '{group.PrincipalName}'");
            }

            return null;
        }
    }
}
