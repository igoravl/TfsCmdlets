using System.Collections.Generic;
using System.Composition;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Controllers.TeamProjectCollection
{
    [CmdletController]
    internal class ConnectTeamProjectCollection : ControllerBase<Connection>
    {
        public ICurrentConnections CurrentConnections { get; }

        public override IEnumerable<Connection> Invoke()
        {
            var tpc = Data.GetCollection();

            tpc.Connect();
            var srv = tpc.ConfigurationServer;
            CurrentConnections.Set(srv, tpc);

            Logger.Log($"Connected to {tpc.Uri}, ID {tpc.ServerId}, as '{tpc.AuthorizedIdentity.DisplayName}'");

            yield return tpc;
        }

        [ImportingConstructor]
        public ConnectTeamProjectCollection(ICurrentConnections currentConnections, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
         : base(powerShell, data, parameters, logger)
        {
            CurrentConnections = currentConnections;
        }
    }
}