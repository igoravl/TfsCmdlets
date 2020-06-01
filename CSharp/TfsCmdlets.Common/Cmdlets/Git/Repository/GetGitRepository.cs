using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Git.Repository
{
    /// <summary>
    /// Gets information from one or more Git repositories in a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "GitRepository")]
    [OutputType(typeof(GitRepository))]
    public class GetGitRepository : BaseCmdlet
    {
        /// <summary>
        /// Specifies the name or ID (a GUID) of a Git repository. Wildcards are supported. 
        /// When omitted, all Git repositories in the supplied team project are returned.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Repository { get; set; } = "*";

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            WriteObject(this.GetMany<GitRepository>(), true);
        }
    }
}