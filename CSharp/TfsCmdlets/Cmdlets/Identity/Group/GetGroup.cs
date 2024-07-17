using Microsoft.VisualStudio.Services.Graph.Client;
using TfsCmdlets.Cmdlets.Identity.Group;

namespace TfsCmdlets.Cmdlets.Identity.Group
{
    /// <summary>
    /// Gets one or more Azure DevOps groups.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(GraphGroup))]
    partial class GetGroup
    {
        /// <summary>
        /// Specifies the group to be retrieved. Supported values are: 
        /// Group name or ID. Wildcards are supported.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards]
        public object Group { get; set; } = "*";

        /// <summary>
        /// Specifies the scope under which to search for the group. 
        /// When omitted, defaults to the Collection scope.
        /// </summary>
        [Parameter]
        public GroupScope Scope { get; set; } = GroupScope.Collection;

        /// <summary>
        /// Searches recursively for groups in the scopes under the specified scope.
        /// </summary>
        [Parameter]
        public SwitchParameter Recurse { get; set; }
    }

    [CmdletController(typeof(GraphGroup))]
    partial class GetGroupController
    {
        [Import]
        private IDescriptorService DescriptorService { get; set; }

        protected override IEnumerable Run()
        {
            var group = Parameters.Get<object>(nameof(GetGroup.Group));
            var scope = Parameters.Get<GroupScope>(nameof(GetGroup.Scope));
            var recurse = Parameters.Get<bool>(nameof(GetGroup.Recurse));
            var client = Data.GetClient<GraphHttpClient>();

            PagedGraphGroups result = null;
            string groupName;

            switch (group)
            {
                case string s:
                    {
                        groupName = s;
                        break;
                    }
                case GraphGroup g:
                    {
                        yield return g;
                        yield break;
                    }
                default:
                    {
                        Logger.LogError($"Invalid group type: {group.GetType().Name}");
                        yield break;
                    }
            }

            switch (scope)
            {
                case GroupScope.Server:
                    {
                        do
                        {
                            result = client.ListGroupsAsync(continuationToken: result?.ContinuationToken.FirstOrDefault())
                                     .GetResult<PagedGraphGroups>("Error getting groups in collection");

                            foreach (var g in result.GraphGroups
                                .Where(g =>
                                    (g.PrincipalName.IsLike(groupName) || g.DisplayName.IsLike(groupName)) &&
                                    (recurse || g.PrincipalName.StartsWith("[TEAM FOUNDATION]"))))
                            {
                                yield return g;
                            }
                        } while (result?.ContinuationToken != null);
                        
                        break;
                    }
                case GroupScope.Collection:
                    {
                        var tpc = Data.GetCollection();

                        do
                        {
                            result = client.ListGroupsAsync(continuationToken: result?.ContinuationToken.FirstOrDefault())
                                     .GetResult<PagedGraphGroups>("Error getting groups in collection");

                            foreach (var g in result.GraphGroups
                                .Where(g =>
                                    (g.PrincipalName.IsLike(groupName) || g.DisplayName.IsLike(groupName)) &&
                                    (recurse || g.Domain.EndsWith(tpc.ServerId.ToString()))))
                            {
                                yield return g;
                            }
                        } while (result?.ContinuationToken != null);

                        break;
                    }
                case GroupScope.Project:
                    {
                        var tp = Data.GetProject();
                        var descriptor = DescriptorService.GetDescriptor(tp.Id);

                        do
                        {
                            result = client.ListGroupsAsync(scopeDescriptor: descriptor.Value, continuationToken: result?.ContinuationToken.FirstOrDefault())
                                     .GetResult<PagedGraphGroups>($"Error getting groups in team project {tp.Name}");

                            foreach (var g in result.GraphGroups.Where(g => g.PrincipalName.IsLike(groupName) || g.DisplayName.IsLike(groupName)))
                            {
                                yield return g;
                            }
                        } while (result?.ContinuationToken != null);

                        break;
                    }
            }
        }
    }
}