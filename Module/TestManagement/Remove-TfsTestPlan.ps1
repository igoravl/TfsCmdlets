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

		$tp = Get-TfsTeamProject -Project $Project -Collection $Collection
		
		if(-not $tp)
		{
            throw "Invalid or non-existent team project $Project"
		}

        $tpc = $tp.Store.TeamProjectCollection
        $client = _GetRestClient "$ns.TestPlanHttpClient" -Collection $tpc

        if ($PSCmdlet.ShouldProcess("Plan $($plan.Id) ('$($plan.Name)')", "Remove test plan"))
        {
            $task = $client.DeleteTestPlanAsync($tp.Name, $plan.Id)
            $task.Wait()

            if($task.IsFaulted)
            {
                throw "Error deleting test plan: $($resultTask.Exception.InnerExceptions | ForEach-Object {$_.ToString()})"
            }
        }
    }
}
