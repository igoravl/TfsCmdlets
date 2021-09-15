using System.Collections.Generic;
using TfsCmdlets.Commands;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Commands.TeamProjectCollection
{
    [Command]
    internal class NewTeamProjectCollection : CommandBase<TpcConnection>
    {
        public NewTeamProjectCollection(TpcConnection collection) : base(collection)
        {
        }

        public override IEnumerable<TpcConnection> Invoke(ParameterDictionary parameters)
        {
            return null;
        }
    }
}