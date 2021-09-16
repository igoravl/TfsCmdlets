using System.Collections.Generic;
using System.Composition;
using TfsCmdlets.Commands;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Commands.TeamProjectCollection
{
    [Command]
    internal class NewTeamProjectCollection : CommandBase<TpcConnection>
    {
        public override IEnumerable<TpcConnection> Invoke(ParameterDictionary parameters)
        {
            return null;
        }

        [ImportingConstructor]
        public NewTeamProjectCollection(IPowerShellService powerShell, IConnectionManager connections, IDataManager data, ILogger logger)
            : base(powerShell, connections, data, logger)
        {
        }
    }
}