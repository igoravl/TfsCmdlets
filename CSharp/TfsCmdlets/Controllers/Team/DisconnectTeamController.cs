using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController(typeof(WebApiTeam))]
    partial class DisconnectTeamController
    {
        public override IEnumerable<WebApiTeam> Invoke()
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