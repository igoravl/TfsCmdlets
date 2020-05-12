#define ITEM_TYPE Microsoft.VisualStudio.Services.WebApi.VssConnection
<#
.SYNOPSIS
Connects to a team project collection. 

.DESCRIPTION
The Connect-TfsTeamProjectCollection cmdlet "connects" (initializes a Microsoft.TeamFoundation.Client.TfsTeamProjectCollection object) to a TFS Team Project Collection. That connection is subsequently kept in a global variable to be later reused until it's closed by a call to Disconnect-TfsTeamProjectCollection.
Most cmdlets in the TfsCmdlets module require a TfsTeamProjectCollection object to be provided via their -Collection argument in order to access a TFS instance. Those cmdlets will use the connection opened by Connect-TfsTeamProjectCollection as their "default connection". In other words, TFS cmdlets (e.g. New-TfsWorkItem) that have a -Collection argument will use the connection provided by Connect-TfsTeamProjectCollection by default.

.PARAMETER Collection
HELP_PARAM_COLLECTION

.PARAMETER Server
HELP_PARAM_SERVER

.PARAMETER Credential
HELP_PARAM_TFSCREDENTIAL

.PARAMETER Interactive
HELP_PARAM_INTERACTIVE

.PARAMETER Passthru
HELP_PARAM_PASSTHRU

.EXAMPLE
Connect-TfsTeamProjectCollection -Collection http://tfs:8080/tfs/DefaultCollection
Connects to a collection called "DefaultCollection" in a TF server called "tfs" using the cached credentials of the logged-on user

.EXAMPLE
Connect-TfsTeamProjectCollection -Collection http://tfs:8080/tfs/DefaultCollection -Interactive
Connects to a collection called "DefaultCollection" in a Team Foundation server called "tfs", firstly prompting the user for credentials (it ignores the cached credentials for the currently logged-in user). It's equivalent to the command:

PS> Connect-TfsTeamProjectCollection -Collection http://tfs:8080/tfs/DefaultCollection -Credential (Get-TfsCredential -Interactive)

.LINK
Get-TfsTeamProjectCollection

.LINK
https://msdn.microsoft.com/en-us/library/microsoft.teamfoundation.client.tfsteamprojectcollection.aspx

.INPUTS
Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
System.String
System.Uri
#>
Function Connect-TfsTeamProjectCollection
{
	[CmdletBinding(DefaultParameterSetName="Cached credentials")]
	[OutputType('ITEM_TYPE')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidGlobalVars', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidUsingPlainTextForPassword', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUseDeclaredVarsMoreThanAssignments', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUsePSCredentialType', '')]
	Param
	(
		[Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
		[ValidateNotNull()]
		[object] 
		$Collection,
	
        [Parameter(ParameterSetName="Cached credentials")]
        [switch]
        $Cached,

        [Parameter(ParameterSetName="User name and password", Mandatory=$true, Position=1)]
        [string]
        $UserName,

        [Parameter(ParameterSetName="User name and password", Position=2)]
        [securestring]
        $Password,

		[Parameter(ParameterSetName="Credential object", Mandatory=$true)]
		[ValidateNotNull()]
		[object]
        $Credential,

        [Parameter(ParameterSetName="Personal Access Token", Mandatory=$true)]
        [Alias('Pat')]
        [string]
        $PersonalAccessToken,

        [Parameter(ParameterSetName="Prompt for credential", Mandatory=$true)]
        [switch]
        $Interactive,

		[Parameter()]
		[object] 
		$Server,
	
		[Parameter()]
		[switch]
		$Passthru
	)

	Begin
	{
		_LogParams
	}

	Process
	{
		$tpc = $null

		if ($Collection -is [ITEM_TYPE])
		{
			_Log "Collection argument is of type TfsTeamProjectCollection. Reusing object."
			$tpc = $Collection
		}
		elseif(($Collection -is [string]) -and (-not [uri]::IsWellFormedUriString($Collection, [UriKind]::Absolute)))
		{
			_Log "Converting collection name '$Collection' to URL 'https://dev.azure.com/$Collection'"
			$Collection = "https://dev.azure.com/$Collection"
		}
		else
		{
			_Log "Connecting with $($PSCmdlet.ParameterSetName)"

			$args = $PSBoundParameters
			
			[void] $args.Remove('Collection')
			[void] $args.Remove('Server')
			[void] $args.Remove('Passthru')

			$tpc = Get-TfsTeamProjectCollection -Collection $Collection -Server $Server -Credential (Get-TfsCredential @args)

			if (-not $tpc -or ($tpc.Count -ne 1))
			{
				throw "Invalid or non-existent team project collection $Collection"
			}

			_Log "Calling VssConnection.ConnectAsync()"
			CALL_ASYNC($tpc.ConnectAsync(), "Error connecting to team project collection / organization '$Collection'")
		}

		$srv = $tpc.ParentConnection

		[TfsCmdlets.CurrentConnections]::Reset()
		[TfsCmdlets.CurrentConnections]::Server = $srv
		[TfsCmdlets.CurrentConnections]::Collection = $tpc

		_Log "Adding $($tpc.Uri) to the MRU list"

		_SetMru 'Server' -Value ($srv.Uri)
		_SetMru 'Collection' -Value ($tpc.Uri)

		_Log "Connected to $($tpc.Uri)"

		if ($Passthru)
		{
			return $tpc
		}
	}
}

Set-Alias -Name ctfs -Value Connect-TfsTeamProjectCollection