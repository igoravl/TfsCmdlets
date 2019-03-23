<#
.SYNOPSIS
Connects to a team project.

.DESCRIPTION
The Connect-TfsTeamProject cmdlet "connects" (initializes a Microsoft.TeamFoundation.WorkItemTracking.Client.Project object) to a TFS Team Project. That connection is subsequently kept in a global variable to be later reused until it's closed by a call to Disconnect-TfsTeamProject.
Cmdlets in the TfsCmdlets module that require a team project object to be provided via their -Project argument in order to access a TFS project will use the connection opened by this cmdlet as their "default project". In other words, TFS cmdlets (e.g. New-TfsArea) that have a -Project argument will use the connection provided by Connect-TfsTeamProject by default.

.PARAMETER Project
${HelpParam_Project}

.PARAMETER Collection
${HelpParam_Collection}

.PARAMETER Credential
${HelpParam_TfsCredential}

.PARAMETER Interactive
${HelpParam_Interactive}

.PARAMETER Passthru
${HelpParam_Passthru}

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
	[OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.Project])]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidGlobalVars', '')]
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
		if ($Interactive.IsPresent)
		{
			$Credential = (Get-TfsCredential -Interactive)
		}

		$tp = (Get-TfsTeamProject -Project $Project -Collection $Collection -Credential $Credential | Select-Object -First 1)

		if (-not $tp)
		{
			throw "Error connecting to team project $Project"
		}

		$script:TfsTeamConnection = $null
		$script:TfsProjectConnection = $tp
		$script:TfsTpcConnection = $tp.Store.TeamProjectCollection
		$script:TfsServerConnection = $script:TfsTpcConnection.ConfigurationServer

		if ($Passthru)
		{
			return $tp
		}
	}
}
