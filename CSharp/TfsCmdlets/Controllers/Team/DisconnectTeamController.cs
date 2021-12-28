using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController(typeof(Models.Team))]
    partial class DisconnectTeamController
    {
        public override IEnumerable<Models.Team> Invoke()
        {
            CurrentConnections.Set(
                CurrentConnections.Server,
                CurrentConnections.Collection,
                CurrentConnections.Project
            );

            return null;
        }

        [Import]
        private ICurrentConnections CurrentConnections { get; }
    }
}