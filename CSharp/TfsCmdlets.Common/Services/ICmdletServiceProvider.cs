using System.Collections.Generic;
using Microsoft.TeamFoundation.Core.WebApi;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
using Microsoft.VisualStudio.Services.Client;
using TfsCmdlets.Cmdlets;

namespace TfsCmdlets.Services
{
    internal interface ICmdletServiceProvider
    {
        TService GetService<TService>(BaseCmdlet cmdlet, object parameters = null) where TService : IService;

        IDataService<TObj> GetDataService<TObj>(BaseCmdlet baseCmdlet, object overriddenParameters) where TObj : class;

        TObj GetItem<TObj>(BaseCmdlet cmdlet, object parameters = null) where TObj : class;

        TObj NewItem<TObj>(BaseCmdlet cmdlet, object parameters = null) where TObj : class;

        bool TestItem<TObj>(BaseCmdlet cmdlet, object parameters = null) where TObj : class;

        IEnumerable<TObj> GetItems<TObj>(BaseCmdlet cmdlet, object parameters = null) where TObj : class;

        Models.Connection GetCollection(BaseCmdlet cmdlet, ParameterDictionary parameters = null);

        (Models.Connection, WebApiTeamProject) GetCollectionAndProject(BaseCmdlet cmdlet, ParameterDictionary parameters = null);

        (Models.Connection, WebApiTeamProject, WebApiTeam) GetCollectionProjectAndTeam(BaseCmdlet cmdlet, ParameterDictionary parameters = null);

        Models.Connection GetServer(BaseCmdlet cmdlet, ParameterDictionary parameters = null);
        
    }
}
