using Microsoft.VisualStudio.Services.Graph.Client;
using TfsCmdlets.Cmdlets.Identity.Group;

namespace TfsCmdlets.Controllers.Identity.Group
{
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
                        throw new NotImplementedException("Server scope is currently not supported");
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