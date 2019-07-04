Function _GetCssNodes($Node, $Scope, $Project, $Collection)
{
    Process
	{
		if ($Node -is [Microsoft.TeamFoundation.Server.NodeInfo])
		{
			return $Node
		}

		$tp = Get-TfsTeamProject -Project $Project -Collection $Collection
		$tpc = $tp.Store.TeamProjectCollection
		$projectName = $tp.Name
        $cssService = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService")
        
		if ($node -is [uri])
		{
			return $cssService.GetNode($node)
		}

		$rootPath = _NormalizeCssNodePath -Project $projectName -Scope $Scope -Path '' -IncludeTeamProject -IncludeScope
		$rootNodeUri = $cssService.GetNodeFromPath("$rootPath").Uri
		$rootElement = $cssService.GetNodesXml(@($rootNodeUri), $true)

		$nodePaths = $rootElement.SelectNodes('//@Path') | Select-Object -ExpandProperty '#text'

		$fullPath = _NormalizeCssNodePath  -Project $projectName -Scope $Scope -Path $Node -IncludeScope -IncludeTeamProject -IncludeLeadingBackslash
		$matchingPaths = $nodePaths | Where-Object { _Log "Evaluating '$_' against pattern '$fullPath'" -Caller (_GetLogCallStack); $_ -like $fullPath }

        return $matchingPaths | Foreach-Object { $cssService.GetNodeFromPath($_) }
    }
}

Function _DeleteCssNode($Node, $Scope, $MoveToNode, $Project, $Collection)
{
    Process
	{
		$newNode = _GetCssNodes -Node $MoveToNode -Scope $Scope -Project $Project -Collection $Collection
		$cssService = _GetCssService -Project $Project -Collection $Collection

		$cssService.DeleteBranches($Node.Uri, $newNode.Uri)        
    }
}

Function _NewCssNode ($Project, $Path, $Scope, $Collection, $StartDate, $FinishDate)
{
	Process
	{
		$tp = Get-TfsTeamProject -Project $Project -Collection $Collection
		$tpc = $tp.Store.TeamProjectCollection
		$projectName = $tp.Name
        $cssService = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService")

        try
        {
			$fullPath = _NormalizeCssNodePath -Project $projectName -Scope $Scope -Path $Path -IncludeTeamProject -IncludeScope
			$parentPath = Split-Path $fullPath -Parent
			$nodeName = Split-Path $fullPath -Leaf
            $parentNode = $cssService.GetNodeFromPath($parentPath)
        }
        catch
        {
            $parentNode = _NewCssNode -Project $Project -Path $parentPath -Scope $Scope -Collection $Collection
        }

		if ($StartDate -or $FinishDate)
		{
			$cssService = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService4")
			$nodeUri = $cssService.CreateNode($nodeName, $parentNode.Uri, $StartDate, $FinishDate)
		}
		else
		{
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

	if ($IncludeTrailingBackslash) { $newPath += $Scope + '\\' }

	_Log "Normalized path: $newPath"

	return $newPath -replace '\\\\{2,}', '\\'
}

Function _GetCssService($Project, $Collection, $Version)
{
	$tp = Get-TfsTeamProject -Project $Project -Collection $Collection
	$tpc = $tp.Store.TeamProjectCollection
	$projectName = $tp.Name

    return $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService$Version")
}