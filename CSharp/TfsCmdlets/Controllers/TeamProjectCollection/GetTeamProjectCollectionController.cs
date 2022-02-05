using TfsCmdlets.Models;

namespace TfsCmdlets.Controllers.TeamProjectCollection
{
    [CmdletController(typeof(Connection))]
    partial class GetTeamProjectCollectionController 
    {
        [Import]
        private ICurrentConnections CurrentConnections { get; }

        protected override IEnumerable Run()
        {
            var current = Parameters.Get<bool>("Current");

            if (current)
            {
                yield return CurrentConnections.Collection;
                yield break;
            }

            yield return Data.GetCollection(new { Collection = Collection ?? Parameters.Get<object>("Organization") });
        }
    }
}