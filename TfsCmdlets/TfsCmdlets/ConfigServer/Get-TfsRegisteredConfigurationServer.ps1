<#
#>
Function Get-TfsRegisteredConfigurationServer
{
	[CmdletBinding()]
	[OutputType([Microsoft.TeamFoundation.Client.RegisteredConfigurationServer[]])]
	Param
	(
		[Parameter(Position=0, ValueFromPipeline=$true)]
		[string]
		$Name = "*"
	)

	Process
	{
		if(($Name -eq "localhost") -or ($Name -eq "."))
		{
			$Name = $env:COMPUTERNAME
		}

		return [Microsoft.TeamFoundation.Client.RegisteredTfsConnections]::GetConfigurationServers() | ? Name -Like $Name
	}
}
