using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    /// <summary>
    /// Creates a new work item tag.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsWorkItemTag", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiTagDefinition))]
    public class NewWorkItemTag : CmdletBase
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord() => throw new System.NotImplementedException();

        //         [Parameter(Position=0,ValueFromPipeline=true)]
        //         [Alias("Name")]
        // public string Tag,

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
        //         tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        //         if(! ShouldProcess(tp.Name, $"Create work item tag "{Tag}""))
        //         {
        //             return
        //         }

        //         var client = GetClient<Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient>();

        //         task = client.CreateTagAsync(tp.Guid, Tag); result = task.Result; if(task.IsFaulted) { _throw new Exception($"Error creating work item tag "{Tag}"" task.Exception.InnerExceptions })

        //         if(Passthru)
        //         {
        //             WriteObject(result); return;
        //         }
        //     }
        // }
    }
}