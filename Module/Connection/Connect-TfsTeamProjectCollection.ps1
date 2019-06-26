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
Connects to a collection called "DefaultCollection" in a TF server called "tfs" using the default credentials of the logged-on user

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
	[CmdletBinding(DefaultParameterSetName="Explicit credentials")]
	[OutputType('Microsoft.TeamFoundation.Client.TfsTeamProjectCollection')]
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
	
		[Parameter()]
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

	Begin
	{
		USE_PSCORE_ALTERNATIVE(Connect-AzDevOrganization)
		REQUIRES(Microsoft.TeamFoundation.Client)
	}

	Process
	{
		$tpc = $null

		if ($Collection -is [Microsoft.TeamFoundation.Client.TfsTeamProjectCollection])
		{
			_Log "Collection argument is of type TfsTeamProjectCollection. Reusing object."
			$tpc = $Collection
		}
		else
		{
			try
			{
				if ($Interactive.IsPresent)
				{
					_Log "Setting credential mode to Interactive Credential Prompt"
					$Credential = (Get-TfsCredential -Interactive)
				}
				elseif (-not $Credential)
				{
					_Log "Setting credential mode to Cached Credentials"
					$Credential = (Get-TfsCredential -Cached)
				}
				else
				{
					_Log "Setting credential mode to Explicit credentials. Credentials supplied are of type $($Credential.GetType().FullName)"
				}

				$tpc = (Get-TfsTeamProjectCollection -Collection $Collection -Server $Server -Credential $Credential | Select-Object -First 1)

				_Log "Calling TfsTeamProjectCollection.EnsureAuthenticated()"
				$tpc.EnsureAuthenticated()
			}
			catch
			{
				throw "Error connecting to team project collection $Collection ($_)"
			}
		}

		if (-not $tpc)
		{
			throw "Invalid or non-existent team project collection '$Collection'"
		}

		$script:TfsTeamConnection = $null
		$script:TfsProjectConnection = $null
		$script:TfsTpcConnection = $tpc
		$script:TfsServerConnection = $tpc.ConfigurationServer

		$script:AzDevTeamConnection = $null
		$script:AzDevProjectConnection = $null
		$script:AzDevOrganizationConnection = [Microsoft.VisualStudio.Services.WebApi.VssConnection]::new($tpc.Uri, $tpc.ClientCredentials)

		_Log "Connected to $($tpc.Uri)"

		if ($Passthru)
		{
			return $tpc
		}
	}
}
