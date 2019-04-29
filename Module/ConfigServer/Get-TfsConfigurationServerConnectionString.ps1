<#
.SYNOPSIS
    Gets the configuration server database connection string.

.PARAMETER Computer
    Specifies the name of a Team Foundation Server application tier from which to retrieve the connection string

.PARAMETER Version
    Specifies the version of the Team Foundation Server being queried. Valid values are '12.0' (TFS 2013), '14.0' (TFS 2015), '15.0' (TFS 2017)

.PARAMETER Credential
    HELP_PARAM_CREDENTIAL
#>
Function Get-TfsConfigurationServerConnectionString
{
	[CmdletBinding()]
	[OutputType([string])]
	Param
	(
		[Parameter()]
		[string]
		[Alias('Session')]
		$Computer,

		[Parameter()]
		[ValidateSet('12.0', '14.0', '15.0')]
		[string]
		$Version,

		[Parameter()]
		[System.Management.Automation.Credential()]
		[System.Management.Automation.PSCredential]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)

	Process
	{

		$scriptBlock = New-ScriptBlock -EntryPoint '_GetConnectionString' -Dependency 'Get-InstallationPath', 'Test-RegistryValue', 'Get-RegistryValue'

		return Invoke-ScriptBlock -ScriptBlock $scriptBlock -Computer $Computer -Credential $Credential -ArgumentList $Version
	}
}

Function _GetConnectionString($Version)
{
	$path = Get-InstallationPath -Version $Version -Component ApplicationTier
	$webConfigPath = Join-Path $path 'Web Services\Web.config'
	$webConfig = [xml] (Get-Content $webConfigPath)

	return (Select-Xml -Xml $webConfig -XPath '/configuration/appSettings/add[@key="applicationDatabase"]/@value').Node.Value
}
