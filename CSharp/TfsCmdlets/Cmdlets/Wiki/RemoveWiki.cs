using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Wiki.WebApi;

namespace TfsCmdlets.Cmdlets.Wiki
{
    /// <summary>
    /// Deletes one or more Git repositories from a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, DefaultParameterSetName = "Remove code wiki")]
    partial class RemoveWiki
    {
        /// <summary>
        /// Specifies the Wiki to be deleted.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Remove code wiki")]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Wiki { get; set; }

        /// <summary>
        /// Deletes the provisioned ("project") Wiki of the specified Team Project.
        /// </summary>
        [Parameter(ParameterSetName = "Remove Project Wiki", Mandatory=true)]
        public SwitchParameter ProjectWiki { get; set; }
    }

[CmdletController(typeof(WikiV2), Client=typeof(IWikiHttpClient))]
    partial class RemoveWikiController
    {
        protected override IEnumerable Run()
        {
            var wikis = Data.GetItems<WikiV2>();
            var force = Parameters.Get<bool>("Force");

            foreach (var w in wikis)
            {
                var tp = Data.GetItem<WebApiTeamProject>(new { Project = w.ProjectId });

                if (!PowerShell.ShouldProcess($"[Project: {tp.Name}]/[Wiki: {w.Name}]", $"Remove wiki '{w.Name}'")) continue;

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
                    Client.DeleteWikiAsync(tp.Name, w.Id).Wait();
                }
            }

            return null;
        }
    }
}