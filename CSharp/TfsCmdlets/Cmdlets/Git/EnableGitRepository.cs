using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.HttpClient;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Enables one or more Git repositories.
    /// </summary>
    /// <remarks>
    /// This cmdlets re-enables access to a repository. When a repository is 
    /// disabled it cannot be accessed (including clones, pulls, pushes, builds, 
    /// pull requests etc) but remains discoverable, with a warning message 
    /// stating it is disabled.
    /// </remarks>
    [Cmdlet(VerbsLifecycle.Enable, "TfsGitRepository", SupportsShouldProcess = true)]
    [OutputType(typeof(GitRepository))]
    public class EnableGitRepository : ProjectScopedCmdlet
    {
        /// <summary>
        /// Specifies the name or ID of a Git repository. Wildcards are supported. 
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Repository { get; set; }
    }
}