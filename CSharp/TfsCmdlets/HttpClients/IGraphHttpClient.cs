using Microsoft.VisualStudio.Services.Graph.Client;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(GraphHttpClient))]
    partial interface IGraphHttpClient
    {
    }
}