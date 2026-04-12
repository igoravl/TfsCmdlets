using System.Management.Automation;
using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Renames an area path.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(Models.ClassificationNode))]
    partial class RenameArea
    {
        /// <summary>
        /// HELP_PARAM_AREA
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Area")]
        public object Node { get; set; }
    }

    [CmdletController(typeof(ClassificationNode), Client = typeof(IWorkItemTrackingHttpClient))]
    partial class RenameAreaController
    {
        [Import]
        private INodeUtil NodeUtil { get; set; }

        protected override IEnumerable Run()
            => ClassificationNodeHelper.RenameNode(
                Items, NewName, StructureGroup,
                Project, NodeUtil, Client, PowerShell);
    }
}