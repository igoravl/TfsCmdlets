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
