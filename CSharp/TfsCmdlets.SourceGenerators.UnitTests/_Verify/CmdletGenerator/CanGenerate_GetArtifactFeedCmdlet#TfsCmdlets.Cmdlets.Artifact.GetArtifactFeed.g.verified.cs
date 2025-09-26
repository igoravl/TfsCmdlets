//HintName: TfsCmdlets.Cmdlets.Artifact.GetArtifactFeed.g.cs
namespace TfsCmdlets.Cmdlets.Artifact
{
    [Cmdlet("Get", "TfsArtifactFeed")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Feed.WebApi.Feed))]
    public partial class GetArtifactFeed: CmdletBase
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