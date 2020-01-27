#define ITEM_TYPE Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
<#
.SYNOPSIS
Gets information about one or more team project collections.

.DESCRIPTION
The Get-TfsTeamProjectCollection cmdlets gets one or more Team Project Collection objects (an instance of Microsoft.TeamFoundation.Client.TfsTeamProjectCollection) from a TFS instance. 
Team Project Collection objects can either be obtained by providing a fully-qualified URL to the collection or by collection name (in which case a TFS Configuration Server object is required).

.PARAMETER Collection
HELP_PARAM_COLLECTION

.PARAMETER Server
Specifies either a URL/name of the Team Foundation Server configuration server (the "root" of a TFS installation) to connect to, or a previously initialized Microsoft.TeamFoundation.Client.TfsConfigurationServer object.

.PARAMETER Current
Returns the team project collection specified in the last call to Connect-TfsTeamProjectCollection (i.e. the "current" project collection)

.PARAMETER Credential
HELP_PARAM_TFSCREDENTIAL

.EXAMPLE
Get-TfsTeamProjectCollection http://

.INPUTS
Microsoft.TeamFoundation.Client.TfsConfigurationServer
System.String
System.Uri

.NOTES
Cmdlets in the TfsCmdlets module that operate on a collection level require a TfsConfigurationServer object to be provided via the -Server argument. If absent, it will default to the connection opened by Connect-TfsConfigurationServer.
#>
Function Get-TfsTeamProjectCollection
{
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidUsingPlainTextForPassword', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUsePSCredentialType', '')]
	[CmdletBinding(DefaultParameterSetName='Get by collection')]
	[OutputType('ITEM_TYPE')]
	Param
	(
		[Parameter(Position=0, ParameterSetName="Get by collection")]
        [SupportsWildcards()]
		[object] 
		$Collection = "*",
	
		[Parameter(ValueFromPipeline=$true, ParameterSetName="Get by collection")]
		[object] 
		$Server,
	
		[Parameter(Position=0, ParameterSetName="Get current")]
        [switch]
        $Current,

		[Parameter(ParameterSetName="Get by collection")]
		[object]
		$Credential
	)

	Begin
	{
		REQUIRES(Microsoft.TeamFoundation.Client)
	}

	Process
	{
		if (($Current.IsPresent -or (-not $Collection)) -and ($script:TfsTpcConnection))
        {
            return $script:TfsTpcConnection
		}
		
		if ($Collection -is [Microsoft.TeamFoundation.Client.TfsTeamProjectCollection])
		{
			return $Collection
		}

		$cred = Get-TfsCredential -Credential $Credential

		if ($Collection -is [Uri] -or ([Uri]::IsWellFormedUriString($Collection, [UriKind]::Absolute)))
		{
			return New-Object Microsoft.TeamFoundation.Client.TfsTeamProjectCollection -ArgumentList ([uri]$Collection), $cred
		}

		if ($Collection -is [string])
		{
			$configServer = Get-TfsConfigurationServer -Server $Server -Credential $cred

			if($configServer)
			{
				$filter = [Guid[]] @([Microsoft.TeamFoundation.Framework.Common.CatalogResourceTypes]::ProjectCollection)
				$collections = $configServer.CatalogNode.QueryChildren($filter, $false, [Microsoft.TeamFoundation.Framework.Common.CatalogQueryOptions]::None) 
				$collections = $collections | Select-Object -ExpandProperty Resource | Where-Object DisplayName -like $Collection

				foreach ($tpc in $collections)
				{
					$collectionId = $tpc.Properties["InstanceId"]
					Write-Output $configServer.GetTeamProjectCollection($collectionId)
				}
			}

			$registeredCollection = Get-TfsRegisteredTeamProjectCollection $Collection

			if($registeredCollection.Count)
			{
				foreach($tpc in $registeredCollection)
				{
					Write-Output (New-Object Microsoft.TeamFoundation.Client.TfsTeamProjectCollection -ArgumentList ([uri]$tpc.Uri), $cred)
				}

				return
			}
		}
	}
}
