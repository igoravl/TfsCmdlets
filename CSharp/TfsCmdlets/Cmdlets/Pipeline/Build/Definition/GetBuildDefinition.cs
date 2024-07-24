using System.Management.Automation;
using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Cmdlets.Pipeline.Build.Definition
{
    /// <summary>
    /// Gets one or more build/pipeline definitions in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(BuildDefinitionReference))]
    partial class GetBuildDefinition
    {
        /// <summary>
        /// Specifies the pipeline path. Wildcards are supported. 
        /// When omitted, all pipelines definitions in the supplied team project are returned.
        /// </summary>
        [Parameter(Position = 0)]
        [Alias("Path")]
        [SupportsWildcards()]
        public object Definition { get; set; } = "\\**";

        /// <summary>
        /// Specifies the query order. When omitted, defaults to None.
        /// </summary>
        [Parameter]
        public DefinitionQueryOrder QueryOrder { get; set; }
    }

    [CmdletController(typeof(BuildDefinitionReference))]
    partial class GetBuildDefinitionController
    {
        [Import]
        private INodeUtil NodeUtil { get; set; }

        protected override IEnumerable Run()
        {
            var client = Data.GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>();
            var ids = new List<int>();

            foreach (var input in Definition)
            {
                var definition = input switch
                {
                    _ => input
                };

                switch (definition)
                {
                    case BuildDefinition bd:
                        {
                            yield return bd;
                            yield break;
                        }
                    case int id:
                        {
                            ids.Add(id);
                            break;
                        }
                    case string s:
                        {
                            var path = NodeUtil.NormalizeNodePath(s, includeLeadingSeparator: true);
                            var defs = client.GetDefinitionsAsync(project: Project.Name)
                                .GetResult($"Error getting pipeline definitions matching '{s}'");
                            ids.AddRange(defs.Where(bd => bd.GetFullPath().IsLike(path) || bd.Name.IsLike(s)).Select(bd => bd.Id));
                            break;
                        }
                    default:
                        {
                            Logger.LogError($"Invalid or non-existent pipeline definition '{definition}'");
                            break;
                        }
                }
            }

            if(ids.Count == 0) yield break;

            foreach (var def in client.GetFullDefinitionsAsync(project: Project.Name, definitionIds: ids, queryOrder: QueryOrder)
                .GetResult($"Error getting pipeline definitions"))
            {
                yield return def;
            }
        }
    }
}