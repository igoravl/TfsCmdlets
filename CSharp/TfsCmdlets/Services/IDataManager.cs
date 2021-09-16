using System;
using System.Collections.Generic;
using TfsCmdlets.Models;
using TfsCmdlets.Extensions;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Services
{
    public interface IDataManager
    {
        T GetItem<T>(object parameters = null);

        IEnumerable<T> GetItems<T>(object parameters = null);

        IEnumerable<T> Invoke<T>(string verb, object parameters = null);

        ServerConnection GetServer(object parameters = null);

        TpcConnection GetCollection(object parameters = null);
        
        WebApiTeamProject GetProject(object parameters = null);
        
        WebApiTeam GetTeam(object parameters = null);
    }
}