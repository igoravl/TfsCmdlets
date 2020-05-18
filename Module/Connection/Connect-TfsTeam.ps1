#define ITEM_TYPE Microsoft.TeamFoundation.Core.WebApi.WebApiTeam
<#
.SYNOPSIS
Connects to a team.

.DESCRIPTION

.PARAMETER Project
HELP_PARAM_PROJECT

.PARAMETER Collection
HELP_PARAM_COLLECTION

.PARAMETER Credential
HELP_PARAM_TFSCREDENTIAL

.PARAMETER Interactive
HELP_PARAM_INTERACTIVE

.PARAMETER Passthru
HELP_PARAM_PASSTHRU

.INPUTS
ITEM_TYPE
System.String
#>
Function Connect-TfsTeam
{
	[CmdletBinding(DefaultParameterSetName="Explicit credentials")]
	[OutputType('ITEM_TYPE')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidUsingPlainTextForPassword', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUseDeclaredVarsMoreThanAssignments', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUsePSCredentialType', '')]
	Param
	(
		[Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
		[ValidateNotNull()]
		[object] 
		$Team,
	
		[Parameter()]
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

	Begin
	{
		_LogParams
        _Requires 'Microsoft.VisualStudio.Services.Common', 'Microsoft.VisualStudio.Services.Client.Interactive', 'Microsoft.TeamFoundation.Core.WebApi'
	}


	Process
	{
		if ($Interactive.IsPresent)
		{
			$Credential = (Get-TfsCredential -Interactive)
		}

        GET_TEAM($t,$tp,$tpc)

		[TfsCmdlets.CurrentConnections]::Reset()
		[TfsCmdlets.CurrentConnections]::Server = $srv
		[TfsCmdlets.CurrentConnections]::Collection = $tpc
		[TfsCmdlets.CurrentConnections]::Project = $tp
		[TfsCmdlets.CurrentConnections]::Team = $t

		_Log "Adding '$($t.Name)' to the MRU list"

		_SetMru 'Server' -Value ($srv.Uri)
		_SetMru 'Collection' -Value ($tpc.Uri)
		_SetMru 'Project' -Value ($tp.Name)
		_SetMru 'Team' -Value ($t.Name)

		_Log "Connected to '$($t.Name)'"

		if ($Passthru)
		{
			return $t
		}
	}
}

Set-Alias -Name ctfst -Value Connect-TfsTeam