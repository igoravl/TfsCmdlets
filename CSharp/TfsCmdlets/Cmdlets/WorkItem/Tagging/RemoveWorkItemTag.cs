using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    /// <summary>
    /// Deletes one or more work item tags.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsWorkItemTag", SupportsShouldProcess = true)]
    public class RemoveWorkItemTag : CmdletBase
    {
        /// <summary>
        /// Specifies one or more tags to delete. Wildcards are supported.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory=true)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Tag { get; set; }

        /// <summary>
        /// HELP_PARAM_FORCE_REMOVE
        /// </summary>
        [Parameter()]
        public SwitchParameter Force {get;set;}
    }

    // TODO

    //partial class WorkItemTagDataService
    //{
    //    protected override void DoRemoveItem(ParameterDictionary parameters)
    //    {
    //        var tags = GetItems<WebApiTagDefinition>(new{IncludeInactive=true});
    //        var force = parameters.Get<bool>(nameof(RemoveWorkItemTag.Force));

    //        var tp = Project;
    //        var client = GetClient<TaggingHttpClient>();

    //        foreach(var t in tags)
    //        {
    //            if(!PowerShell.ShouldProcess(tp, $"Delete {((bool)t.Active? "active": "inactive")} work item tag '{t.Name}'")) continue;

    //            if(((bool)t.Active) && !force && !PowerShell.ShouldContinue($"The tag '{t.Name}' is currently in use. "  +
    //                "Are you sure you want to remove this tag?"))
    //            {
    //                continue;
    //            }

    //            client.DeleteTagAsync(tp.Id, t.Id);
    //        }
    //    }
    //}
}