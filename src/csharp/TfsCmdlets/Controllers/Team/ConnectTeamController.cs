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
            var team = Data.GetItem<Models.Team>();

            CurrentConnections.Set(Collection.ConfigurationServer, Collection, Project, team);

            Logger.Log($"Connected to '{team.Name}'");

            yield return Team;
        }
    }
}