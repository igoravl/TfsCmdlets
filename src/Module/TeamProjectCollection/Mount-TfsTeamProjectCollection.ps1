<#
.SYNOPSIS
Attaches a team project collection database to a Team Foundation Server installation.

.PARAMETER Credential
${HelpParam_Credential}

.INPUTS
Microsoft.TeamFoundation.Client.TfsConfigurationServer
System.String
System.Uri
#>
Function Mount-TfsTeamProjectCollection
{
    [CmdletBinding(ConfirmImpact='Medium')]
	Param
	(
		[Parameter(Mandatory=$true, Position=0)]
		[string]
		$Name,

		[Parameter()]
		[string]
		$Description,

		[Parameter(ParameterSetName="Use database server", Mandatory=$true)]
		[string]
		$DatabaseServer,

		[Parameter(ParameterSetName="Use database server", Mandatory=$true)]
		[string]
		$DatabaseName,

		[Parameter(ParameterSetName="Use connection string", Mandatory=$true)]
		[string]
		$ConnectionString,

		[Parameter()]
		[ValidateSet("Started", "Stopped")]
		[string]
		$InitialState = "Started",

		[Parameter()]
		[switch]
		$Clone,

		[Parameter()]
		[int]
		$PollingInterval = 5,

		[Parameter()]
		[timespan]
		$Timeout = [timespan]::MaxValue,

		[Parameter(ValueFromPipeline=$true)]
		[object] 
		$Server,
	
		[Parameter()]
		[System.Management.Automation.Credential()]
		[System.Management.Automation.PSCredential]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)
	Process
	{
		$configServer = Get-TfsConfigurationServer $Server -Credential $Credential
		$tpcService = $configServer.GetService([type] 'Microsoft.TeamFoundation.Framework.Client.ITeamProjectCollectionService')

		$servicingTokens = New-Object 'System.Collections.Generic.Dictionary[string,string]'

		if ($DatabaseName)
		{
			$servicingTokens["CollectionDatabaseName"] = $DatabaseName
		}

		if ($PSCmdlet.ParameterSetName -eq "Use database server")
		{
			$ConnectionString = "Data source=$DatabaseServer; Integrated Security=true; Initial Catalog=$DatabaseName"
		}

		try
		{
			Write-Progress -Id 1 -Activity "Attach team project collection" -Status "Attaching team project collection $Name" -PercentComplete 0

			#$start = Get-Date

			# string databaseConnectionString, IDictionary<string, string> servicingTokens, bool cloneCollection, string name, string description, string virtualDirectory)

			$tpcJob = $tpcService.QueueAttachCollection(
				$ConnectionString,
				$servicingTokens, 
				$Clone.ToBool(),
				$Name,
				$Description,
				"~/$Name/")

			[void] $tpcService.WaitForCollectionServicingToComplete($tpcJob, $Timeout)

			return Get-TfsTeamProjectCollection -Server $Server -Credential $Credential -Collection $Name
		}
		finally
		{
			Write-Progress -Id 1 -Activity "Attach team project collection" -Completed
		}

		throw (New-Object 'System.TimeoutException' -ArgumentList "Operation timed out during creation of team project collection $Name")
	}
}
