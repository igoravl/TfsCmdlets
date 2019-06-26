Function _GetRegistryValue
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
