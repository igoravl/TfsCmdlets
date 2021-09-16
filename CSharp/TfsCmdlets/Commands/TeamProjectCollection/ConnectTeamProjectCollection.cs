using System.Collections.Generic;
using System.Composition;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Commands.TeamProjectCollection
{
    [Command]
    internal class ConnectTeamProjectCollection : CommandBase<TpcConnection>
    {
        public ICurrentConnections CurrentConnections { get; }

        public override IEnumerable<TpcConnection> Invoke(ParameterDictionary parameters)
        {
            var tpc = Connections.GetCollection();

            tpc.Connect();
            var srv = tpc.ConfigurationServer;
            CurrentConnections.Set(srv, tpc);

            Logger.Log($"Connected to {tpc.Uri}, ID {tpc.ServerId}, as '{tpc.AuthorizedIdentity.DisplayName}'");

            yield return tpc;
        }

        [ImportingConstructor]
        public ConnectTeamProjectCollection(ICurrentConnections currentConnections, IConnectionManager connections, IDataManager data, ILogger logger) : base(connections, data, logger)
        {
            CurrentConnections = currentConnections;
        }
    }
}