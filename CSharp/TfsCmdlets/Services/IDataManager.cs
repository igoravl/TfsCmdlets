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
        T GetItem<T>(ParameterDictionary parameters);

        IEnumerable<T> GetItems<T>(ParameterDictionary parameters);

        T NewItem<T>(ParameterDictionary parameters);

        T SetItem<T>(ParameterDictionary parameters);

        void RemoveItem<T>(ParameterDictionary parameters);

        T RenameItem<T>(ParameterDictionary parameters);

        bool TestItem<T>(ParameterDictionary parameters);

        IEnumerable<T> Invoke<T>(string verb, ParameterDictionary parameters);

        IEnumerable<T> Invoke<T>(string verb, string noun, ParameterDictionary parameters);

        Connection GetServer(ParameterDictionary parameters);

        Connection GetCollection(ParameterDictionary parameters);
        
        WebApiTeamProject GetProject(ParameterDictionary parameters);
        
        WebApiTeam GetTeam(ParameterDictionary parameters);

        T GetClient<T>(ParameterDictionary parameters);

        T GetService<T>(ParameterDictionary parameters);
    }
}