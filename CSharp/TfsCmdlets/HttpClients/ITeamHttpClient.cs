using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.HttpClients {

    [HttpClient(typeof(TeamHttpClient))]
    partial interface ITeamHttpClient
    {
    }
}