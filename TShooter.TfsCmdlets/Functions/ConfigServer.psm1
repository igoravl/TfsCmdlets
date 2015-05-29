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
	param
	(
		[Parameter(ParameterSetName="Server from URL", ValueFromPipeline=$true, Mandatory=$true, Position=0)]
		[string]
		$Url,
	
		[Parameter(ParameterSetName="Server from URL")] 
		[System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
		$Credential,

		[Parameter(ParameterSetName="Preexisting connection")]
		[Microsoft.TeamFoundation.Client.TfsConfigurationServer] 
		$Server
	)

	Process
	{
		if ($Server)
		{
			$configServer = $Server
		}
		else
		{
			$configServer = Get-TfsConfigurationServer @PSBoundParameters
		}

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
	param
	(
		[Parameter(Position=0)]  
		[string]
		$Url,
	
		[Parameter()] 
		[System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
		$Credential
	)

	Process
	{
		if (-not $Url)
		{
			if ($Global:TfsServerConnection)
			{
				return $Global:TfsServerConnection
			}
			throw "No TFS connection information available. Either supply -Url argument or use Connect-TfsConfigurationServer prior to invoking this cmdlet."
		}
		
		if ($Credential)
		{
			$cred = $Credential.GetNetworkCredential()
		}
		else
		{
			$cred = [System.Net.CredentialCache]::DefaultNetworkCredentials
		}

		$configServer = _NewConfigServer $Url $cred

		return $configServer
	}
}

# =================
# Helper Functions
# =================

Function _NewConfigServer
{
	Param ($Url, $Cred)
	
	$configServer = New-Object Microsoft.TeamFoundation.Client.TfsConfigurationServer ([Uri] $Url), $cred
	[void] $configServer.EnsureAuthenticated()

	return $configServer
}