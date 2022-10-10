using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    /// <summary>
    /// Deletes a team project collection.
    /// </summary>
    [TfsCmdlet(CmdletScope.Server, SupportsShouldProcess = true, DesktopOnly = true)]
    partial class RemoveTeamProjectCollection
    {
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [SupportsWildcards]
        public object Collection { get; set; }

        [Parameter]
        public TimeSpan Timeout { get; set; } = TimeSpan.MaxValue;
    }
}