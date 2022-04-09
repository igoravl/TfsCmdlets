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

            var colsObj = Parameters.HasParameter("Organization") ?
                Parameters.Get<object>("Organization") :
                Parameters.Get<object>("Collection");

            IEnumerable cols;

            if (colsObj is ICollection enumObj)
            {
                cols = enumObj;
            }
            else
            {
                cols = new[] { colsObj };
            }

            foreach (var col in cols)
            {
                yield return Data.GetCollection(new { Collection = col });
            }
        }
    }
}