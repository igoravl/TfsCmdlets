using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Controllers.Pipeline.Build.Definition
{
    [CmdletController(typeof(BuildDefinitionReference))]
    partial class GetBuildDefinitionController
    {
        [Import]
        private INodeUtil NodeUtil { get; set; }

        protected override IEnumerable Run()
        {
            var client = Data.GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>();

            foreach(var input in Definition)
            {
                var definition = input switch {
                    _ => input
                };

                switch(definition)
                {
                    case BuildDefinition bd:
                        {
                            yield return bd;
                            yield break;
                        }
                    case int id:
                        {
                            yield return client.GetDefinitionAsync(Project.Name, id)
                                .GetResult($"Error getting pipeline definition '{id}'");
                            yield break;
                        }
                    case string s:
                        {
                            var path = NodeUtil.NormalizeNodePath(s, includeLeadingSeparator: true);

                            var defs = client.GetDefinitionsAsync(Project.Name, queryOrder: QueryOrder)
                                .GetResult($"Error getting pipeline definitions matching '{s}'");

                            foreach (var i in defs.Where(bd => bd.GetFullPath().IsLike(path)))
                            {
                                yield return client.GetDefinitionAsync(Project.Name, i.Id)
                                    .GetResult($"Error getting pipeline definition '{i.Id}'");
                            }
                            yield break;
                        }
                    default:
                        {
                            Logger.LogError($"Invalid or non-existent pipeline definition '{definition}'");
                            break;
                        }
                }
            }
        }
    }
}