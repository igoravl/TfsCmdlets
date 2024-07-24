using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    /// <summary>
    /// Enables (activates) a work item tag.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(WebApiTagDefinition))]
    partial class EnableWorkItemTag
    {
        /// <summary>
        /// Specifies the tag to enable. Wildcards are supported.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [SupportsWildcards]
        [Alias("Name")]
        public object Tag { get; set; }

        [Parameter]
        internal bool Enabled => true;
    }

    [CmdletController(typeof(WebApiTagDefinition), CustomBaseClass = typeof(ToggleWorkItemTagController))]
    partial class EnableWorkItemTagController { 
        // See ToggleWorkItemTagController
    }
}