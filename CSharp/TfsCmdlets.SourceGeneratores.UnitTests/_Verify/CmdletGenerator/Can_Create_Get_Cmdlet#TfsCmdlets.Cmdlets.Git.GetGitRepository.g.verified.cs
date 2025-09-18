//HintName: TfsCmdlets.Cmdlets.Git.GetGitRepository.g.cs
namespace TfsCmdlets.Cmdlets.Git
{
    [Cmdlet("Get", "TfsGitRepository", DefaultParameterSetName = "Get by ID or Name")]
    [OutputType(typeof(Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository))]
    public partial class GetGitRepository: CmdletBase
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
