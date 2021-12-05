using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Operations;
using TfsCmdlets.Cmdlets.TeamProject;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController(typeof(WebApiTeamProject))]
    partial class ConnectTeamProjectController
    {
        public override IEnumerable<WebApiTeamProject> Invoke()
        {
            var tpc = Data.GetCollection();
            var tp = Data.GetProject();

            CurrentConnections.Set(tpc.ConfigurationServer, tpc, tp);

            // TODO: 
            //Logger.Log($"Adding '{tp.Name} to the MRU list");
            //_SetMru "Server" - Value(srv.Uri)
            //_SetMru "Collection" - Value(tpc.Uri)
            //_SetMru "Project" - Value(tp.Name)

            Logger.Log($"Connected to '{tp.Name}'");

            yield return tp;
        }

        [Import]
        private ICurrentConnections CurrentConnections { get; }
    }
}