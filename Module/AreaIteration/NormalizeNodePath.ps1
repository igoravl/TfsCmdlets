Function _NormalizeNodePath
{
	Param
	(
		[Parameter(Mandatory=$true)]
		[string]
		$Project, 

        [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup]
		$Scope, 

		[Parameter(Mandatory=$true)]
		[AllowEmptyString()]
		[string]
		$Path, 

		[switch]
		$IncludeScope,

		[switch]
		$ExcludePath,

		[switch]
		$IncludeLeadingBackslash,

		[switch]
		$IncludeTrailingBackslash,

		[switch]
		$IncludeTeamProject
	)

	_Log "Normalizing path '$Path' with arguments $(_DumpObj $PSBoundParameters)"

	$newPath = ''

	$scopeName = $Scope.ToString().TrimEnd('s')

	if ($IncludeLeadingBackslash) { $newPath += '\\' }
	if ($IncludeTeamProject) { $newPath += $Project + '\\' }
	if ($IncludeScope) { $newPath += $scopeName + '\\' }

	if(-not $ExcludePath.IsPresent)
	{
		$Path = $Path.Trim(' ', '\\')

		if ($Path -like "$Project\\$scopeName\\*")
		{
			$Path = $Path.Substring("$Project\\$scopeName\\".Length)
		}
		if ($Path -like "$Project\\*")
		{
			$Path = $Path.Substring($Path.IndexOf('\\'))
		}
		elseif ($Path -eq $Project)
		{
			$Path = ''
		}

		$newPath += $Path
	}

	if ($newPath.EndsWith('\\') -and (-not $IncludeTrailingBackslash.IsPresent))
	{ 
		$newPath = $newPath.TrimEnd('\\')
	}

	_Log "Normalized path: $newPath"

	return $newPath -replace '\\\\{2,}', '\\'
}
