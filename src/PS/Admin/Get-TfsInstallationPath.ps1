<#
.SYNOPSIS
    Gets the installation path of a given Team Foundation Server component.

.PARAMETER Computer
    The machine name of the server where the TFS component is installed. It must be properly configured for PowerShell Remoting in case it's a remote machine. If omitted, defaults to the local machine where the script is being run.
    Optionally, a System.Management.Automation.Runspaces.PSSession object pointing to a previously opened PowerShell Remote session can be provided instead.

.PARAMETER Component
    Indicates the TFS component whose installation path is being searched for. For the main TFS installation directory, use BaseInstallation.

.PARAMETER Version
    The TFS version number, in the format '##.#'. For TFS 2015, use '14.0'

.PARAMETER Credential
    The user credentials to be used to access a remote machine. Those credentials must have the required permission to execute a PowerShell Remote session on that computer and also the permission to access the Windows Registry.
#>
Function Get-TfsInstallationPath
{
	[CmdletBinding()]
	[OutputType([string])]
	Param
	(
		[Parameter()]
		[object]
		[Alias('Session')]
		$Computer,

		[Parameter()]
		[ValidateSet('BaseInstallation', 'ApplicationTier', 'SharePointExtensions', 'TeamBuild', 'Tools', 'VersionControlProxy')]
		[string]
		$Component = 'BaseInstallation',

		[Parameter(Mandatory=$true)]
		[ValidateSet('11.0','12.0','14.0','15.0')]
		[string]
		$Version,

		[Parameter()]
		[System.Management.Automation.Credential()]
		[System.Management.Automation.PSCredential]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)

	Process
	{
		$scriptBlock = New-ScriptBlock -EntryPoint '_GetInstallationPath' -Dependency 'Test-RegistryValue', 'Get-RegistryValue'

		return Invoke-ScriptBlock -ScriptBlock $scriptBlock -Computer $Computer -Credential $Credential -ArgumentList $Version, $Component
	}
}


Function _GetInstallationPath($Version, $Component)
{
	$rootKeyPath = "HKLM:\Software\Microsoft\TeamFoundationServer\$Version"

	if ($Component -eq 'BaseInstallation')
	{
		$componentPath = $rootKeyPath
	}
	else
	{
		$componentPath = "$rootKeyPath\InstalledComponents\$Component"
	}

	if (-not (Test-RegistryValue -Path $rootKeyPath -Value 'InstallPath'))
	{
		throw "Team Foundation Server is not installed in computer $env:COMPUTERNAME"
	}

	if (-not (Test-RegistryValue -Path $componentPath -Value 'InstallPath'))
	{
		throw "Team Foundation Server component '$Component' is not installed in computer $env:COMPUTERNAME"
	}

	return Get-RegistryValue -Path $componentPath -Value 'InstallPath'
}
