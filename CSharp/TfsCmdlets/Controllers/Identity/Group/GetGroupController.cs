using Microsoft.VisualStudio.Services.Graph.Client;
using TfsCmdlets.Cmdlets.Identity.Group;

namespace TfsCmdlets.Controllers.Identity.Group
{
    [CmdletController(typeof(GraphGroup))]
    partial class GetGroupController
    {
        [Import]
        private IDescriptorService DescriptorService { get; set; }

        public override IEnumerable<GraphGroup> Invoke()
        {
            var group = Parameters.Get<string>(nameof(GetGroup.Group));
            var scope = Parameters.Get<GroupScope>(nameof(GetGroup.Scope));
            var recurse = Parameters.Get<bool>(nameof(GetGroup.Recurse));
            var client = Data.GetClient<GraphHttpClient>();

            PagedGraphGroups result = null;

            switch (scope)
            {
                case GroupScope.Server:
                    {
                        throw new NotImplementedException("Server scope is currently not supported");
                    }
                case GroupScope.Collection:
                    {
                        var tpc = Data.GetCollection();
                        var id = Guid.NewGuid();
                        object descriptor = null;//DescriptorService.GetDescriptor(id);

                        do
                        {
                            result = client.ListGroupsAsync(scopeDescriptor: (string)descriptor, continuationToken: result?.ContinuationToken.FirstOrDefault())
                                     .GetResult<PagedGraphGroups>("Error getting groups in collection");

                            foreach (var g in result.GraphGroups
                                .Where(g =>
                                    (g.PrincipalName.IsLike(group) || g.DisplayName.IsLike(group)) &&
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

                            foreach (var g in result.GraphGroups.Where(g => g.PrincipalName.IsLike(group) || g.DisplayName.IsLike(group)))
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