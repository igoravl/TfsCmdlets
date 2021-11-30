using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Gets one or more Iterations from a given Team Project.
    /// </summary>
    /// <example>
    ///   <code>Get-TfsIteration</code>
    ///   <para>Returns all iterations in the currently connected Team Project (as defined by a previous call to Connect-TfsTeamProject)</para>
    /// </example>
    /// <example>
    ///   <code>Get-TfsIteration '\**\Support' -Project Tailspin</code>
    ///   <para>Performs a recursive search and returns all iterations named 'Support' that may exist in a team project called Tailspin</para>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "TfsIteration")]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class GetIteration : CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_ITERATION
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Iteration")]
        public object Node { get; set; } = @"\**";

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Project { get; set; }

        /// <summary>
        /// Indicates the type of structure (area or iteration)
        /// </summary>
        [Parameter()]
        internal TreeStructureGroup StructureGroup => TreeStructureGroup.Iterations;

        protected override string CommandName =>
            nameof(TfsCmdlets.Controllers.WorkItem.AreasIterations.GetClassificationNode);
    }
}