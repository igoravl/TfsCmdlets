//HintName: TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement.RemoveReleaseDefinitionFolder.g.cs
namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement
{
    [Cmdlet("Remove", "TfsReleaseDefinitionFolder", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder))]
    public partial class RemoveReleaseDefinitionFolder: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
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