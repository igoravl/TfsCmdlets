//HintName: TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement.GetReleaseDefinition.g.cs
namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement
{
    [Cmdlet("Get", "TfsReleaseDefinition")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.ReleaseDefinition))]
    public partial class GetReleaseDefinition: CmdletBase
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