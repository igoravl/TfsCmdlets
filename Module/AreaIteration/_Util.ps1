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

Function _GetCssService($Collection, $Version)
{
	GET_COLLECTION($tpc)

    return $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService$Version")
}