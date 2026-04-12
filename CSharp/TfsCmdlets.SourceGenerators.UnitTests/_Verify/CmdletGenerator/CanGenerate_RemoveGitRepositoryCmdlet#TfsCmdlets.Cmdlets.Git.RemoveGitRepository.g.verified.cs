//HintName: TfsCmdlets.Cmdlets.Git.RemoveGitRepository.g.cs
namespace TfsCmdlets.Cmdlets.Git
{
    [Cmdlet("Remove", "TfsGitRepository", SupportsShouldProcess = true)]
    public partial class RemoveGitRepository: CmdletBase
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