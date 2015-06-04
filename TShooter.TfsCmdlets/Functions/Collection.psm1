Function Connect-TfsTeamProjectCollection
{
	[CmdletBinding()]
	[OutputType([Microsoft.TeamFoundation.Client.TfsTeamProjectCollection])]
	Param
	(
		[Parameter(Mandatory=$true, Position=0)]
		[object] 
		$Collection,
	
		[Parameter(ValueFromPipeline=$true)]
		[object] 
		$Server,
	
		[Parameter()]
		[System.Management.Automation.Credential()]
		[System.Management.Automation.PSCredential]
		$Credential
	)

	Process
	{
		$tpc = (Get-TfsTeamProjectCollection @PSBoundParameters | Select -First 1)
		$tpc.EnsureAuthenticated()

		$Global:TfsTpcConnection = $tpc
		$Global:TfsTpcConnectionCredential = $Credential

		return $tpc
	}
}

Function Disconnect-TfsTeamProjectCollection
{
	Process
	{
		Remove-Variable -Name TfsTpcConnection -Scope Global
		Remove-Variable -Name TfsTpcConnectionUrl -Scope Global
		Remove-Variable -Name TfsTpcConnectionCredential -Scope Global
		Remove-Variable -Name TfsTpcConnectionUseDefaultCredentials -Scope Global
	}
}

Function Get-TfsTeamProjectCollection
{
	[CmdletBinding()]
	[OutputType([Microsoft.TeamFoundation.Client.TfsTeamProjectCollection])]
	Param
	(
		[Parameter(Position=0)]
		[object] 
		$Collection = "*",
	
		[Parameter(ValueFromPipeline=$true)]
		[object] 
		$Server,
	
		[Parameter()]
		[System.Management.Automation.Credential()]
		[System.Management.Automation.PSCredential]
		$Credential
	)

	Process
	{
		if ($Collection -is [Microsoft.TeamFoundation.Client.TfsTeamProjectCollection])
		{
			return $Collection
		}

		if ($Collection -is [Uri])
		{
			return _GetCollectionFromUrl $Collection $Credential
		}

		if ($Collection -is [string])
		{
			if ([Uri]::IsWellFormedUriString($Collection, [UriKind]::Absolute))
			{
				return _GetCollectionFromUrl ([Uri] $Collection) $Server $Credential
			}

			if (-not [string]::IsNullOrWhiteSpace($Collection))
			{
				return _GetCollectionFromName $Collection $Server $Credential
			}

			$Collection = $null
		}

		if ($Collection -eq $null)
		{
			if ($Global:TfsTpcConnection)
			{
				return $Global:TfsTpcConnection
			}
		}

		throw "No TFS connection information available. Either supply a valid -Collection argument or use Connect-TfsTeamProjectCollection prior to invoking this cmdlet."
	}
}

Function Get-TfsRegisteredTeamProjectCollection
{
	[CmdletBinding()]
	[OutputType([Microsoft.TeamFoundation.Client.RegisteredProjectCollection[]])]
	Param
	(
		[Parameter(Position=0, ValueFromPipeline=$true)]
		[string]
		$Name = "*"
	)

	Process
	{
		return [Microsoft.TeamFoundation.Client.RegisteredTfsConnections]::GetProjectCollections() | ? DisplayName -Like $Name
	}
}

# =================
# Helper Functions
# =================

Function _GetCollectionFromUrl
{
	Param ($Url, $Cred)
	
	if ($Cred)
	{
		$tpc = [Microsoft.TeamFoundation.Client.TfsTeamProjectCollectionFactory]::GetTfsTeamProjectCollection([Uri] $Url, (_GetCredential $cred))
	}
	else
	{
		$tpc = [Microsoft.TeamFoundation.Client.TfsTeamProjectCollectionFactory]::GetTfsTeamProjectCollection([Uri] $Url)
	}

	return $tpc
}


Function _GetCollectionFromName
{
	Param
	(
		$Name, $Server, $Cred
	)
	Process
	{
		if (-not $Server)
		{
			$registeredCollections = Get-TfsRegisteredTeamProjectCollection $Name

			foreach ($registeredTpc in $registeredCollections)
			{
				if ($Cred)
				{
					$tpc = New-Object Microsoft.TeamFoundation.Client.TfsTeamProjectCollection -ArgumentList $registeredTpc.Uri, ([System.Net.NetworkCredential] $cred)
				}
				else
				{
					$tpc = [Microsoft.TeamFoundation.Client.TfsTeamProjectCollectionFactory]::GetTeamProjectCollection($registeredTpc)
				}

				$tpc.EnsureAuthenticated()
				$tpc
			}

			return
		}
		
		$configServer = Get-TfsConfigurationServer $Server -Credential $Cred
		$filter = [Guid[]] @([Microsoft.TeamFoundation.Framework.Common.CatalogResourceTypes]::ProjectCollection)
		
		$collections = $configServer.CatalogNode.QueryChildren($filter, $false, [Microsoft.TeamFoundation.Framework.Common.CatalogQueryOptions]::IncludeParents) 
		$collections = $collections | Select -ExpandProperty Resource | ? DisplayName -like $Name

		if ($collections.Count -eq 0)
		{
			throw "Invalid or non-existent Team Project Collection(s): $Name"
		}

		foreach($tpc in $collections)
		{
			$collectionId = $tpc.Properties["InstanceId"]
			$tpc = $configServer.GetTeamProjectCollection($collectionId)
			$tpc.EnsureAuthenticated()

			$tpc
		}

	}
}

Function _GetCredential
{
	Param ($Cred)

	if ($Cred)
	{
		return [System.Net.NetworkCredential] $Cred
	}
	
	return [System.Net.CredentialCache]::DefaultNetworkCredentials
}