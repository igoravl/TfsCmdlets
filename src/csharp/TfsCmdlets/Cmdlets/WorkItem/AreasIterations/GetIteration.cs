using System.Management.Automation;

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
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(Models.ClassificationNode))]
    partial class GetIteration 
    {
        /// <summary>
        /// HELP_PARAM_ITERATION
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Iteration")]
        public object Node { get; set; } = @"\**";
    }
}