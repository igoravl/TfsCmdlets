using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    /// <summary>
    /// Gets one or more work item tags.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsWorkItemTag")]
    [OutputType(typeof(WebApiTagDefinition))]
    public class GetWorkItemTag : CmdletBase
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord() => throw new System.NotImplementedException();


        //         [Parameter(Position=0)]
        //         [SupportsWildcards()]
        //         [Alias("Name")]
        //         public object Tag = "*";

        //         [Parameter()]
        //         public SwitchParameter IncludeInactive { get; set; }

        //         [Parameter(ValueFromPipeline=true)]
        //         public object Project { get; set; }

        //         [Parameter()]
        //         public object Collection { get; set; }

        //     protected override void BeginProcessing()
        //     {
        //         #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.Core.WebApi"
        //     }

        //         /// <summary>
        //         /// Performs execution of the command
        //         /// </summary>
        //         protected override void DoProcessRecord()
        //     {
        //         if (Tag is Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition) { this.Log("Input item is of type Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition; returning input item immediately, without further processing."; WriteObject(Tag }); return;);

        //         tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        //         var client = GetClient<Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient>();

        //         task = client.GetTagsAsync(tp.Guid, IncludeInactive.IsPresent); result = task.Result; if(task.IsFaulted) { _throw new Exception($"Error retrieving work item tag "{Tag}"" task.Exception.InnerExceptions })

        //         WriteObject(result | Where-Object Name -like Tag | Add-Member -Name TeamProject -MemberType NoteProperty -Value TP.Name -PassThru); return;
        //     }
        // }
    }
}
