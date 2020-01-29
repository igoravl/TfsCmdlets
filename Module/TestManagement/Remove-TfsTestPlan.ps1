Function Remove-TfsTestPlan
{
    [CmdletBinding(ConfirmImpact="High", SupportsShouldProcess=$true)]
    Param
    (
        [Parameter(Position=0, Mandatory=$true, ValueFromPipeline=$true)]
        [Alias("id")]
        [ValidateNotNull()]
        [object]
        $TestPlan,

        [Parameter()]
        [object] 
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Begin
    {
		REQUIRES(Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi)
		REQUIRES(Microsoft.TeamFoundation.TestManagement.WebApi)
		$ns = 'Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi'
    }

    Process
    {
		$plan = Get-TfsTestPlan -TestPlan $TestPlan -Project $Project -Collect $Collection

		if(-not $plan)
		{
            throw "Invalid or non-existent test plan $TestPlan"
		}

		if (-not $Project)
		{
			$Project = $plan.Project.Name
		}

        GET_TEAM_PROECT($tp,$tpc)
        GET_CLIENT("$ns.TestPlanHttpClient")

        if ($PSCmdlet.ShouldProcess("Plan $($plan.Id) ('$($plan.Name)')", "Remove test plan"))
        {
            $task = $client.DeleteTestPlanAsync($tp.Name, $plan.Id)

            CHECK_ASYNC($task,$result,'Error deleting test plan')
        }
    }
}
