using System.Collections.Generic;
using System.Composition;
using TfsCmdlets.Services;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController]
    internal class DisconnectTeamProjectController : ControllerBase<WebApiTeamProject>
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

        private ICurrentConnections CurrentConnections { get; }

        [ImportingConstructor]
        public DisconnectTeamProjectController(ICurrentConnections currentConnections, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            CurrentConnections = currentConnections;
        }
    }
}