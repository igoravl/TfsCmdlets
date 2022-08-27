using Microsoft.VisualStudio.Services.Graph.Client;
using TfsCmdlets.Cmdlets.Identity.Group;

namespace TfsCmdlets.Controllers.Identity.Group
{
    [CmdletController(typeof(GraphGroup))]
    partial class NewGroupController
    {
        [Import]
        private IDescriptorService DescriptorService { get; set; }

        protected override IEnumerable Run()
        {
            var group = Parameters.Get<string>(nameof(NewGroup.Group));
            var description = Parameters.Get<string>(nameof(NewGroup.Description));
            var scope = Parameters.Get<GroupScope>(nameof(NewGroup.Scope));
            var client = Data.GetClient<GraphHttpClient>();

            switch (scope)
            {
                case GroupScope.Server:
                    {
                        if(Collection.IsHosted)
                        {
                            throw new NotSupportedException("Server scope is not supported in Azure DevOps Services");
                        }

                        throw new NotImplementedException("Server scope is currently not supported");
                    }
                case GroupScope.Collection:
                    {
                        if(!PowerShell.ShouldProcess(Data.GetCollection(), $"Create group '{group}'")) yield break;

                        yield return client.CreateGroupAsync(new GraphGroupVstsCreationContext() {
                            DisplayName = group,
                            Description = description
                        }).GetResult($"Error creating group '{group}' in collection");

                        break;
                    }
                case GroupScope.Project:
                    {
                        var tp = Data.GetProject();
                        var descriptor = DescriptorService.GetDescriptor(tp.Id);

                        if(!PowerShell.ShouldProcess(tp, $"Create group {group}")) yield break;

                        yield return client.CreateGroupAsync(new GraphGroupVstsCreationContext() {
                            DisplayName = group,
                            Description = description
                        }, scopeDescriptor: descriptor.Value).GetResult($"Error creating group '{group}' in project '{tp.Name}'");

                        break;
                    }
            }
        }
    }
}