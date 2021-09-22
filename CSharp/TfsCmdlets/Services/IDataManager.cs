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
        T GetItem<T>(object parameters);

        IEnumerable<T> GetItems<T>(object parameters);

        IEnumerable<T> Invoke<T>(string verb, object parameters);

        Connection GetServer(object parameters);

        Connection GetCollection(object parameters);
        
        WebApiTeamProject GetProject(object parameters);
        
        WebApiTeam GetTeam(object parameters);

        T GetClient<T>(object parameters);

        T GetService<T>(object parameters);
    }
}