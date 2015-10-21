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
		[string]
		$Version = '12.0',

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
