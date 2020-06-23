using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    /// <summary>
    /// Deletes one or more work item tags.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsWorkItemTag", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    public class RemoveWorkItemTag : CmdletBase
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord() => throw new System.NotImplementedException();

        //         [Parameter(Position=0,ValueFromPipeline=true)]
        //         [SupportsWildcards()]
        //         [Alias("Name")]
        // public object Tag = "*";

        //         [Parameter()]
        //         public SwitchParameter IncludeInactive { get; set; }

        //         [Parameter()]
        //         public object Project { get; set; }

        //         [Parameter()]
        //         public object Collection { get; set; }

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
        //             if(t.TeamProject) {Project = t.TeamProject}; tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        //             if(! ShouldProcess(tp.Name, $"Delete work item tag [{{t}.Name}]"))
        //             {
        //                 continue
        //             }

        //             var client = GetClient<Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient>();

        //             task = client.DeleteTagAsync(tp.Guid, t.Id); result = task.Result; if(task.IsFaulted) { _throw new Exception($"Error deleting work item tag [{{t}.Name}]"" task.Exception.InnerExceptions })
        //         }
        //     }
        // }
    }
}
