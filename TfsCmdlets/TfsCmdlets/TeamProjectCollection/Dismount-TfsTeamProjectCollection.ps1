<#

.SYNOPSIS
    Detaches a team project collection database from a Team Foundation Server installation.

.PARAMETER Collection
    ${HelpParam_Collection}

#>
Function Dismount-TfsTeamProjectCollection
{
	[CmdletBinding(ConfirmImpact="High", SupportsShouldProcess=$true)]
	Param
	(
		[Parameter(Mandatory=$true, Position=0)]
		[object] 
		$Collection,

		[Parameter(ValueFromPipeline=$true)]
		[object] 
		$Server,
	
		[Parameter()]
		[string]
		$Reason,
	
		[Parameter()]
		[timespan]
		$Timeout = [timespan]::MaxValue,

		[Parameter()]
		[System.Management.Automation.Credential()]
		[System.Management.Automation.PSCredential]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)

	Process
	{
		$tpc = Get-TfsTeamProjectCollection -Collection $Collection -Server $Server -Credential $Credential

		if ($PSCmdlet.ShouldProcess($tpc.Name, "Detach Project Collection"))
		{
			$configServer = $tpc.ConfigurationServer
			$tpcService = $configServer.GetService([type] 'Microsoft.TeamFoundation.Framework.Client.ITeamProjectCollectionService')
			$collectionInfo = $tpcService.GetCollection($tpc.InstanceId)
			$connectionString = $null

			$tpcJob = $tpcService.QueueDetachCollection($collectionInfo, $null, $Reason, [ref] $connectionString)
			$collectionInfo = $tpcService.WaitForCollectionServicingToComplete($tpcJob, $Timeout)

			return $connectionString
		}
	}
}
