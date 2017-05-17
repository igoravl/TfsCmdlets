<#
.SYNOPSIS
Detaches a team project collection database from a Team Foundation Server installation

.DESCRIPTION
Before you move a collection, you must first detach it from the deployment of TFS on which it is running. It's very important that you not skip this step. When you detach a collection, all jobs and services are stopped, and then the collection database is stopped. In addition, the detach process copies over the collection-specific data from the configuration database and saves it as part of the team project collection database. This configuration data is what allows the collection database to be attached to a different deployment of TFS. If that data is not present, you cannot attach the collection to any deployment of TFS except the one from which it originated

.PARAMETER Reason
Speficies a Servicing Message (optional), to provide a message for users who might try to connect to projects in this collection

.PARAMETER Timeout
The maximum period of time this cmdlet should wait for the detach procedure to complete. By default, it waits indefinitely until the collection servicing completes

.PARAMETER Collection
${HelpParam_Collection}

.PARAMETER Server
${HelpParam_Server}

.PARAMETER Credential
${HelpParam_Credential}

.INPUTS
Microsoft.TeamFoundation.Client.TfsConfigurationServer
System.String
System.Uri

.LINK
https://www.visualstudio.com/en-us/docs/setup-admin/tfs/admin/move-project-collection#1-detach-the-collection

.NOTES
Detaching a collection prevents users from accessing any projects in that collection
#>
Function Dismount-TfsTeamProjectCollection
{
	[CmdletBinding(ConfirmImpact="High", SupportsShouldProcess=$true)]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidUsingPlainTextForPassword', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUsePSCredentialType', '')]
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
		[object]
		$Credential
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
