<#

.SYNOPSIS
    Connects to a team project collection. 

.DESCRIPTION
	The Connect-TfsTeamProjectCollection cmdlet "connects" (initializes a Microsoft.TeamFoundation.Client.TfsTeamProjectCollection object) to a TFS Team Project Collection. That connection is subsequently kept in a global variable to be later reused until it's closed by a call to Disconnect-TfsTeamProjectCollection.
	Most cmdlets in the TfsCmdlets module require a TfsTeamProjectCollection object to be provided via their -Collection argument in order to access a TFS instance. Those cmdlets will use the connection opened by Connect-TfsTeamProjectCollection as their "default connection". In other words, TFS cmdlets (e.g. New-TfsWorkItem) that have a -Collection argument will use the connection provided by Connect-TfsTeamProjectCollection by default.

.PARAMETER Collection
	Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object.
    When a TfsTeamProjectCollection object is provided via this argument, it will be used as the new default connection. This may be especially useful if you e.g. received a pre-initialized connection to a TFS collection via a call to an external library or API.
	For more details, see the -Collection argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Server
	Specifies either a URL or the name of the Team Foundation Server configuration server (the "root" of a TFS installation) to connect to, or a previously initialized Microsoft.TeamFoundation.Client.TfsConfigurationServer object.
	For more details, see the -Server argument in the Get-TfsConfigurationServer cmdlet.

.PARAMETER Credential
    ${HelpParam_TfsCredential}

.PARAMETER Interactive
	Prompts for user credentials. Can be used for both TFS and VSTS accounts - the proper login dialog is automatically selected. Should only be used in an interactive PowerShell session (i.e., a PowerShell terminal window), never in an unattended script (such as those executed during an automated build).
	
.PARAMETER Passthru
    ${HelpParam_Passthru}

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
	[OutputType([Microsoft.TeamFoundation.Client.TfsTeamProjectCollection])]
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

	Process
	{
		$tpc = $null

		try
		{
			if ($Interactive.IsPresent)
			{
				$Credential = (Get-TfsCredential -Interactive)
			}

			$tpc = (Get-TfsTeamProjectCollection -Collection $Collection -Server $Server -Credential $Credential | Select -First 1)
			$tpc.EnsureAuthenticated()
		}
		catch
		{
			throw "Error connecting to team project collection $Collection ($_)"
		}

		$Global:TfsTeamConnection = $null
		$Global:TfsProjectConnection = $null
		$Global:TfsTpcConnection = $tpc
		$Global:TfsServerConnection = $tpc.ConfigurationServer

		if ($Passthru)
		{
			return $tpc
		}
	}
}
