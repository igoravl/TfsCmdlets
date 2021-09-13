using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Build.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using TfsCmdlets.Util;
using WebApiFolder = Microsoft.TeamFoundation.Build.WebApi.Folder;

namespace TfsCmdlets.Cmdlets.Pipeline.Build
{
    /// <summary>
    /// Gets one or more build/pipeline definitions in a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsBuildDefinition")]
    [OutputType(typeof(BuildDefinitionReference))]
    public class GetBuildDefinition : GetCmdletBase<BuildDefinition>
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
        [Parameter()]
        public DefinitionQueryOrder QueryOrder { get; set; } = DefinitionQueryOrder.None;

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        /// <value></value>
        [Parameter(ValueFromPipeline = true)]
        public object Project { get; set; }
    }

    [Exports(typeof(BuildDefinition))]
    internal partial class BuildDefinitionDataService : BaseDataService<BuildDefinition>
    {
        protected override IEnumerable<BuildDefinition> DoGetItems()
        {
            var definition = GetParameter<object>(nameof(GetBuildDefinition.Definition));
            var queryOrder = GetParameter<DefinitionQueryOrder>(nameof(GetBuildDefinition.QueryOrder), DefinitionQueryOrder.None);
            
            var client = GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>();
            var (_, tp) = GetCollectionAndProject();


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