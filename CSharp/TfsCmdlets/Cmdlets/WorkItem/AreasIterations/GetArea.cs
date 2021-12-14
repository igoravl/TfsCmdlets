using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Gets one or more Work Item Areas from a given Team Project.
    /// </summary>
    /// <example>
    ///   <code>Get-TfsArea</code>
    ///   <para>Returns all area paths in the currently connected Team Project (as defined by a previous call to Connect-TfsTeamProject)</para>
    /// </example>
    /// <example>
    ///   <code>Get-TfsArea '\**\Support' -Project Tailspin</code>
    ///   <para>Performs a recursive search and returns all area paths named 'Support' that may exist in a team project called Tailspin</para>
    /// </example>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(Models.ClassificationNode))]
    partial class GetArea 
    {
        /// <summary>
        /// HELP_PARAM_AREA
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Area")]
        public object Node { get; set; } = @"\**";

        [Parameter()]
        internal TreeStructureGroup StructureGroup => TreeStructureGroup.Areas;
    }
}