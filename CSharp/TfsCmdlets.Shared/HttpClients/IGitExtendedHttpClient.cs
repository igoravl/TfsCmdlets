using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(GitExtendedHttpClient))]
    public partial interface IGitExtendedHttpClient
    {
    }
}