Function Connect-TfsTeamProject
{
	[CmdletBinding()]
	[OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.Project])]
	Param
	(
		[Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
		[object] 
		$Project,
	
		[Parameter()]
		[object] 
		$Collection,
	
		[Parameter()]
		[System.Management.Automation.Credential()]
		[System.Management.Automation.PSCredential]
		$Credential,

		[Parameter()]
		[switch]
		$Passthru
	)

	Process
	{
		$tp = (Get-TfsTeamProject -Project $Project -Collection $Collection -Credential $Credential | Select -First 1)

		if (-not $tp)
		{
			throw "Error connecting to team project $Project"
		}

		$Global:TfsProjectConnection = $tp

		Connect-TfsTeamProjectCollection -Collection $tp.Store.TeamProjectCollection

		if ($Passthru)
		{
			return $tp
		}
	}
}
