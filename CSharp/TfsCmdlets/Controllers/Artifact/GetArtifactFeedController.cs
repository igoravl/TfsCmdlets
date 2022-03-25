using Microsoft.VisualStudio.Services.Feed.WebApi;

namespace TfsCmdlets.Controllers.Artifact
{
    [CmdletController(typeof(Feed))]
    partial class GetArtifactFeedController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<FeedHttpClient>();

            foreach (var input in Feed)
            {
                var feed = input switch
                {
                    string s when s.IsGuid() => Guid.Parse(s),
                    _ => input
                };

                switch (feed)
                {
                    case Feed f:
                        {
                            yield return f;
                            break;
                        }
                    case Guid g:
                        {
                            yield return client.GetFeedsAsync(Role)
                                .GetResult($"Error getting artifact feed(s) '{g}'")
                                .Where(f => f.Id == g);
                            break;
                        }
                    case string s when !string.IsNullOrEmpty(s):
                        {
                            foreach (var o in client.GetFeedsAsync(Role)
                                .GetResult($"Error getting artifact feed(s) '{s}'")
                                .Where(f1 => f1.Name.IsLike(s) && (
                                    (string.IsNullOrEmpty(f1.Project?.Name) && ((Scope & ProjectOrCollectionScope.Collection) > 0)) ||
                                    (!string.IsNullOrEmpty(f1.Project?.Name) && ((Scope & ProjectOrCollectionScope.Project) > 0)))))
                            {
                                yield return o;
                            }
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent feed '{feed}'"));
                            break;
                        }
                }
            }
        }
    }
}