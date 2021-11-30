using System.Collections.Generic;
using System.Composition;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Controllers.TeamProjectCollection
{
    [CmdletController]
    internal class GetTeamProjectCollection : ControllerBase<Connection>
    {
        private ICurrentConnections CurrentConnections { get; }

        public override IEnumerable<Connection> Invoke()
        {
            var current = Parameters.Get<bool>("Current");

            if (current)
            {
                yield return CurrentConnections.Collection;
                yield break;
            }

            yield return Data.GetCollection();
        }

        [ImportingConstructor]
        public GetTeamProjectCollection(ICurrentConnections currentConnections, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
                : base(powerShell, data, parameters, logger)
        {
            CurrentConnections = currentConnections;
        }

    }
}