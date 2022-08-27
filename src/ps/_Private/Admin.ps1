Function _GetInstallationPath($Version, $Component = 'BaseInstallation')
{
	if(-not $Version)
	{
		foreach($v in 20..8)
		{
			if(Test-Path "HKLM:\Software\Microsoft\TeamFoundationServer\${v}.0")
            {
				$Version = "${v}.0"
				break
			}
		}
    
		if(-not $Version)
		{
			throw "No Team Foundation Server installation found in computer $([Environment]::MachineName)"
		}
	}

	$rootKeyPath = "HKLM:\Software\Microsoft\TeamFoundationServer\$Version"

	if ($Component -eq 'BaseInstallation')
	{
		$componentPath = $rootKeyPath
	}
	else
	{
		$componentPath = "$rootKeyPath\InstalledComponents\$Component"
	}

	if (-not (Test-Path $rootKeyPath))
	{
		throw "Team Foundation Server is not installed in computer $([Environment]::MachineName)"
	}

	if (-not (Test-Path $componentPath))
	{
		throw "Team Foundation Server component '$Component' is not installed in computer $([Environment]::MachineName)"
	}

	return _GetRegistryValue -Path $componentPath -Value 'InstallPath'
}

Function _GetConnectionString($Version)
{
	$path = _GetInstallationPath -Version $Version -Component ApplicationTier
	$webConfigPath = Join-Path $path 'Web Services/Web.config'
	$webConfig = [xml] (Get-Content $webConfigPath)

	return (Select-Xml -Xml $webConfig -XPath '/configuration/appSettings/add[@key="applicationDatabase"]/@value').Node.Value
}

Function _GetRegistryValue($Path, $Value)
{
    return Get-ItemProperty -Path $Path -ErrorAction Continue | Select-Object -ExpandProperty $Value
}

Function _TestRegistryValue($Path, $Value)
{
    try
    {
        _GetRegistryValue -Path $Path -Value $Value | Out-Null
        return $true
    }
    finally {}

    return $false
}

