using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    /// <summary>
    /// Disconnects from the currently connected TFS team project collection or Azure DevOps organization.
    /// </summary>
    /// <remarks>
    /// The Disconnect-TfsTeamProjectCollection cmdlet removes the connection previously set by its 
    /// counterpart Connect-TfsTeamProjectCollection. Therefore, cmdlets relying on a "default collection" 
    /// as provided by "Get-TfsTeamProjectCollection -Current" will no longer work after a call to 
    /// this cmdlet, unless their -Collection argument is provided or a new call to 
    /// Connect-TfsTeam is made.
    /// </remarks>
    [TfsCmdlet(CmdletScope.None)]
    partial class DisconnectTeamProjectCollection 
    {
    }

    [CmdletController(typeof(Connection))]
    partial class DisconnectTeamProjectCollectionController 
    {
        [Import]
        private ICurrentConnections CurrentConnections { get; }

        protected override IEnumerable Run()
        {
            Logger.Log($"Disconnecting from TPC '{CurrentConnections.Collection}'");

            CurrentConnections.Set(CurrentConnections.Server);

            return null;
        }
    }
}