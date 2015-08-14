Function Get-TfsInstallationPath
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
		[ValidateSet('BaseInstallation', 'ApplicationTier', 'SharePointExtensions', 'TeamBuild', 'Tools', 'VersionControlProxy')]
		[string]
		$Component = 'BaseInstallation',

		[Parameter()]
		[string]
		$Version = '12.0',

		[Parameter()]
		[System.Management.Automation.Credential()]
		[System.Management.Automation.PSCredential]
		$Credential
	)

	Process
	{

		$scriptBlock = New-ScriptBlock -EntryPoint '_GetInstallationPath' -Dependency 'Test-RegistryValue', 'Get-RegistryValue'

		return Invoke-ScriptBlock -ScriptBlock $scriptBlock -Computer $Computer -Credential $Credential -ArgumentList $Version, $Component
	}
}


Function _GetInstallationPath($Version, $Component)
{
	return Get-InstallationPath @PSBoundParameters
}
