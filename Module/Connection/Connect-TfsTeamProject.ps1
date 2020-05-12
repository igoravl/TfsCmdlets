#define ITEM_TYPE Microsoft.TeamFoundation.Core.WebApi.TeamProject
<#
.SYNOPSIS
Connects to a team project.

.DESCRIPTION
The Connect-TfsTeamProject cmdlet "connects" (initializes a Microsoft.TeamFoundation.WorkItemTracking.Client.Project object) to a TFS Team Project. That connection is subsequently kept in a global variable to be later reused until it's closed by a call to Disconnect-TfsTeamProject.
Cmdlets in the TfsCmdlets module that require a team project object to be provided via their -Project argument in order to access a TFS project will use the connection opened by this cmdlet as their "default project". In other words, TFS cmdlets (e.g. New-TfsArea) that have a -Project argument will use the connection provided by Connect-TfsTeamProject by default.

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
Microsoft.TeamFoundation.WorkItemTracking.Client.Project
System.String

.EXAMPLE
Connect-TfsTeamProject -Project FabrikamFiber
Connects to a project called FabrikamFiber in the current team project collection (as specified in a previous call to Connect-TfsTeamProjectCollection)

.EXAMPLE
Connect-TfsTeamProject -Project FabrikamFiber -Collection http://vsalm:8080/tfs/FabrikamFiberCollection
Connects to a project called FabrikamFiber in the team project collection specified in the given URL
#>
Function Connect-TfsTeamProject
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
		$tpcArgs = $PSBoundParameters
		[void] $tpcArgs.Remove('Project')

		$tpc = Get-TfsTeamProjectCollection @tpcArgs

		$tp = (Get-TfsTeamProject -Project $Project -Collection $tpc | Select-Object -First 1)

		if (-not $tp)
		{
			throw "Error connecting to team project $Project"
		}

		$srv = $tpc.ConfigurationServer

		[TfsCmdlets.CurrentConnections]::Reset()
		[TfsCmdlets.CurrentConnections]::Server = $srv
		[TfsCmdlets.CurrentConnections]::Collection = $tpc
		[TfsCmdlets.CurrentConnections]::Project = $tp

		_Log "Adding '$($tp.Name)' to the MRU list"

		_SetMru 'Server' -Value ($srv.Uri)
		_SetMru 'Collection' -Value ($tpc.Uri)
		_SetMru 'Project' -Value ($tp.Name)

		_Log "Connected to $($tp.Name)"

		if ($Passthru)
		{
			return $tp
		}
	}
}

Set-Alias -Name ctfstp -Value Connect-TfsTeamProject