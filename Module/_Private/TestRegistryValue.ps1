Function _TestRegistryValue
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
			_GetRegistryValue -Path $Path -Value $Value | Out-Null
			return $true
		}
		finally {}

		return $false

	}
}
