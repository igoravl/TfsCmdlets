Function _GetInstallationPath($Version, $Component)
{
	$rootKeyPath = "HKLM:\\Software\\Microsoft\\TeamFoundationServer\\$Version"

	if ($Component -eq 'BaseInstallation')
	{
		$componentPath = $rootKeyPath
	}
	else
	{
		$componentPath = "$rootKeyPath\\InstalledComponents\\$Component"
	}

	if (-not (_TestRegistryValue -Path $rootKeyPath -Value 'InstallPath'))
	{
		throw "Team Foundation Server is not installed in computer $env:COMPUTERNAME"
	}

	if (-not (_TestRegistryValue -Path $componentPath -Value 'InstallPath'))
	{
		throw "Team Foundation Server component '$Component' is not installed in computer $env:COMPUTERNAME"
	}

	return _GetRegistryValue -Path $componentPath -Value 'InstallPath'
}
