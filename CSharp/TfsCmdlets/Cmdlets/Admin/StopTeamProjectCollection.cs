using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Admin
{
    /// <summary>
    /// Stops a team project collection and make it offline.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Stop, "TfsTeamProjectCollection", SupportsShouldProcess = true)]
    public class StopTeamProjectCollection : CollectionScopedCmdlet
    {
    }
}
