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
    [Cmdlet(VerbsCommon.Get, "TfsArea")]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class GetArea : CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_AREA
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Area")]
        public object Node { get; set; } = @"\**";

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
        /// Indicates the type of structure (area or iteration)
        /// </summary>
        [Parameter()]
        internal TreeStructureGroup StructureGroup => TreeStructureGroup.Areas;

        /// <summary>
        /// Returns the type name for the underlying ICommand implementing the logic of this cmdlet
        /// </summary>
        protected override string CommandName =>
            nameof(TfsCmdlets.Commands.WorkItem.AreasIterations.GetClassificationNode);
    }
}