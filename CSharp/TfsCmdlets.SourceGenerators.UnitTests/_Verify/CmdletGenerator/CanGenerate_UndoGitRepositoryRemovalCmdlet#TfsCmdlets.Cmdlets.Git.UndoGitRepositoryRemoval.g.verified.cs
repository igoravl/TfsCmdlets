//HintName: TfsCmdlets.Cmdlets.Git.UndoGitRepositoryRemoval.g.cs
namespace TfsCmdlets.Cmdlets.Git
{
    [Cmdlet("Undo", "TfsGitRepositoryRemoval", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository))]
    public partial class UndoGitRepositoryRemoval: CmdletBase
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