<#

.SYNOPSIS
    Connects to a team project.

.PARAMETER Credential
    ${HelpParam_TfsCredential}

.PARAMETER Interactive
	Prompts for user credentials. Can be used for both TFS and VSTS accounts - the proper login dialog is automatically selected. Should only be used in an interactive PowerShell session (i.e., a PowerShell terminal window), never in an unattended script (such as those executed during an automated build).

.PARAMETER Passthru
    ${HelpParam_Passthru}

#>
Function Connect-TfsTeamProject
{
	[CmdletBinding(DefaultParameterSetName="Explicit credentials")]
	[OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.Project])]
	Param
	(
		[Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
		[ValidateNotNull()]
		[object] 
		$Project,
	
		[Parameter()]
		[object] 
		$Collection,
	
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

		$tp = (Get-TfsTeamProject -Project $Project -Collection $Collection -Credential $Credential | Select -First 1)

		if (-not $tp)
		{
			throw "Error connecting to team project $Project"
		}

		$Global:TfsTeamConnection = $null
		$Global:TfsProjectConnection = $tp
		$Global:TfsTpcConnection = $tp.Store.TeamProjectCollection
		$Global:TfsServerConnection = $Global:TfsTpcConnection.ConfigurationServer

		if ($Passthru)
		{
			return $tp
		}
	}
}
