using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(GitExtendedHttpClient))]
    partial interface IGitExtendedHttpClient
    {
    }
}