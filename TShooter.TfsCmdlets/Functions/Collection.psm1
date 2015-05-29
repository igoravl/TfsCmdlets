Function Connect-TfsTeamProjectCollection
{
	param
	(
		[Parameter(ParameterSetName="Collection from URL", ValueFromPipeline=$true, Mandatory=$true, Position=0)]
		[string]
		$Url,
	
		[Parameter(ParameterSetName="Collection from URL")]  
		[System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
		$Credential,

		[Parameter(ParameterSetName="Collection from Configuration Server", ValueFromPipeline=$true, Mandatory=$true)]  
		[string]
		$Name,

		[Parameter(ParameterSetName="Collection from configuration server")] 
		[Microsoft.TeamFoundation.Client.TfsConfigurationServer] 
		$Server,

		[Parameter(ParameterSetName="Preexisting connection")]
		[Microsoft.TeamFoundation.Client.TfsTeamProjectCollection] 
		$Collection
	)

	Process
	{
		if ($Collection)
		{
			$tpc = $Collection
		}
		else
		{
			$tpc = Get-TfsTeamProjectCollection @PSBoundParameters
		}

		$Global:TfsTpcConnection = $tpc
		$Global:TfsTpcConnectionCredential = $cred

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
	param
	(
		[Parameter(ParameterSetName="Ad-hoc connection", Mandatory=$true)]  
		[string]
		$Url,
	
		[Parameter(ParameterSetName="Ad-hoc connection")] 
		[System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
		$Credential,

		[Parameter(ParameterSetName="Collection from configuration server", Position=0)]  
		[string]
		$Name = "*",

		[Parameter(ParameterSetName="Collection from configuration server", ValueFromPipeline=$true)] 
		[Microsoft.TeamFoundation.Client.TfsConfigurationServer] 
		$Server,

		[Parameter(ParameterSetName="Current connection", Mandatory=$true)]
		[switch]
		$Current,

		[Parameter(ParameterSetName="Current connection",DontShow=$True)]
		[Microsoft.TeamFoundation.Client.TfsTeamProjectCollection]
		$Collection
	)

	Process
	{
		switch($PSCmdlet.ParameterSetName)
		{
			"Ad-hoc connection"
			{
				Write-Verbose "Get-TfsTeamProjectCollection: Ad-hoc connection using URL $Url"
				if ($Credential)
				{
					Write-Verbose "Get-TfsTeamProjectCollection: Credentials retrieved from \$Credential.GetNetworkCredential()"
					$cred = $Credential.GetNetworkCredential()
				}
				else
				{
					Write-Verbose "Get-TfsTeamProjectCollection: Credentials retrieved from System.Net.CredentialCache.DefaultNetworkCredentials"
					$cred = [System.Net.CredentialCache]::DefaultNetworkCredentials
				}
				$tpc = New-Object Microsoft.TeamFoundation.Client.TfsTeamProjectCollection ([Uri] $Url), $cred
				[void] $tpc.EnsureAuthenticated()
			}
			"Collection from configuration server"
			{
				Write-Verbose "Get-TfsTeamProjectCollection: Retrieving all collections with name like '$Name' from configuration server"
				$tpc = _GetCollectionByName $Name $Server
			}
			"Current Connection"
			{
				Write-Verbose "Get-TfsTeamProjectCollection: Retrieving currently connected connection"
				Write-Verbose "Get-TfsTeamProjectCollection: Global connection: $($Global:TfsTpcConnection); -Collection argument: $($Collection)"
				$tpc = _GetCollection $Collection
			}
		}

		return $tpc
	}
}

# =================
# Helper Functions
# =================

Function _GetCollection
{
	Param ($Collection)
	
	if ($Collection)
	{
		return $Collection
	}	

	if ($Global:TfsTpcConnection)
	{
		return $Global:TfsTpcConnection
	}

	throw "No TFS connection information available. Either supply -Collection argument or use Connect-TfsTeamProjectCollection prior to invoking this cmdlet."
}

Function _GetCollectionByName
{
	Param
	(
		$Name, $Server
	)
	Process
	{
		$configServer = _GetConfigServer $Server
		$filter = New-Object 'System.Collections.Generic.List[System.Guid]'
		[void] $filter.Add([Microsoft.TeamFoundation.Framework.Common.CatalogResourceTypes]::ProjectCollection)
		
		$collections = $configServer.CatalogNode.QueryChildren($filter, $false, [Microsoft.TeamFoundation.Framework.Common.CatalogQueryOptions]::IncludeParents) 
		$collections = $collections | Select -ExpandProperty Resource | ? -Property DisplayName -like $Name

		if ($collections.Count -eq 0)
		{
			throw "Invalid or non-existent Team Project Collection(s): $Name"
		}

		foreach($tpc in $collections)
		{
			$collectionId = $tpc.Properties["InstanceId"]
			return $configServer.GetTeamProjectCollection($collectionId)
		}

	}
}

Function _GetConfigServer
{
	Param ($Server)

	if ($Server)
	{
		return $Server
	}
	
	if ($Global:TfsServerConnection)
	{
		return $Global:TfsServerConnection
	}

	if ($Global:TfsTpcConnection)
	{
		return $Global:TfsTpcConnection.ConfigurationServer
	}

	throw "No TFS connection information available. Either supply -Server argument or use Connect-TfsConfigurationServer prior to invoking this cmdlet."
}
