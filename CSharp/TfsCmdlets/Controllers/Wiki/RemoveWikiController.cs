using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Wiki.WebApi;

namespace TfsCmdlets.Controllers.Wiki
{
    [CmdletController(typeof(WikiV2))]
    partial class RemoveWikiController
    {
        protected override IEnumerable Run()
        {
            var tp = Data.GetProject();
            var client = Data.GetClient<WikiHttpClient>();

            var wikis = Data.GetItems<WikiV2>();
            var force = Parameters.Get<bool>("Force");

            foreach (var w in wikis)
            {
                if (!PowerShell.ShouldProcess(tp, $"Remove wiki '{w.Name}'")) continue;
                if (!force && !PowerShell.ShouldContinue($"Are you sure you want to delete wiki '{w.Name}'?")) continue;

                if (w.Type == WikiType.ProjectWiki)
                {
                    Data.RemoveItem<GitRepository>(new
                    {
                        Repository = w.Name,
                        Project = w.ProjectId,
                        Force = true
                    });
                }
                else
                {
                    client.DeleteWikiAsync(tp.Name, w.Id).Wait();
                }
            }

            return null;
        }
    }
}
