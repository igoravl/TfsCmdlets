using System.Collections.Generic;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Client;
using TfsCmdlets.Cmdlets;
using TfsConnection = TfsCmdlets.Services.Connection;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Services
{
    public interface ICmdletServiceProvider
    {
        TService GetService<TService>(BaseCmdlet cmdlet) where TService : IService;

        TObj GetInstanceOf<TObj>(BaseCmdlet cmdlet, ParameterDictionary parameters = null, object userState = null) where TObj : class;

        IEnumerable<TObj> GetCollectionOf<TObj>(BaseCmdlet cmdlet, ParameterDictionary parameters = null, object userState = null) where TObj : class;

        TfsConnection GetCollection(BaseCmdlet cmdlet, ParameterDictionary parameters = null);

        (TfsConnection, WebApiTeamProject) GetCollectionAndProject(BaseCmdlet cmdlet, ParameterDictionary parameters = null);

        (TfsConnection, WebApiTeamProject, WebApiTeam) GetCollectionProjectAndTeam(BaseCmdlet cmdlet, ParameterDictionary parameters = null);

        TfsConnection GetServer(BaseCmdlet cmdlet, ParameterDictionary parameters = null);
        
    }
}
