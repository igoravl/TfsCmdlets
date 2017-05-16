<#
.SYNOPSIS
Gets information about one or more team project collections.

.DESCRIPTION
The Get-TfsTeamProjectCollection cmdlets gets one or more Team Project Collection objects (an instance of Microsoft.TeamFoundation.Client.TfsTeamProjectCollection) from a TFS instance. 
Team Project Collection objects can either be obtained by providing a fully-qualified URL to the collection or by collection name (in which case a TFS Configuration Server object is required).

.PARAMETER Collection
${HelpParam_Collection}

.PARAMETER Server
Specifies either a URL/name of the Team Foundation Server configuration server (the "root" of a TFS installation) to connect to, or a previously initialized Microsoft.TeamFoundation.Client.TfsConfigurationServer object.

.PARAMETER Current
Returns the team project collection specified in the last call to Connect-TfsTeamProjectCollection (i.e. the "current" project collection)

.PARAMETER Credential
${HelpParam_TfsCredential}

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
	[CmdletBinding(DefaultParameterSetName='Get by collection')]
	[OutputType([Microsoft.TeamFoundation.Client.TfsTeamProjectCollection])]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidGlobalVars', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidUsingPlainTextForPassword', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUsePSCredentialType', '')]
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

	Process
	{
        if ($Current)
        {
            return $Global:TfsTpcConnection
        }

		if ($Collection -is [Microsoft.TeamFoundation.Client.TfsTeamProjectCollection])
		{
			return $Collection
		}

		$cred = Get-TfsCredential -Credential $Credential

		if ($Collection -is [Uri])
		{
			return New-Object Microsoft.TeamFoundation.Client.TfsTeamProjectCollection -ArgumentList ([uri]$Collection), $cred
		}

		if ($Collection -is [string])
		{
			if ([Uri]::IsWellFormedUriString($Collection, [UriKind]::Absolute))
			{
				return New-Object Microsoft.TeamFoundation.Client.TfsTeamProjectCollection -ArgumentList ([uri]$Collection), $cred
			}

			if (-not [string]::IsNullOrWhiteSpace($Collection))
			{
				$configServer = Get-TfsConfigurationServer $Server -Credential $cred
				$filter = [Guid[]] @([Microsoft.TeamFoundation.Framework.Common.CatalogResourceTypes]::ProjectCollection)
				$collections = $configServer.CatalogNode.QueryChildren($filter, $false, [Microsoft.TeamFoundation.Framework.Common.CatalogQueryOptions]::IncludeParents) 
				$collections = $collections | Select-Object -ExpandProperty Resource | Where-Object DisplayName -like $Name

				if ($collections.Count -eq 0)
				{
					throw "Invalid or non-existent Team Project Collection(s): $Name"
				}

				foreach($tpc in $collections)
				{
					$collectionId = $tpc.Properties["InstanceId"]
					Write-Output $configServer.GetTeamProjectCollection($collectionId)
				}
			}
		}

		if ($null -eq $Collection)
		{
			if ($Global:TfsTpcConnection)
			{
				return $Global:TfsTpcConnection
			}
		}

		throw "No TFS connection information available. Either supply a valid -Collection argument or use Connect-TfsTeamProjectCollection prior to invoking this cmdlet."
	}
}
