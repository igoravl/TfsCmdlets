//HintName: TfsCmdlets.Cmdlets.Artifact.GetArtifactFeedView.g.cs
namespace TfsCmdlets.Cmdlets.Artifact
{
    [Cmdlet("Get", "TfsArtifactFeedView")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Feed.WebApi.FeedView))]
    public partial class GetArtifactFeedView: CmdletBase
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