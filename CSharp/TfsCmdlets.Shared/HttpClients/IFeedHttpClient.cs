using Microsoft.VisualStudio.Services.Feed.WebApi;

namespace TfsCmdlets.HttpClients {

    [HttpClient(typeof(FeedHttpClient))]
    public partial interface IFeedHttpClient {

    }
}