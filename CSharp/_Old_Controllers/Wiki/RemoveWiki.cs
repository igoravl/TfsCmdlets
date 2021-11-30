using System.Collections.Generic;
using System.Composition;
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Wiki.WebApi;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Controllers.Wiki
{
    
    [CmdletController]
    internal class RemoveWikiController : ControllerBase<WikiV2>
    {
        public override IEnumerable<WikiV2> Invoke()
        {
            var tp = Data.GetProject();
            var wikis = Data.GetItems<WikiV2>(parameters);
            var force = parameters.Get<bool>("Force");

            var client = Data.GetClient<WikiHttpClient>(parameters);

            foreach (var w in wikis)
            {
                if (!PowerShell.ShouldProcess(tp, $"Remove wiki '{w.Name}'")) continue;

                if (!force && !PowerShell.ShouldContinue($"Are you sure you want to delete wiki '{w.Name}'?")) continue;

                if (w.Type == WikiType.ProjectWiki)
                {
                    Data.RemoveItem<GitRepository>(parameters.Override(new
                    {
                        Repository = w.Name,
                        Project = w.ProjectId,
                        Force = true
                    }));
                }
                else
                {
                    client.DeleteWikiAsync(tp.Name, w.Id).Wait();
                }
            }

            return null;
        }

        [ImportingConstructor]
        public RemoveWikiController(IPowerShellService powerShell, IDataManager data, ILogger logger)
            : base(powerShell, data, logger)
        {
        }
    }
}