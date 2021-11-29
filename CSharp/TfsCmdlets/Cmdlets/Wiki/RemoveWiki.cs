using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Wiki.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Wiki
{
    /// <summary>
    /// Deletes one or more Git repositories from a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsWiki", SupportsShouldProcess = true, DefaultParameterSetName = "Remove code wiki")]
    public class RemoveWiki : CmdletBase
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

    // TODO

    //partial class WikiDataService
    //{
    //    protected override void DoRemoveItem(ParameterDictionary parameters)
    //    {
    //        var tp = Data.GetProject(parameters);
    //        var client = Data.GetClient<WikiHttpClient>(parameters);

    //        var wikis = GetItems<WikiV2>();
    //        var force = parameters.Get<bool>("Force");

    //        foreach (var w in wikis)
    //        {
    //            if (!PowerShell.ShouldProcess(tp, $"Remove wiki '{w.Name}'")) continue;
    //            if (!force && !PowerShell.ShouldContinue($"Are you sure you want to delete wiki '{w.Name}'?")) continue;

    //            if(w.Type == WikiType.ProjectWiki)
    //            {
    //                RemoveItem<GitRepository>(new {
    //                    Repository = w.Name,
    //                    Project = w.ProjectId,
    //                    Force = true
    //                });
    //            }
    //            else
    //            {
    //                client.DeleteWikiAsync(tp.Name, w.Id).Wait();
    //            }
    //        }
    //    }
    //}
}