using Microsoft.VisualStudio.Services.Feed.WebApi;

namespace TfsCmdlets.HttpClients {

    [HttpClient(typeof(FeedHttpClient))]
    partial interface IFeedHttpClient {

    }
}