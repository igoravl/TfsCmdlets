using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.WorkItem.Tagging;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    /// <summary>
    /// Gets one or more work item tags.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(WebApiTagDefinition))]
    partial class GetWorkItemTag
    {
        /// <summary>
        /// Specifies one or more tags to returns. Wildcards are supported. 
        /// When omitted, returns all existing tags in the given project.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Tag { get; set; } = "*";

        /// <summary>
        /// Includes tags not associated to any work items.
        /// </summary>
        [Parameter]
        public SwitchParameter IncludeInactive { get; set; }
    }

    [CmdletController(typeof(WebApiTagDefinition), Client = typeof(ITaggingHttpClient))]
    partial class GetWorkItemTagController
    {
        protected override IEnumerable Run()
        {
            foreach (var input in Tag)
            {
                switch (input)
                {
                    case WebApiTagDefinition t:
                        {
                            yield return t;
                            break;
                        }
                    case string s when s.IsWildcard():
                        {
                            yield return Client.GetTagsAsync(Project.Id, IncludeInactive)
                                .GetResult($"Error getting work item tag(s) '{s}'")
                                .Where(t => t.Name.IsLike(s));
                            break;
                        }
                    case string s:
                        {
                            yield return Client.GetTagAsync(scopeId: Project.Id, name: s)
                                .GetResult($"Error getting work item tag(s) '{s}'");
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent tag '{input}'"));
                            break;
                        }
                }
            }
        }
    }
}