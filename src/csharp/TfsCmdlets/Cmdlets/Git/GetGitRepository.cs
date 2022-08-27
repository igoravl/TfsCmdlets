using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Gets information from one or more Git repositories in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(GitRepository), DefaultParameterSetName = "Get by ID or Name")]
    partial class GetGitRepository
    {
        /// <summary>
        /// Specifies the name or ID of a Git repository. Wildcards are supported. 
        /// When omitted, all Git repositories in the supplied team project are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by ID or Name")]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Repository { get; set; } = "*";

        /// <summary>
        /// Returns the default repository in the given team project.
        /// The default repository is the one that is created when a team project is created, and has the same name as the team project.
        /// </summary>
        [Parameter(ParameterSetName = "Get default", Mandatory = true)]
        public SwitchParameter Default { get; set; }

        /// <summary>
        /// Returns details about the repository's parent (forked) repository, if it has one.
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludeParent { get; set; }
    }
}