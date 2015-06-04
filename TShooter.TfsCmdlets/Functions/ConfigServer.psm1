<#
.SYNOPSIS

Connects to a Team Foundation Server configuration server.

.DESCRIPTION

The Connect-TfsConfigurationServer function connects to a TFS configuration server. Functions that operate on a server level (as opposed to those operation on a team project collection level) will use by default a connection opened by this function.

.NOTES

A TFS Configuration Server represents the server that is running Team Foundation Server. On a database level, it is represented by the Tfs_Configuration database. 

Operations that should be performed on a server level (such as setting server-level permissions) require a connection to a TFS configuration server. 

Internally, this connection is represented by an instance of the Microsoft.TeamFoundation.Client.TfsConfigurationServer class and is kept in a PowerShell global variable caled TfsServerConnection .

.LINK
https://msdn.microsoft.com/en-us/library/vstudio/microsoft.teamfoundation.client.tfsconfigurationserver(v=vs.120).aspx

#>
Function Connect-TfsConfigurationServer
{
	[CmdletBinding()]
	[OutputType([Microsoft.TeamFoundation.Client.TfsConfigurationServer])]
	Param
	(
		[Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
		[object] 
		$Server,
	
		[Parameter(Position=1)]
		[System.Management.Automation.Credential()]
		[System.Management.Automation.PSCredential]
		$Credential
	)

	Process
	{
		$configServer = (Get-TfsConfigurationServer @PSBoundParameters | Select -First 1)

		$Global:TfsServerConnection = $configServer
		$Global:TfsServerConnectionCredential = $Credential

		return $configServer
	}
}

Function Disconnect-TfsConfigurationServer
{
	Process
	{
		Remove-Variable -Name TfsServerConnection -Scope Global
		Remove-Variable -Name TfsServerConnectionUrl -Scope Global
		Remove-Variable -Name TfsServerConnectionCredential -Scope Global
		Remove-Variable -Name TfsServerConnectionUseDefaultCredentials -Scope Global
	}
}

Function Get-TfsConfigurationServer
{
	[CmdletBinding()]
	[OutputType([Microsoft.TeamFoundation.Client.TfsConfigurationServer])]
	Param
	(
		[Parameter(Position=0)]
		[object] 
		$Server,
	
		[Parameter(Position=1)]
		[System.Management.Automation.Credential()]
		[System.Management.Automation.PSCredential]
		$Credential
	)

	Process
	{
		if ($Server -is [Microsoft.TeamFoundation.Client.TfsConfigurationServer])
		{
			return $Server
		}

		if ($Server -is [Uri])
		{
			return _GetConfigServerFromUrl $Server $Credential
		}

		if ($Server -is [string])
		{
			if ([Uri]::IsWellFormedUriString($Server, [UriKind]::Absolute))
			{
				return _GetConfigServerFromUrl ([Uri] $Server) $Credential
			}

			if (-not [string]::IsNullOrWhiteSpace($Server))
			{
				return _GetConfigServerFromName $Server $Credential
			}

			$Server = $null
		}

		if ($Server -eq $null)
		{
			if ($Global:TfsServerConnection)
			{
				return $Global:TfsServerConnection
			}
		}

		throw "No TFS connection information available. Either supply a valid -Server argument or use Connect-TfsConfigurationServer prior to invoking this cmdlet."
	}
}

Function Get-TfsRegisteredConfigurationServer
{
	[CmdletBinding()]
	[OutputType([Microsoft.TeamFoundation.Client.RegisteredConfigurationServer[]])]
	Param
	(
		[Parameter(Position=0, ValueFromPipeline=$true)]
		[string]
		$Name = "*"
	)

	Process
	{
		if(($Name -eq "localhost") -or ($Name -eq "."))
		{
			$Name = $env:COMPUTERNAME
		}

		return [Microsoft.TeamFoundation.Client.RegisteredTfsConnections]::GetConfigurationServers() | ? Name -Like $Name
	}
}

# =================
# Helper Functions
# =================

Function _GetConfigServerFromUrl
{
	Param ($Url, $Cred)
	
	if ($Cred)
	{
		$configServer = New-Object Microsoft.TeamFoundation.Client.TfsConfigurationServer -ArgumentList ([Uri] $Url), ([System.Net.NetworkCredential] $cred)
	}
	else
	{
		$configServer = [Microsoft.TeamFoundation.Client.TfsConfigurationServerFactory]::GetConfigurationServer([Uri] $Url)
	}


	$configServer.EnsureAuthenticated()
	return $configServer
}

Function _GetConfigServerFromName
{
	Param ($Name, $Cred)

	$Servers = Get-TfsRegisteredConfigurationServer $Name
	
	foreach($Server in $Servers)
	{
		if ($Cred)
		{
			$configServer = New-Object Microsoft.TeamFoundation.Client.TfsConfigurationServer -ArgumentList ($Server.Uri), ([System.Net.NetworkCredential] $cred)
		}
		else
		{
			$configServer = [Microsoft.TeamFoundation.Client.TfsConfigurationServerFactory]::GetConfigurationServer($Server)
		}

		$configServer.EnsureAuthenticated()
		$configServer
	}
}
