//HintName: TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement.GetReleaseDefinitionFolder.g.cs
namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement
{
    [Cmdlet("Get", "TfsReleaseDefinitionFolder")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder))]
    public partial class GetReleaseDefinitionFolder: CmdletBase
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