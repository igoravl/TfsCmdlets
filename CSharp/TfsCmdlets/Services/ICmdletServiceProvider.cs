using System.Collections.Generic;
using Microsoft.TeamFoundation.Core.WebApi;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
using Microsoft.VisualStudio.Services.Client;
using TfsCmdlets.Cmdlets;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Services
{
    public interface ICmdletServiceProvider
    {
        TService GetService<TService>(CmdletBase cmdlet, object parameters = null) where TService : IService;

        IDataService<TObj> GetDataService<TObj>(CmdletBase CmdletBase, object overriddenParameters = null) where TObj : class;

        Models.Connection GetCollection(CmdletBase cmdlet, ParameterDictionary parameters = null);

        (Models.Connection, WebApiTeamProject) GetCollectionAndProject(CmdletBase cmdlet, ParameterDictionary parameters = null);

        (Models.Connection, WebApiTeamProject, WebApiTeam) GetCollectionProjectAndTeam(CmdletBase cmdlet, ParameterDictionary parameters = null);

        Models.Connection GetServer(CmdletBase cmdlet, ParameterDictionary parameters = null);

        TClient GetClient<TClient>(CmdletBase cmdlet, ClientScope scope = ClientScope.Collection, ParameterDictionary parameters = null) where TClient : VssHttpClientBase;
    }
}
