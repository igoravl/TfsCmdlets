Function _NormalizeNodePath
{
	Param
	(
		[Parameter(Mandatory=$true, Position=0)]
		[AllowEmptyString()]
		[string]
		$Path, 

		[Parameter(Mandatory=$true)]
		[string]
		$Project, 

        [string]
		$Scope = '', 

		[switch]
		$IncludeScope,

		[switch]
		$ExcludePath,

		[switch]
		$IncludeLeadingSeparator,

		[switch]
		$IncludeTrailingSeparator,

		[switch]
		$IncludeTeamProject,

		[string]
		$Separator = '\\'
	)

	_Log "Normalizing path '$Path' with arguments $(_DumpObj $PSBoundParameters)"

	$Path = $Path -replace '[/|\\\\]+', $Separator
	$newPath = @()

	switch($Scope)
	{
		'Areas' {
			$Scope = 'Area'
		}
		'Iterations' {
			$Scope = 'Iteration'
		}
	}

	if ($IncludeLeadingSeparator) { $newPath += '' }
	if ($IncludeTeamProject) { $newPath += $Project }
	if ($IncludeScope) { $newPath += $Scope }

	if(-not $ExcludePath.IsPresent)
	{
		$Path = $Path.Trim(' ', $Separator)

		if($Path.StartsWith($Project))
		{
			if ($Path -like "$Project${Separator}$Scope${Separator}*")
			{
				$Path = $Path.Substring("$Project${Separator}$Scope${Separator}".Length)
			}
			if ($Path -like "$Project${Separator}*")
			{
				$Path = $Path.Substring($Path.IndexOf($Separator)+1)
			}
			elseif ($Path -eq $Project)
			{
				$Path = ''
			}
		}
		elseif ($Path.StartsWith($Scope))
		{
			if ($Path -like "$Scope${Separator}*")
			{
				$Path = $Path.Substring($Path.IndexOf($Separator)+1)
			}
			elseif ($Path -eq $Scope)
			{
				$Path = ''
			}
		}
		
		$newPath += $Path
	}

	if ($IncludeTrailingSeparator.IsPresent)
	{ 
		$newPath += ''
	}

	$newPath = $newPath -join $Separator

	_Log "Normalized path: $newPath"

	return $newPath
}
