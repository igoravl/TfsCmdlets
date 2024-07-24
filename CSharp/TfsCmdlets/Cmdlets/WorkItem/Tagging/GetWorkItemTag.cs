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

    [CmdletController(typeof(WebApiTagDefinition))]
    partial class GetWorkItemTagController
    {
        protected override IEnumerable Run()
        {
            var tag = Parameters.Get<object>(nameof(GetWorkItemTag.Tag));
            var includeInactive = Parameters.Get<bool>(nameof(GetWorkItemTag.IncludeInactive));

            var tp = Data.GetProject();
            var client = Data.GetClient<Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient>();

            switch (tag)
            {
                case WebApiTagDefinition t:
                    {
                        return new[] { t };
                    }
                case string s:
                    {
                        return client.GetTagsAsync(tp.Id, includeInactive)
                            .GetResult($"Error getting work item tag(s) '{s}'")
                            .Where(t => t.Name.IsLike(s));
                    }
                case IEnumerable<string> tags:
                    {
                        return client.GetTagsAsync(tp.Id, includeInactive)
                            .GetResult($"Error getting work item tag(s) '{string.Join(", ", tags)}'")
                            .Where(t => tags.Any(tag => t.Name.IsLike(tag)));
                    }
            }

            Logger.LogError(new ArgumentException($"Invalid or non-existent tag '{tag}'"));

            return null;
        }
    }
}