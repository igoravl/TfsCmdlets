<#
.SYNOPSIS
Creates a new team project collection.

.PARAMETER Credential
${HelpParam_Credential}

.INPUTS
System.String
#>
Function New-TfsTeamProjectCollection
{
	[CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
	[OutputType([Microsoft.TeamFoundation.Client.TfsTeamProjectCollection])]
	Param
	(
		[Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
		[string]
		$Name,

		[Parameter()]
		[string]
		$Description,

		[Parameter(ParameterSetName="Use database server", Mandatory=$true)]
		[string]
		$DatabaseServer,

		[Parameter(ParameterSetName="Use database server")]
		[string]
		$DatabaseName,

		[Parameter(ParameterSetName="Use connection string", Mandatory=$true)]
		[string]
		$ConnectionString,

		[Parameter()]
		[switch]
		$Default,

		[Parameter()]
		[switch]
		$UseExistingDatabase,

		[Parameter()]
		[ValidateSet("Started", "Stopped")]
		[string]
		$InitialState = "Started",

		[Parameter()]
		[int]
		$PollingInterval = 5,

		[Parameter()]
		[timespan]
		$Timeout = [timespan]::MaxValue,

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
		if($PSCmdlet.ShouldProcess($Name, 'Create team project collection'))
		{
			$configServer = Get-TfsConfigurationServer $Server -Credential $Credential
			$tpcService = $configServer.GetService([type] 'Microsoft.TeamFoundation.Framework.Client.ITeamProjectCollectionService')
			$servicingTokens = New-Object 'System.Collections.Generic.Dictionary[string,string]'
			$servicingTokens["SharePointAction"] = "None"
			$servicingTokens["ReportingAction"] = "None"

			if ($DatabaseName)
			{
				$servicingTokens["CollectionDatabaseName"] = $DatabaseName
			}

			if ($UseExistingDatabase)
			{
				$servicingTokens["UseExistingDatabase"] = $UseExistingDatabase.ToBool()
			}

			if ($PSCmdlet.ParameterSetName -eq "Use database server")
			{
				$ConnectionString = "Data source=$DatabaseServer; Integrated Security=true"
			}

			try
			{
				Write-Progress -Id 1 -Activity "Create team project collection" -Status "Creating team project collection $Name" -PercentComplete 0

				$start = Get-Date

				$tpcJob = $tpcService.QueueCreateCollection(
					$Name,
					$Description, 
					$Default.ToBool(),
					"~/$Name/",
					[Microsoft.TeamFoundation.Framework.Common.TeamFoundationServiceHostStatus] $InitialState,
					$servicingTokens,
					$ConnectionString,
					$null,  # Default connection string
					$null)  # Default category connection strings

				while((Get-Date).Subtract($start) -le $Timeout)
				{
					Start-Sleep -Seconds $PollingInterval

					$collectionInfo = $tpcService.GetCollection($tpcJob.HostId, [Microsoft.TeamFoundation.Framework.Client.ServiceHostFilterFlags]::IncludeAllServicingDetails)
					$jobDetail = $collectionInfo.ServicingDetails | Where-Object JobId -eq $tpcJob.JobId

					if (($null -eq $jobDetail) -or 
						(($jobDetail.JobStatus -ne [Microsoft.TeamFoundation.Framework.Client.ServicingJobStatus]::Queued) -and 
						($jobDetail.JobStatus -ne [Microsoft.TeamFoundation.Framework.Client.ServicingJobStatus]::Running)))
					{
						if ($jobDetail.Result -eq [Microsoft.TeamFoundation.Framework.Client.ServicingJobResult]::Failed -or 
							$jobDetail.JobStatus -eq [Microsoft.TeamFoundation.Framework.Client.ServicingJobStatus]::Failed)
						{
							throw "Error creating team project collection $Name : "
						}
					
						$tpc = Get-TfsTeamProjectCollection -Server $Server -Credential $Credential -Collection $Name

						if ($Passthru)
						{
							return $tpc
						}
					}
				}
			}
			finally
			{
					Write-Progress -Id 1 -Activity "Create team project collection" -Completed
			}

			throw (New-Object 'System.TimeoutException' -ArgumentList "Operation timed out during creation of team project collection $Name")
		}
	}
}
