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
    [Cmdlet(VerbsCommon.Get, "WorkItemTag")]
    [OutputType(typeof(WebApiTagDefinition))]
    public class GetWorkItemTag: BaseCmdlet
    {
/*
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [Alias("Name")]
        [object] 
        Tag = "*",

        [Parameter()]
        public SwitchParameter IncludeInactive { get; set; }

        [Parameter(ValueFromPipeline=true)]
        public object Project { get; set; }

        [Parameter()]
        public object Collection { get; set; }

    protected override void BeginProcessing()
    {
        #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.Core.WebApi"
    }

    protected override void ProcessRecord()
    {
        if (Tag is Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition) { this.Log("Input item is of type Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition; returning input item immediately, without further processing."; WriteObject(Tag }); return;);

        tp = Get-TfsTeamProject -Project Project -Collection Collection; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        client = Get-TfsRestClient "Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient" -Collection tpc

        task = client.GetTagsAsync(tp.Guid, IncludeInactive.IsPresent); result = task.Result; if(task.IsFaulted) { _throw new Exception($"Error retrieving work item tag "{Tag}"" task.Exception.InnerExceptions })

        WriteObject(result | Where-Object Name -like Tag | Add-Member -Name TeamProject -MemberType NoteProperty -Value TP.Name -PassThru); return;
    }
}
*/
}
}