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

        [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup]
		$Scope, 

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
		$Separator = $Separator
	)

	_Log "Normalizing path '$Path' with arguments $(_DumpObj $PSBoundParameters)"

	$Path = $Path -replace '[/|\\]', $Separator
	$newPath = ''

	$scopeName = $Scope.ToString().TrimEnd('s')

	if ($IncludeLeadingSeparator) { $newPath += $Separator }
	if ($IncludeTeamProject) { $newPath += $Project + $Separator }
	if ($IncludeScope) { $newPath += $scopeName + $Separator }

	if(-not $ExcludePath.IsPresent)
	{
		$Path = $Path.Trim(' ', $Separator)

		if ($Path -like "$Project${Separator}$scopeName${Separator}*")
		{
			$Path = $Path.Substring("$Project${Separator}$scopeName${Separator}".Length)
		}
		if ($Path -like "$Project${Separator}*")
		{
			$Path = $Path.Substring($Path.IndexOf($Separator))
		}
		elseif ($Path -eq $Project)
		{
			$Path = ''
		}

		$newPath += $Path
	}

	if ($newPath.EndsWith($Separator) -and (-not $IncludeTrailingSeparator.IsPresent))
	{ 
		$newPath = $newPath.TrimEnd($Separator)
	}

	_Log "Normalized path: $newPath"

	return $newPath -replace '${Separator}${Separator}{2,}', $Separator
}
