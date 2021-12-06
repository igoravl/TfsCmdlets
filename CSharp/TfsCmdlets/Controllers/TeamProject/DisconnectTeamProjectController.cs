using System.Collections.Generic;
using System.Composition;
using TfsCmdlets.Services;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController(typeof(WebApiTeamProject))]
    partial class DisconnectTeamProjectController
    {
        public override IEnumerable<WebApiTeamProject> Invoke()
        {
            CurrentConnections.Set(
                CurrentConnections.Server,
                CurrentConnections.Collection,
                null
            );

            return null;
        }

        [Import]
        private ICurrentConnections CurrentConnections { get; }
    }
}