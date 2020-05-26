/*
.SYNOPSIS
    Gets one or more work item tags
.DESCRIPTION
    
.EXAMPLE

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
.OUTPUTS
    Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition
.NOTES
*/

using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    [Cmdlet(VerbsCommon.New, "WorkItemTag", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiTagDefinition))]
    public class NewWorkItemTag : BaseCmdlet
    {
        /*
                [Parameter(Position=0,ValueFromPipeline=true)]
                [Alias("Name")]
                [string] 
                Tag,

                [Parameter()]
                public object Project { get; set; }

                [Parameter()]
                public object Collection { get; set; }

                [Parameter()]
                public SwitchParameter Passthru { get; set; }

            protected override void BeginProcessing()
            {
                #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.Core.WebApi"
            }

            protected override void ProcessRecord()
            {
                tp = Get-TfsTeamProject -Project Project -Collection Collection; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

                if(! ShouldProcess(tp.Name, $"Create work item tag "{Tag}""))
                {
                    return
                }

                client = Get-TfsRestClient "Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient" -Collection tpc

                task = client.CreateTagAsync(tp.Guid, Tag); result = task.Result; if(task.IsFaulted) { _throw new Exception($"Error creating work item tag "{Tag}"" task.Exception.InnerExceptions })

                if(Passthru)
                {
                    WriteObject(result); return;
                }
            }
        }
        */
    }
}
