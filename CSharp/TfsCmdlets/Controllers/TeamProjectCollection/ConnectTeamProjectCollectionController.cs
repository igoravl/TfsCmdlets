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
            object collection;

            switch (Collection)
            {
                case string s when !string.IsNullOrEmpty(s) && !Uri.IsWellFormedUriString(s, UriKind.Absolute):
                    {
                        if (Data.TryGetServer(out var server) && !server.IsHosted)
                        {
                            throw new NotImplementedException("Connecting to an on-premises collection by name is not yet supported.");
                        }

                        collection = $"https://dev.azure.com/{s.Trim('/')}";
                        break;
                    }
                default:
                    {
                        collection = Collection;
                        break;
                    }
            }

            var tpc = Data.GetCollection(new { Collection = collection });

            tpc.Connect();
            var srv = tpc.ConfigurationServer;
            CurrentConnections.Set(srv, tpc);

            Logger.Log($"Connected to {tpc.Uri}, ID {tpc.ServerId}, as '{tpc.CurrentUserDisplayName}'");

            yield return tpc;
        }
    }
}