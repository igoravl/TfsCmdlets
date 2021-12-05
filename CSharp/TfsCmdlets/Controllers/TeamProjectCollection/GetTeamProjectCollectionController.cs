using System.Collections.Generic;
using System.Composition;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Controllers.TeamProjectCollection
{
    [CmdletController(typeof(Connection))]
    partial class GetTeamProjectCollectionController 
    {
        [Import]
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
    }
}