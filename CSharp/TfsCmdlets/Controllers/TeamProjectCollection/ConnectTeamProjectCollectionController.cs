using TfsCmdlets.Models;

namespace TfsCmdlets.Controllers.TeamProjectCollection
{
    [CmdletController(typeof(Connection))]
    partial class ConnectTeamProjectCollectionController 
    {
        [Import]
        private ICurrentConnections CurrentConnections { get; }

        public override IEnumerable<Connection> Invoke()
        {
            var tpc = Data.GetCollection();

            tpc.Connect();
            var srv = tpc.ConfigurationServer;
            CurrentConnections.Set(srv, tpc);

            Logger.Log($"Connected to {tpc.Uri}, ID {tpc.ServerId}, as '{tpc.AuthorizedIdentity.DisplayName}'");

            yield return tpc;
        }
    }
}