using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Commands.Team
{
    [Command]
    internal class ConnectTeam : CommandBase<WebApiTeam>
    {
        private ICurrentConnections CurrentConnections { get; }

        public override IEnumerable<WebApiTeam> Invoke(ParameterDictionary parameters)
        {
            var tpc = Data.GetCollection();
            var tp = Data.GetProject();
            var t = Data.GetTeam();

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