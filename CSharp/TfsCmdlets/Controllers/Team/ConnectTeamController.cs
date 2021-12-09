using System.Collections.Generic;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Controllers.Team
{
    [CmdletController(typeof(WebApiTeam))]
    partial class ConnectTeamController
    {
        private ICurrentConnections CurrentConnections { get; }

        public override IEnumerable<WebApiTeam> Invoke()
        {
            var parms = new ParameterDictionary();

            var tpc = Data.GetCollection();
            parms["Collection"] = null;
            CurrentConnections.Set(tpc.ConfigurationServer, tpc);

            var tp = Data.GetProject(parms);
            parms["Project"] = null;
            CurrentConnections.Set(tpc.ConfigurationServer, tpc, tp);

            var t = Data.GetTeam(parms);
            CurrentConnections.Set(tpc.ConfigurationServer, tpc, tp, t);

            // TODO: 
            //Logger.Log($"Adding '{tp.Name} to the MRU list");
            //_SetMru "Server" - Value(srv.Uri)
            //_SetMru "Collection" - Value(tpc.Uri)
            //_SetMru "Project" - Value(tp.Name)

            Logger.Log($"Connected to '{t.Name}'");

            yield return t;
        }
    }
}