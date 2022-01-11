using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Controllers.Team
{
    [CmdletController(typeof(Models.Team))]
    partial class ConnectTeamController
    {
        [Import]
        private ICurrentConnections CurrentConnections { get; }

        protected override IEnumerable Run()
        {
            var tpc = Data.GetCollection();
            var tp = Data.GetProject();
            var t = Data.GetTeam();

            CurrentConnections.Set(tpc.ConfigurationServer, tpc, tp, t);

            Logger.Log($"Connected to '{t.Name}'");

            yield return t;
        }
    }
}