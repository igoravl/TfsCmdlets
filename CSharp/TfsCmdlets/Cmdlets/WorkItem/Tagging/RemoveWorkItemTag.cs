/*
.SYNOPSIS
    Deletes one or more work item tags
.DESCRIPTION
    
.EXAMPLE

.INPUTS
    Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition
    System.String
.NOTES
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    [Cmdlet(VerbsCommon.Remove, "WorkItemTag", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    public class RemoveWorkItemTag : PSCmdlet
    {
        /*
                [Parameter(Position=0,ValueFromPipeline=true)]
                [SupportsWildcards()]
                [Alias("Name")]
                [object] 
                Tag = "*",

                [Parameter()]
                public SwitchParameter IncludeInactive { get; set; }

                [Parameter()]
                public object Project { get; set; }

                [Parameter()]
                public object Collection { get; set; }

            protected override void BeginProcessing()
            {
                #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.Core.WebApi"
            }

            protected override void ProcessRecord()
            {
                tags = Get-TfsWorkItemTag -Tag Tag -Project Project -Collection Collection

                foreach(t in tags)
                {
                    if(t.TeamProject) {Project = t.TeamProject}; tp = Get-TfsTeamProject -Project Project -Collection Collection; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

                    if(! ShouldProcess(tp.Name, $"Delete work item tag [{{t}.Name}]"))
                    {
                        continue
                    }

                    client = Get-TfsRestClient "Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient" -Collection tpc

                    task = client.DeleteTagAsync(tp.Guid, t.Id); result = task.Result; if(task.IsFaulted) { _throw new Exception($"Error deleting work item tag [{{t}.Name}]"" task.Exception.InnerExceptions })
                }
            }
        }
        */
    }
}
