using TfsCmdlets.Models;

namespace TfsCmdlets.Controllers.TeamProjectCollection
{
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