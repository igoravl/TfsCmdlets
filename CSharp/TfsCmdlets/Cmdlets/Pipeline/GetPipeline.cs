using System.Management.Automation;
using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Cmdlets.Pipeline
{
    /// <summary>
    /// Gets one or more pipelines in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(BuildDefinitionReference))]
    partial class GetPipeline
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

    [CmdletController(typeof(BuildDefinitionReference), Client=typeof(IBuildHttpClient))]
    partial class GetPipelineController
    {
        [Import]
        private INodeUtil NodeUtil { get; set; }

        protected override IEnumerable Run()
        {
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
                            var defs = Client.GetDefinitionsAsync(project: (string) Project.Name, yamlFilename: null)
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

            foreach (var def in Client.GetFullDefinitionsAsync(project: Project.Name, definitionIds: ids, queryOrder: QueryOrder, yamlFilename: null)
                .GetResult($"Error getting pipeline definitions"))
            {
                yield return def;
            }
        }
    }
}