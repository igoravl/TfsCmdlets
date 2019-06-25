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

		$rootPath = _NormalizePath "$projectName\\$Scope"
		$fullPath = _NormalizePath "$rootPath\\$Node"

		$rootNodeUri = $cssService.GetNodeFromPath("$rootPath").Uri
		$rootElement = $cssService.GetNodesXml(@($rootNodeUri), $true)
		
		$nodePaths = $rootElement.SelectNodes('//@Path') | Select-Object -ExpandProperty '#text'
		$matchingPaths = $nodePaths | Where-Object { _Log $_; $_ -like $fullPath }

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
			$fullPath = _NormalizePath "$projectName\\$Scope\\$Path"
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

Function _NormalizePath($Path)
{
	_Log "Normalizing path $Path"

	if([string]::IsNullOrWhiteSpace($Path))
	{
		_Log "Unable to normalize empty paths"
		return [string]::Empty
	}

	$newPath = [System.Text.RegularExpressions.Regex]::Replace($Path, '\\\\{2,}', '\\')

	if (-not $newPath.StartsWith("\\"))
	{
		$newPath = "\\$newPath"
	}

	if ($newPath.EndsWith("\\"))
	{
		$newPath = $newPath.Substring(0, $newPath.Length-1)
	}

	_Log "Normalized path: $newPath"

	return $newPath
}

Function _GetCssService($Project, $Collection, $Version)
{
	$tp = Get-TfsTeamProject -Project $Project -Collection $Collection
	$tpc = $tp.Store.TeamProjectCollection
	$projectName = $tp.Name

    return $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService$Version")
}