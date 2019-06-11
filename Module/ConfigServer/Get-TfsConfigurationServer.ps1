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
Function Get-TfsConfigurationServer
{
	[CmdletBinding(DefaultParameterSetName='Get by server')]
	[OutputType('Microsoft.TeamFoundation.Client.TfsConfigurationServer')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidGlobalVars', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidUsingPlainTextForPassword', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUsePSCredentialType', '')]
	Param
	(
		[Parameter(Position=0, ParameterSetName='Get by server', Mandatory=$true, ValueFromPipeline=$true)]
        [AllowNull()]
		[object] 
		$Server,
	
		[Parameter(Position=0, ParameterSetName="Get current")]
        [switch]
        $Current,

		[Parameter(Position=1, ParameterSetName='Get by server')]
		[object]
		$Credential
	)

	Process
	{
		if ($Current)
		{
			return $script:TfsServerConnection
        }

		if ($Server -is [Microsoft.TeamFoundation.Client.TfsConfigurationServer])
		{
			return $Server
		}

		$cred = Get-TfsCredential -Credential $Credential

		if ($Server -is [Uri])
		{
			return New-Object Microsoft.TeamFoundation.Client.TfsConfigurationServer -ArgumentList ([Uri] $Server), $cred
		}

		if ($Server -is [string])
		{
			if ([Uri]::IsWellFormedUriString($Server, [UriKind]::Absolute))
			{
				return New-Object Microsoft.TeamFoundation.Client.TfsConfigurationServer -ArgumentList ([Uri] $Server), $cred
			}

			if (-not [string]::IsNullOrWhiteSpace($Server))
			{
				$serverNames = Get-TfsRegisteredConfigurationServer $Name
				
				foreach($s in $serverNames)
				{
					New-Object Microsoft.TeamFoundation.Client.TfsConfigurationServer -ArgumentList $s.Uri,  $cred
				}

				return
			}
		}

		if ($null -eq $Server)
		{
			if ($script:TfsServerConnection)
			{
				return $script:TfsServerConnection
			}
		}

		throw "No TFS connection information available. Either supply a valid -Server argument or use Connect-TfsConfigurationServer prior to invoking this cmdlet."
	}
}
