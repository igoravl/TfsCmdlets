#=====================
# Team cmdlets
#=====================

Function New-TfsTeam
{
	param
	(
		[Parameter(ParameterSetName="Not connected",Mandatory=$true)]
		[string] 
		$CollectionUrl = $null,
	
		[Parameter(ParameterSetName="Not connected",Mandatory=$true)]
		[Parameter(ParameterSetName="Connected",Mandatory=$true)]
		[string] 
		$ProjectName,
	
		[Parameter(ParameterSetName="Not connected",Mandatory=$true, ValueFromPipeline=$true)]
		[Parameter(ParameterSetName="Connected",Mandatory=$true, ValueFromPipeline=$true)]
		[string] 
		$Name,
	
		[Parameter(ParameterSetName="Not connected")]
		[Parameter(ParameterSetName="Connected")]
		[string] 
		$Description,
	
		[Parameter(ParameterSetName="Not connected")]
		[switch] 
		$UseDefaultCredentials,
	
		[Parameter(ParameterSetName="Not connected")]
		[System.Management.Automation.PSCredential] [System.Management.Automation.Credential()]
		$Credential = [System.Management.Automation.PSCredential]::Empty
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
