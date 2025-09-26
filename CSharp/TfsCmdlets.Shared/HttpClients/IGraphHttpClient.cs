using Microsoft.VisualStudio.Services.Graph.Client;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(GraphHttpClient))]
    public partial interface IGraphHttpClient
    {
    }
}