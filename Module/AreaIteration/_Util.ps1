Function _GetCssNodes($Node, $Scope, $Project, $Collection)
{
    Process
	{
		if ($Node -is [Microsoft.TeamFoundation.Server.NodeInfo])
		{
			_Log "Input item is of type NodeInfo; returning input item immediately, without further processing."

			return $Node
		}

		GET_TEAM_PROJECT($tp,$tpc)

		$projectName = $tp.Name
		$cssService = _GetCssService -Collection $tpc
        
		if ($node -is [uri])
		{
			_Log "Getting node by URL [$node]"
			return $cssService.GetNode($node)
		}

		$rootPath = _NormalizeCssNodePath -Project $projectName -Scope $Scope -Path '' -IncludeTeamProject -IncludeScope
		$rootNodeUri = $cssService.GetNodeFromPath("$rootPath").Uri

		_Log "Retrieving Nodes XML from root path [$rootPath]"

		$rootElement = $cssService.GetNodesXml(@($rootNodeUri), $true)
		$nodePaths = $rootElement.SelectNodes('//@Path') | Select-Object -ExpandProperty '#text'

		$fullPath = _NormalizeCssNodePath  -Project $projectName -Scope $Scope -Path $Node -IncludeScope -IncludeTeamProject -IncludeLeadingBackslash
		$matchingPaths = $nodePaths | Where-Object { _Log "Evaluating '$_' against pattern '$fullPath' == $($_ -like $fullPath)" -Caller (_GetLogCallStack); $_ -like $fullPath }

        return $matchingPaths | Foreach-Object { _Log "Returning node from path [$_]" -Caller (_GetLogCallStack); $cssService.GetNodeFromPath($_) }
    }
}

Function _DeleteCssNode($Node, $Scope, $MoveToNode, $Project, $Collection)
{
	GET_TEAM_PROJECT($tp,$tpc)

	$newNode = _GetCssNodes -Node $MoveToNode -Scope $Scope -Project $Project -Collection $Collection

	_Log "Moving work items from deleted node [$($Node.Path)] to node [$($newNode.Path)]"

	$cssService = _GetCssService -Collection $tpc

	$cssService.DeleteBranches($Node.Uri, $newNode.Uri)        
}

Function _NewCssNode ($Project, $Path, $Scope, $Collection, $StartDate, $FinishDate)
{
	Process
	{
		GET_TEAM_PROJECT($tp,$tpc)

		$projectName = $tp.Name

		_Log "Creating $Scope node [$Path] in project $projectName"

		$cssService = _GetCssService -Collection $tpc

        try
        {
			$fullPath = _NormalizeCssNodePath -Project $projectName -Scope $Scope -Path $Path -IncludeTeamProject -IncludeScope
			$parentPath = Split-Path $fullPath -Parent
			$nodeName = Split-Path $fullPath -Leaf
            $parentNode = $cssService.GetNodeFromPath($parentPath)
        }
        catch
        {
			_Log "Parent node [$parentPath] does not exist. Creating recursively..."

            $parentNode = _NewCssNode -Project $Project -Path $parentPath -Scope $Scope -Collection $Collection
        }

		if ($StartDate -or $FinishDate)
		{
			_Log "Iteration date(s) were provided as Start = [$StartDate], Finish = [$FinishDate]. Creating iteration with supplied dates"
			$cssService = _GetCssService -Collection $tpc -Version 4
			$nodeUri = $cssService.CreateNode($nodeName, $parentNode.Uri, $StartDate, $FinishDate)
		}
		else
		{
			if($Scope -eq 'Iteration')
			{
				_Log "Iteration date(s) were not provided. Creating iteration without dates"
			}
			
			$nodeUri = $cssService.CreateNode($nodeName, $parentNode.Uri)
		}

        return $cssService.GetNode($nodeUri)
    }
}

Function _NormalizeCssNodePath
{
	Param
	(
		[Parameter(Mandatory=$true)]
		[string]
		$Project, 

		[ValidateSet('Area', 'Iteration')]
		[string]
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

	if ($IncludeLeadingBackslash) { $newPath += '\\' }
	if ($IncludeTeamProject) { $newPath += $Project + '\\' }
	if ($IncludeScope) { $newPath += $Scope + '\\' }

	if(-not $ExcludePath.IsPresent)
	{
		$Path = $Path.Trim(' ', '\\')

		if ($Path -like "$Project\\$Scope\\*")
		{
			$Path = $Path.Substring("$Project\\$Scope\\".Length)
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

Function _GetCssService($Collection, $Version)
{
	GET_COLLECTION($tpc)

    return $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService$Version")
}