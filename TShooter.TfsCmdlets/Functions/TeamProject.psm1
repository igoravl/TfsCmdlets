Function Get-TfsTeamProject
{
    [CmdletBinding()]
	[OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.Project])]
    Param
    (
        [Parameter(Position=0)]
		[ValidateNotNull()]
        [object] 
        $Project = '*',

        [Parameter(ValueFromPipeline=$true, Position=1)]
        [object]
        $Collection,

		[Parameter()]
		[System.Management.Automation.Credential()]
		[System.Management.Automation.PSCredential]
		$Credential
    )

    Process
    {
		if ($Project -is [Microsoft.TeamFoundation.WorkItemTracking.Client.Project])
		{
			return $Project
		}

		if ($Project -is [string])
		{
			$tpc = Get-TfsTeamProjectCollection $Collection -Credential $Credential
        
			$wiStore = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')

			$projects = _GetAllProjects $tpc | ? Name -Like $Project

			foreach($project in $projects)
			{
				$wiStore.Projects[$project.Name]
			}

		}
		else
		{
			throw "Invalid argument Project: $Project"
		}
    }
}

Function _GetAllProjects
{
    param ($tpc)

    $css = $tpc.GetService([type]'Microsoft.TeamFoundation.Server.ICommonStructureService')

    return $css.ListAllProjects() | ? Status -eq WellFormed
}