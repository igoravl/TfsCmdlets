<#
.SYNOPSIS
    Gets information about a configuration server.

.PARAMETER Server
    ${HelpParam_Server}

.PARAMETER Current
    Returns the configuration server specified in the last call to Connect-TfsConfigurationServer (i.e. the "current" configuration server)

.PARAMETER Credential
    ${HelpParam_Credential}

.INPUTS
    Microsoft.TeamFoundation.Client.TfsConfigurationServer
    System.String
    System.Uri
#>
Function Get-TfsConfigurationServer
{
	[CmdletBinding(DefaultParameterSetName='Get by server')]
	[OutputType([Microsoft.TeamFoundation.Client.TfsConfigurationServer])]
	Param
	(
		[Parameter(Position=0, ParameterSetName='Get by server', Mandatory=$true, ValueFromPipeline=$true)]
        [AllowNull()]
		[object] 
		$Server,
	
		[Parameter(Position=0, ParameterSetName="Get current")]
        [switch]
        $Current,

		[Parameter(Position=1, ParameterSetName='Get by server')]
		[System.Management.Automation.Credential()]
		[System.Management.Automation.PSCredential]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)

	Process
	{
		if ($Current)
		{
			return $Global:TfsServerConnection
        }

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

# =================
# Helper Functions
# =================

Function _GetConfigServerFromUrl
{
	Param ($Url, $Cred)
	
	if ($Cred -ne [System.Management.Automation.PSCredential]::Empty)
	{
		return New-Object Microsoft.TeamFoundation.Client.TfsConfigurationServer -ArgumentList ([Uri] $Url), (_GetCredential $cred)
	}
	
	return [Microsoft.TeamFoundation.Client.TfsConfigurationServerFactory]::GetConfigurationServer([Uri] $Url)
}

Function _GetConfigServerFromName
{
	Param ($Name, $Cred)

	$Servers = Get-TfsRegisteredConfigurationServer $Name
	
	foreach($Server in $Servers)
	{
		if ($Cred -ne [System.Management.Automation.PSCredential]::Empty)
		{
			$configServer = New-Object Microsoft.TeamFoundation.Client.TfsConfigurationServer -ArgumentList $Server.Uri, (_GetCredential $cred)
		}
		else
		{
			$configServer = [Microsoft.TeamFoundation.Client.TfsConfigurationServerFactory]::GetConfigurationServer($Server)
		}

		$configServer.EnsureAuthenticated()
		$configServer
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
