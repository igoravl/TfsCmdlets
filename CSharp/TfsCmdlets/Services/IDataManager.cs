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
        T GetItem<T>(object overridingParameters = null);

        IEnumerable<T> GetItems<T>(object overridingParameters = null);

        T NewItem<T>(object overridingParameters = null);

        T SetItem<T>(object overridingParameters = null);

        void RemoveItem<T>(object overridingParameters = null);

        T RenameItem<T>(object overridingParameters = null);

        bool TestItem<T>(object overridingParameters = null);

        IEnumerable<T> Invoke<T>(string verb, object overridingParameters = null);

        IEnumerable<T> Invoke<T>(string verb, string noun, object overridingParameters = null);

        Connection GetServer(object overridingParameters = null);

        Connection GetCollection(object overridingParameters = null);
        
        WebApiTeamProject GetProject(object overridingParameters = null, string contextValue = null);
        
        WebApiTeam GetTeam(object overridingParameters = null, string contextValue = null);

        T GetClient<T>(object overridingParameters = null);

        T GetService<T>(object overridingParameters = null);
    }
}