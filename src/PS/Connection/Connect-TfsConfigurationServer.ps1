<#
.SYNOPSIS
Connects to a configuration server.

.PARAMETER Credential
${HelpParam_TfsCredential}

.PARAMETER Passthru
${HelpParam_Passthru}

.DESCRIPTION
The Connect-TfsConfigurationServer function connects to a TFS configuration server. Functions that operate on a server level (as opposed to those operation on a team project collection level) will use by default a connection opened by this function.

.NOTES
A TFS Configuration Server represents the server that is running Team Foundation Server. On a database level, it is represented by the Tfs_Configuration database. Operations that should be performed on a server level (such as setting server-level permissions) require a connection to a TFS configuration server. Internally, this connection is represented by an instance of the Microsoft.TeamFoundation.Client.TfsConfigurationServer class and is kept in a PowerShell global variable caled TfsServerConnection.

.PARAMETER Interactive
Prompts for user credentials. Can be used for both TFS and VSTS accounts - the proper login dialog is automatically selected. Should only be used in an interactive PowerShell session (i.e., a PowerShell terminal window), never in an unattended script (such as those executed during an automated build).

.INPUTS
Microsoft.TeamFoundation.Client.TfsConfigurationServer
System.String
System.Uri
#>
Function Connect-TfsConfigurationServer
{
	[CmdletBinding(DefaultParameterSetName="Explicit credentials")]
	[OutputType([Microsoft.TeamFoundation.Client.TfsConfigurationServer])]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidGlobalVars', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidUsingPlainTextForPassword', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUseDeclaredVarsMoreThanAssignments', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUsePSCredentialType', '')]
	Param
	(
		[Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
		[ValidateNotNull()]
		[object] 
		$Server,
	
		[Parameter(ParameterSetName="Explicit credentials")]
		[object]
		$Credential,

		[Parameter(ParameterSetName="Prompt for credentials", Mandatory=$true)]
		[switch]
		$Interactive,

		[Parameter()]
		[switch]
		$Passthru
	)

	Process
	{
		if ($Interactive.IsPresent)
		{
			$Credential = (Get-TfsCredential -Interactive)
		}

		$configServer = Get-TfsConfigurationServer -Server $Server -Credential $Credential

		if (-not $configServer)
		{
			throw "Error connecting to TFS"
		}

		$Global:TfsTeamConnection = $null
		$Global:TfsProjectConnection = $null
		$Global:TfsTpcConnection = $null
		$Global:TfsServerConnection = $configServer

		if ($Passthru)
		{
			return $configServer
		}
	}
}
