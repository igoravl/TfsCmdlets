//HintName: TfsCmdlets.Cmdlets.WorkItem.Query.ExportWorkItemQuery.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.Query
{
    [Cmdlet("Export", "TfsWorkItemQuery", SupportsShouldProcess = true, DefaultParameterSetName = "Export to output stream")]
    [OutputType(typeof(string))]
    public partial class ExportWorkItemQuery: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(ValueFromPipeline=true)]
        public object Project { get; set; }
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Alias("Organization")]
        [Parameter()]
        public object Collection { get; set; }
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter()]
        public object Server { get; set; }
    }
}