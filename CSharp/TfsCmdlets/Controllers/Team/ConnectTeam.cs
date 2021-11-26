using System.Collections.Generic;
using System.Composition;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Controllers.Team
{
    [CmdletController]
    internal class ConnectTeam : ControllerBase<WebApiTeam>
    {
        private ICurrentConnections CurrentConnections { get; }

        public override IEnumerable<WebApiTeam> Invoke(ParameterDictionary parameters)
        {

            var parms = new ParameterDictionary(parameters);

            var tpc = Data.GetCollection(parameters);
            parms["Collection"] = null;
            CurrentConnections.Set(tpc.ConfigurationServer, tpc);

            var tp = Data.GetProject(parms);
            parms["Project"] = null;
            CurrentConnections.Set(tpc.ConfigurationServer, tpc, tp);

            var t = Data.GetTeam(parms);
            CurrentConnections.Set(tpc.ConfigurationServer, tpc, tp, t);

            // TODO: 
            //this.Log($"Adding '{tp.Name} to the MRU list");
            //_SetMru "Server" - Value(srv.Uri)
            //_SetMru "Collection" - Value(tpc.Uri)
            //_SetMru "Project" - Value(tp.Name)

            Logger.Log($"Connected to '{t.Name}'");

            yield return t;
        }

        [ImportingConstructor]
        public ConnectTeam(ICurrentConnections currentConnections, IPowerShellService powerShell, IDataManager data, ILogger logger) : base(powerShell, data, logger)
        {
            CurrentConnections = currentConnections;
        }
    }
}