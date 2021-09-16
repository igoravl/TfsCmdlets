using System.Collections.Generic;
using System.Composition;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Commands.TeamProjectCollection
{
    [Command]
    internal class GetTeamProjectCollection : CommandBase<TpcConnection>
    {
        private ICurrentConnections CurrentConnections { get; }

        public override IEnumerable<TpcConnection> Invoke(ParameterDictionary parameters)
        {
            var current = parameters.Get<bool>("Current");

            if (current)
            {
                yield return CurrentConnections.Collection;
                yield break;
            }

            yield return Connections.GetCollection();
        }

        [ImportingConstructor]
        public GetTeamProjectCollection(ICurrentConnections currentConnections, IPowerShellService powerShell, IConnectionManager connections, IDataManager data, ILogger logger)
                : base(powerShell, connections, data, logger)
        {
            CurrentConnections = currentConnections;
        }

    }
}