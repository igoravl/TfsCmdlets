using System.Collections.Generic;
using System.Composition;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Commands.TeamProjectCollection
{
    [Command]
    internal class ConnectTeamProjectCollection : CommandBase<Connection>
    {
        public ICurrentConnections CurrentConnections { get; }

        public override IEnumerable<Connection> Invoke(ParameterDictionary parameters)
        {
            var tpc = Data.GetCollection();

            tpc.Connect();
            var srv = tpc.ConfigurationServer;
            CurrentConnections.Set(srv, tpc);

            Logger.Log($"Connected to {tpc.Uri}, ID {tpc.ServerId}, as '{tpc.AuthorizedIdentity.DisplayName}'");

            yield return tpc;
        }

        [ImportingConstructor]
        public ConnectTeamProjectCollection(ICurrentConnections currentConnections, IPowerShellService powerShell, IDataManager data, ILogger logger)
         : base(powerShell, data, logger)
        {
            CurrentConnections = currentConnections;
        }
    }
}