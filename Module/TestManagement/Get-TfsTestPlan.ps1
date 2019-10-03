<#
.SYNOPSIS
    Gets the contents of one or more test plans

.DESCRIPTION

.EXAMPLE
    Get-TfsTestPlan 'Release 1 - Sprint*' -Project 'Fabrikam'
    
    Returns all test plans from team project 'Fabrikam' whose names start with 'Release 1 - Sprint'

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project

.OUTPUTS
    Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan

.NOTES
    
#>
Function Get-TfsTestPlan
{
    [CmdletBinding()]
    [OutputType('Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan')]
    Param
    (
        # Specifies the test plan name. Wildcards are supported
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [Alias("Id")]
        [Alias("Name")]
        [object]
        $TestPlan = '*',

        # Specifices the plan's owner name
        [Parameter()]
        [string]
        $Owner,

        # Get only basic properties of the test plan
        [Parameter()]
        [switch]
        $NoPlanDetails,

        # Get just the active plans
        [Parameter()]
        [switch]
        $FilterActivePlans,

        # Specifies the team project
        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        # Specifies the collection / organization
        [Parameter()]
        [Alias("Organization")]
        [object]
        $Collection
    )

    Begin
    {
        REQUIRES(Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi)
    }

    Process
    {
        if ($TestPlan -is [Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan])
        {
            return $TestPlan
        }

        $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
        $tpc = $tp.Store.TeamProjectCollection
        GET_CLIENT('Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlanHttpClient')

        return $client.GetTestPlansAsync(
            $tp.Name, $Owner, $null, 
            (-not $NoPlanDetails.IsPresent), 
            $FilterActivePlans.IsPresent).Result | Where-Object Name -like $TestPlan
    }
}