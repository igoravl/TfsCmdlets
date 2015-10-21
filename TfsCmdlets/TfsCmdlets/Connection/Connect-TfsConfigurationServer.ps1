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
		$Credential = [System.Management.Automation.PSCredential]::Empty,

		[Parameter()]
		[switch]
		$Passthru
	)

	Process
	{
		$configServer = Get-TfsConfigurationServer -Server $Server -Credential $Credential

		if (-not $configServer)
		{
			throw "Error connecting to TFS"
		}

		$Global:TfsServerConnection = $configServer

		if ($Passthru)
		{
			return $configServer
		}
	}
}
