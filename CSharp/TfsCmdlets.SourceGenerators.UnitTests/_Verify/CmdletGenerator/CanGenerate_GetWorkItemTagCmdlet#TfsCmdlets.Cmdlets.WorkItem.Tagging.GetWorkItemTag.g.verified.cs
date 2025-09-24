//HintName: TfsCmdlets.Cmdlets.WorkItem.Tagging.GetWorkItemTag.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    [Cmdlet("Get", "TfsWorkItemTag")]
    [OutputType(typeof(Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition))]
    public partial class GetWorkItemTag: CmdletBase
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