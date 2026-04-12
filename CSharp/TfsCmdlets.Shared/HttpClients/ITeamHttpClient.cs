using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.HttpClients {

    [HttpClient(typeof(TeamHttpClient))]
    public partial interface ITeamHttpClient
    {
    }
}