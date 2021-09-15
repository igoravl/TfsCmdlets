using System.Collections.Generic;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using TfsCmdlets.Services.Impl;

namespace TfsCmdlets.Commands.TeamProjectCollection
{
    [Command]
    internal class GetTeamProjectCollection : GetCommand<TpcConnection>
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

            yield return Collection;
        }

        protected GetTeamProjectCollection(ICurrentConnections currentConnections, TpcConnection collection)
            : base(collection)
        {
            CurrentConnections = currentConnections;
        }

    }
}