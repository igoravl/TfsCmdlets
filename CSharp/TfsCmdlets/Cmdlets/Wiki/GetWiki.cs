using System.Management.Automation;
using Microsoft.TeamFoundation.Wiki.WebApi;

namespace TfsCmdlets.Cmdlets.Wiki
{
    /// <summary>
    /// Gets information from one or more Wiki repositories in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(WikiV2))]
    partial class GetWiki
    {
        /// <summary>
        /// Specifies the name or ID of a Wiki repository. Wildcards are supported. 
        /// When omitted, all Wiki repositories in the supplied team project are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get all wikis")]
        [SupportsWildcards()]
        [Alias("Name", "Id")]
        public object Wiki { get; set; } = "*";

        /// <summary>
        /// Returns only provisioned ("project") Wikis. When omitted, returns all Wikis 
        /// (both Project wikis and Code wikis).
        /// </summary>
        [Parameter(ParameterSetName = "Get Project Wiki", Mandatory = true)]
        public SwitchParameter ProjectWiki { get; set; }
    }
}