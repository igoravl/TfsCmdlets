//HintName: TfsCmdlets.Cmdlets.Identity.Group.GetGroup.g.cs
namespace TfsCmdlets.Cmdlets.Identity.Group
{
    [Cmdlet("Get", "TfsGroup")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Graph.Client.GraphGroup))]
    public partial class GetGroup: CmdletBase
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