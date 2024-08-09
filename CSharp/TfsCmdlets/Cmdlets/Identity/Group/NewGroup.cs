using Microsoft.VisualStudio.Services.Graph.Client;
using TfsCmdlets.Cmdlets.Identity.Group;

namespace TfsCmdlets.Cmdlets.Identity.Group
{
    /// <summary>
    /// Creates a new Azure DevOps group.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(GraphGroup), SupportsShouldProcess = true)]
    partial class NewGroup
    {
        /// <summary>
        /// Specifies the name of the new group.
        /// </summary>
        [Parameter(Position = 0)]
        public string Group { get; set; }

        /// <summary>
        /// Specifies a description for the new group.
        /// </summary>
        [Parameter()]
        public string Description { get; set; }

        /// <summary>
        /// Specifies the scope under which to create the group. 
        /// When omitted, defaults to the Collection scope.
        /// </summary>
        [Parameter]
        public GroupScope Scope { get; set; } = GroupScope.Collection;
    }

    [CmdletController(typeof(GraphGroup), Client=typeof(IGraphHttpClient))]
    partial class NewGroupController
    {
        [Import]
        private IDescriptorService DescriptorService { get; set; }

        protected override IEnumerable Run()
        {
            var group = Parameters.Get<string>(nameof(NewGroup.Group));
            var description = Parameters.Get<string>(nameof(NewGroup.Description));
            var scope = Parameters.Get<GroupScope>(nameof(NewGroup.Scope));

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

                        yield return Client.CreateGroupAsync(new GraphGroupVstsCreationContext() {
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

                        yield return Client.CreateGroupAsync(new GraphGroupVstsCreationContext() {
                            DisplayName = group,
                            Description = description
                        }, scopeDescriptor: descriptor.Value).GetResult($"Error creating group '{group}' in project '{tp.Name}'");

                        break;
                    }
            }
        }
    }
}