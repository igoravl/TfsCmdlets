using Microsoft.TeamFoundation.Build.WebApi;
using TfsCmdlets.Cmdlets.Pipeline.Build;

namespace TfsCmdlets.Controllers.Pipeline.Build
{
    [CmdletController(typeof(BuildDefinitionReference))]
    partial class GetBuildDefinitionController
    {
        [Import]
        private INodeUtil NodeUtil { get; set; }

        protected override IEnumerable Run()
        {
            var definition = Parameters.Get<object>(nameof(GetBuildDefinition.Definition));
            var queryOrder = Parameters.Get<DefinitionQueryOrder>(nameof(GetBuildDefinition.QueryOrder), DefinitionQueryOrder.None);

            var client = Data.GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>();
            var tp = Data.GetProject();


            while (true) switch (definition)
                {
                    case BuildDefinition bd:
                        {
                            yield return bd;
                            yield break;
                        }
                    case int id:
                        {
                            yield return client.GetDefinitionAsync(tp.Name, id)
                                .GetResult($"Error getting pipeline definition #{id}");
                            yield break;
                        }
                    case string s:
                        {
                            var path = NodeUtil.NormalizeNodePath(s, includeLeadingSeparator: true);

                            var defs = client.GetDefinitionsAsync(tp.Name, queryOrder: queryOrder)
                                .GetResult($"Error getting pipeline definitions matching {s}");

                            foreach (var i in defs.Where(bd => bd.GetFullPath().IsLike(path)))
                            {
                                yield return client.GetDefinitionAsync(tp.Name, i.Id)
                                    .GetResult($"Error getting pipeline definition {i.Id}");
                            }

                            yield break;
                        }
                    default:
                        {
                            throw new ArgumentException($"Invalid or non-existent pipeline definition '{definition}'");
                        }
                }
        }
    }
}