using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    /// <summary>
    /// Renames a work item tag.
    /// </summary>
    [Cmdlet(VerbsCommon.Rename, "TfsWorkItemTag", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiTagDefinition))]
    public class RenameWorkItemTag : CmdletBase
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord() => throw new System.NotImplementedException();

        //         [Parameter(Position=0,ValueFromPipeline=true)]
        //         [Alias("Name")]
        // public object Tag,

        //         [Parameter(Position=1)]
        //         public string NewName { get; set; }

        //         [Parameter()]
        //         public object Project { get; set; }

        //         [Parameter()]
        //         public object Collection { get; set; }

        //         [Parameter()]
        //         public SwitchParameter Passthru { get; set; }

        //     protected override void BeginProcessing()
        //     {
        //         #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.Core.WebApi"
        //     }

        // /// <summary>
        // /// Performs execution of the command
        // /// </summary>
        // protected override void DoProcessRecord()
        //     {
        //         tags = Get-TfsWorkItemTag -Tag Tag -Project Project -Collection Collection

        //         foreach(t in tags)
        //         {
        //             if(! ShouldProcess(t.Name, $"Rename work item tag to [{NewName}]"))
        //             {
        //                 continue
        //             }

        //             if(t.TeamProject) {Project = t.TeamProject}; tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        //             var client = GetClient<Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient>();

        //             task = client.UpdateTagAsync(tp.Guid, t.Id, NewName, t.Active); result = task.Result; if(task.IsFaulted) { _throw new Exception($"Error renaming work item tag [{{t}.Name}]"" task.Exception.InnerExceptions })

        //             if(Passthru.IsPresent)
        //             {
        //                 WriteObject(result); return;
        //             }
        //         }
        //     }
        // }
    }
}
