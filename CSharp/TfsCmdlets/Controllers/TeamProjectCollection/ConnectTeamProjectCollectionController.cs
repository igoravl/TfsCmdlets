using TfsCmdlets.Models;

namespace TfsCmdlets.Controllers.TeamProjectCollection
{
    [CmdletController(typeof(Connection))]
    partial class ConnectTeamProjectCollectionController
    {
        [Import]
        private ICurrentConnections CurrentConnections { get; }

        protected override IEnumerable Run()
        {
            var tpc = Data.GetCollection();

            tpc.Connect();
            var srv = tpc.ConfigurationServer;
            CurrentConnections.Set(srv, tpc);

            Logger.Log($"Connected to {tpc.Uri}, ID {tpc.ServerId}, as '{tpc.CurrentUserDisplayName}'");

            yield return tpc;
        }
    }
}