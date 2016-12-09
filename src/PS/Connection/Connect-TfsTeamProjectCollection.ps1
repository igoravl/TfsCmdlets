<#

.SYNOPSIS
    Connects to a team project collection. 

.DESCRIPTION
	The Connect-TfsTeamProjectCollection cmdlet "connects" (initializes a Microsoft.TeamFoundation.Client.TfsTeamProjectCollection object) to a TFS Team Project Collection. That connection is subsequently kept in a global variable to be later reused until it's closed by a call to Disconnect-TfsTeamProjectCollection.
	Most cmdlets in the TfsCmdlets module require a TfsTeamProjectCollection object to be provided via their -Collection argument in order to access a TFS instance. Those cmdlets will use the connection opened by Connect-TfsTeamProjectCollection as their "default connection". In other words, TFS cmdlets (e.g. New-TfsWorkItem) that have a -Collection argument will use the connection provided by Connect-TfsTeamProjectCollection by default.

.PARAMETER Collection
	Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 
    Finally, if a TfsTeamProjectCollection object is provided via this argument, it will be used as the new default connection. This may be especially useful if you e.g. received a pre-initialized connection to a TFS collection via a call to an external library or API.
	For more details, see the -Collection argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Server
	Specifies either a URL or the name of the Team Foundation Server configuration server (the "root" of a TFS installation) to connect to, or a previously initialized Microsoft.TeamFoundation.Client.TfsConfigurationServer object.
	For more details, see the -Server argument in the Get-TfsConfigurationServer cmdlet.

.PARAMETER Credential
    ${HelpParam_Credential}

.PARAMETER Passthru
    ${HelpParam_Passthru}

.EXAMPLE
	Connect-TfsTeamProjectCollection -Collection http://tfs:8080/tfs/DefaultCollection
	Connects to a collection called "DefaultCollection" in a TF server called "tfs" using the default credentials of the logged-on user

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
	[CmdletBinding()]
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
	
		[Parameter()]
		[System.Management.Automation.Credential()]
		[System.Management.Automation.PSCredential]
		$Credential = [System.Management.Automation.PSCredential]::Empty,

		[Parameter()]
		[switch]
		$Passthru
	)

	Process
	{
		$tpc = $null

		try
		{
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
