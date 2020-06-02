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

        TObj GetOne<TObj>(BaseCmdlet cmdlet, ParameterDictionary overriddenParameters = null, object userState = null) where TObj : class;

        IEnumerable<TObj> GetMany<TObj>(BaseCmdlet cmdlet, ParameterDictionary overriddenParameters = null, object userState = null) where TObj : class;

        TfsConnection GetCollection(BaseCmdlet cmdlet, ParameterDictionary overriddenParameters = null);

        (TfsConnection, WebApiTeamProject) GetCollectionAndProject(BaseCmdlet cmdlet, ParameterDictionary overriddenParameters = null);

        (TfsConnection, WebApiTeamProject, WebApiTeam) GetCollectionProjectAndTeam(BaseCmdlet cmdlet, ParameterDictionary overriddenParameters = null);

        TfsConnection GetServer(BaseCmdlet cmdlet, ParameterDictionary overriddenParameters = null);
        
    }
}
