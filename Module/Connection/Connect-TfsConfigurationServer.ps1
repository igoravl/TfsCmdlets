<#
.SYNOPSIS
Connects to a configuration server.

.PARAMETER Server
#HELP_PARAM_SERVER

.PARAMETER Credential
HELP_PARAM_TFSCREDENTIAL

.PARAMETER Interactive
HELP_PARAM_INTERACTIVE

.PARAMETER Passthru
HELP_PARAM_PASSTHRU

.DESCRIPTION
The Connect-TfsConfigurationServer function connects to a TFS configuration server. Functions that operate on a server level (as opposed to those operation on a team project collection level) will use by default a connection opened by this function.

.NOTES
A TFS Configuration Server represents the server that is running Team Foundation Server. On a database level, it is represented by the Tfs_Configuration database. Operations that should be performed on a server level (such as setting server-level permissions) require a connection to a TFS configuration server. Internally, this connection is represented by an instance of the Microsoft.TeamFoundation.Client.TfsConfigurationServer class and is kept in a PowerShell global variable caled TfsServerConnection.

.EXAMPLE
Connect-TfsConfigurationServer -Server http://vsalm:8080/tfs
Connects to the TFS server specified by the URL in the Server argument

.EXAMPLE
Connect-TfsConfigurationServer -Server vsalm
Connects to a previously registered TFS server by its user-defined name "vsalm". For more information, see Get-TfsRegisteredConfigurationServer

.INPUTS
Microsoft.TeamFoundation.Client.TfsConfigurationServer
System.String
System.Uri

.LINK
Microsoft.TeamFoundation.Client.TfsConfigurationServer

.LINK
https://blogs.msdn.microsoft.com/taylaf/2010/02/23/introducing-the-tfsconnection-tfsconfigurationserver-and-tfsteamprojectcollection-classes/
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

		$script:TfsTeamConnection = $null
		$script:TfsProjectConnection = $null
		$script:TfsTpcConnection = $null
		$script:TfsServerConnection = $configServer

		if ($Passthru)
		{
			return $configServer
		}
	}
}
