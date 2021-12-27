using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Determines whether the specified Work Area exist.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(bool), CustomControllerName = "TestClassificationNode")]
    partial class TestArea 
    {
        /// <summary>
        /// HELP_PARAM_AREA
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Area", "Path")]
        [SupportsWildcards()]
        public string Node { get; set; }
    }
}