#=====================
# Team cmdlets
#=====================

Function New-TfsTeam
{
	param
	(
		[Parameter(Mandatory=$true)]
		[string] 
		$ProjectName,
	
		[Parameter(Mandatory=$true, ValueFromPipeline=$true)]
		[string] 
		$Name,
	
		[Parameter()]
		[string] 
		$Description
	)

	Process
	{
		$tpc = Get-TfsTeamProjectCollection -Current

		# Get Team Project
		$cssService = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService3")
		$teamProject = $cssService.GetProjectFromName($ProjectName)

		# Create Team
		$teamService = $tpc.GetService([type]"Microsoft.TeamFoundation.Client.TfsTeamService")

		$teamService.CreateTeam($teamProject.Uri, $Name, $Description, $null)
	}
}
