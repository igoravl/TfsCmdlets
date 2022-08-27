using Microsoft.VisualStudio.Services.Feed.WebApi;

namespace TfsCmdlets.Controllers.Artifact
{
    [CmdletController(typeof(FeedView))]
    partial class GetArtifactFeedViewController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<FeedHttpClient>();

            foreach (var input in View)
            {
                var view = input switch
                {
                    _ => input
                };

                var feed = GetItem<Feed>();

                switch (view)
                {
                    case FeedView fv:
                        {
                            yield return fv;
                            break;
                        }
                    case string s when !string.IsNullOrEmpty(s) && feed.Project == null:
                        {
                            yield return client.GetFeedViewsAsync(feed.Id.ToString())
                                .GetResult($"Error getting artifact feed view(s) '{s}'")
                                .Where(fv => fv.Name.IsLike(s));
                            break;
                        }
                    case string s when !string.IsNullOrEmpty(s):
                        {
                            yield return client.GetFeedViewsAsync(feed.Project.Id, feed.Id.ToString())
                                .GetResult($"Error getting artifact feed view(s) '{s}'")
                                .Where(fv => fv.Name.IsLike(s));
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent feed view '{view}'"));
                            break;
                        }
                }
            }
        }
    }
}