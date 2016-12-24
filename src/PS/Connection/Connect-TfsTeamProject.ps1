<#

.SYNOPSIS
    Connects to a team project.

.PARAMETER Credential
    ${HelpParam_TfsCredential}

.PARAMETER Passthru
    ${HelpParam_Passthru}

#>
Function Connect-TfsTeamProject
{
	[CmdletBinding()]
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
	
		[Parameter()]
		[object]
		$Credential,

		[Parameter()]
		[switch]
		$Passthru
	)

	Process
	{
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
