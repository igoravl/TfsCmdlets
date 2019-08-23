<#
.SYNOPSIS
Gets information about a configuration server.

.PARAMETER Server
HELP_PARAM_SERVER

.PARAMETER Current
Returns the configuration server specified in the last call to Connect-TfsConfigurationServer (i.e. the "current" configuration server)

.PARAMETER Credential
HELP_PARAM_TFSCREDENTIAL

.INPUTS
Microsoft.TeamFoundation.Client.TfsConfigurationServer
System.String
System.Uri
#>
Function Start-TfsIdentitySync
{
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidUsingPlainTextForPassword', '')]
	[CmdletBinding()]
	Param
	(
		[Parameter(Position=0,ValueFromPipeline=$true)]
		[object] 
		$Server,

		[Parameter()]
		[switch]
		$Wait,

		[Parameter()]
		[object]
		$Credential
	)

	Process
	{
		$srv = Get-TfsConfigurationServer -Server $Server -Credential $Credential

		if($srv.Count -ne 1)
		{
			throw "Invalid or non-existent configuration server $Server"
		}

		$jobSvc = $srv.GetService([type]'Microsoft.TeamFoundation.Framework.Client.ITeamFoundationJobService')
		$syncJobId = [guid]'544dd581-f72a-45a9-8de0-8cd3a5f29dfe'
		$syncJobDef = $jobSvc.QueryJobs() | Where-Object { $_.JobId -eq $syncJobId }

		if ($syncJobDef)
		{
			_Log "Queuing job '$($syncJobDef.Name)' with high priority now"

			$success = ([bool] $jobSvc.QueueJobNow($syncJobDef, $true))

			if (-not $success)
			{
				throw "Failed to queue synchronization job"
			}

			if($Wait.IsPresent)
			{
				do
				{
					_Log "Waiting for the job to complete"
					Start-Sleep -Seconds 5

					$status = $jobSvc.QueryLatestJobHistory($syncJobId)
					_Log "Current job status: $($status.Result)"
				} while($status.Result -eq 'None')

				return $result
			}
		}
		else
		{
			throw "Could not find Periodic Identity Synchronization job definition (id $syncJobId). Unable to start synchronization process."
		}
	}
}
