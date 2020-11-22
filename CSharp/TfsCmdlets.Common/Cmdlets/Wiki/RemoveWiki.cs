using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Wiki.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Wiki
{
    /// <summary>
    /// Deletes one or more Git repositories from a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsWiki", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium, DefaultParameterSetName = "Remove code wiki")]
    public class RemoveWiki : RemoveCmdletBase<WikiV2>
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

    partial class WikiDataService
    {
        protected override void DoRemoveItem()
        {
            var (tpc, tp) = GetCollectionAndProject();
            var client = GetClient<WikiHttpClient>();

            var wikis = GetItems<WikiV2>();
            var force = GetParameter<bool>("Force");

            foreach (var w in wikis)
            {
                if (!ShouldProcess(tp, $"Remove wiki '{w.Name}'")) continue;
                if (!force && !ShouldContinue($"Are you sure you want to delete wiki '{w.Name}'?")) continue;

                if(w.Type == WikiType.ProjectWiki)
                {
                    RemoveItem<GitRepository>(new {
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
        }
    }
}