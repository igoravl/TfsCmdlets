using System.Collections.Generic;
using System.Composition;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Commands.TeamProjectCollection
{
    [Command]
    internal class GetTeamProjectCollection : CommandBase<Connection>
    {
        private ICurrentConnections CurrentConnections { get; }

        public override IEnumerable<Connection> Invoke(ParameterDictionary parameters)
        {
            var current = parameters.Get<bool>("Current");

            if (current)
            {
                yield return CurrentConnections.Collection;
                yield break;
            }

            yield return Data.GetCollection();
        }

        [ImportingConstructor]
        public GetTeamProjectCollection(ICurrentConnections currentConnections, IPowerShellService powerShell, IDataManager data, ILogger logger)
                : base(powerShell, data, logger)
        {
            CurrentConnections = currentConnections;
        }

    }
}