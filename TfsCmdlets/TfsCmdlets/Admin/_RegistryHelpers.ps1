Function Test-RegistryValue
{
	Param
	(
		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		$Path,

		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		$Value
	)

	Process
	{
		try
		{
			Get-RegistryValue -Path $Path -Value $Value | Out-Null
			return $true
		}
		catch {}

		return $false

	}
}

Function Get-RegistryValue
{
	Param
	(
		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		$Path,

		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		$Value
	)

	Process
	{
		return Get-ItemProperty -Path $Path | Select-Object -ExpandProperty $Value
	}
}

Function Get-InstallationPath
{
	Param
	(
		[string]
		$Version, 
		
		[string]
		$Component
	)

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
