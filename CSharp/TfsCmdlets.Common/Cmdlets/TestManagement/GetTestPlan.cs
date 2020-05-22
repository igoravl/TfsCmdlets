/*
.SYNOPSIS
    Gets the contents of one or more test plans

.DESCRIPTION

.EXAMPLE
    Get-TfsTestPlan "Release 1 - Sprint*" -Project "Fabrikam"
    
    Returns all test plans from team project "Fabrikam" whose names start with "Release 1 - Sprint"

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project

.OUTPUTS
    Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan

.NOTES
    
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TestManagement
{
    [Cmdlet(VerbsCommon.Get, "TestPlan")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan))]
    public class GetTestPlan: BaseCmdlet
    {
/*
        # Specifies the test plan name. Wildcards are supported
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [Alias("Id")]
        [Alias("Name")]
        public object TestPlan { get; set; } = "*",

        # Specifices the plan"s owner name
        [Parameter()]
        public string Owner { get; set; }

        # Get only basic properties of the test plan
        [Parameter()]
        public SwitchParameter NoPlanDetails { get; set; }

        # Get just the active plans
        [Parameter()]
        public SwitchParameter FilterActivePlans { get; set; }

        # Specifies the team project
        [Parameter(ValueFromPipeline=true)]
        public object Project { get; set; }

        # Specifies the collection / organization
        [Parameter()]
        [Alias("Organization")]
        public object Collection { get; set; }

    protected override void BeginProcessing()
    {
        #_ImportRequiredAssembly -AssemblyName "Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi"
    }

    protected override void ProcessRecord()
    {
        if (TestPlan is Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan)
        {
            WriteObject(TestPlan); return;
        }

        tp = Get-TfsTeamProject -Project Project -Collection Collection; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)
        client = Get-TfsRestClient "Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlanHttpClient" -Collection tpc

        WriteObject(client.GetTestPlansAsync(); return;
            tp.Name, Owner, null, 
            (! NoPlanDetails.IsPresent), 
            FilterActivePlans.IsPresent).Result | Where-Object Name -like TestPlan
    }
}
*/
}
}
