using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    /// <summary>
    /// Deletes a team project collection.
    /// </summary>
    [TfsCmdlet(CmdletScope.Server, SupportsShouldProcess = true, DesktopOnly = true)]
    partial class RemoveTeamProjectCollection
    {
        /// <summary>
        /// Specifies the collection to be removed. Wildcards are supported.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [SupportsWildcards]
        public object Collection { get; set; }

        /// <summary>
        /// Sets the timeout for the operation to complete. When omitted, will wait indefinitely until the operation completes.
        /// </summary>
        [Parameter]
        public TimeSpan Timeout { get; set; } = TimeSpan.MaxValue;
    }
}