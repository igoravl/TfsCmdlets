Function _FindQueryFolder($folder, $parent)
{
    Write-Verbose "_FindQueryFolder: Searching for $folder under $($parent.Path)"

    if ($folder -is [Microsoft.TeamFoundation.WorkItemTracking.Client.QueryFolder])
    {
        Write-Verbose "_FindQueryFolder: Returning folder immediately, since it's a QueryFolder object"
        return $folder
    }

    $folders = $parent | ? {$_ -Is [Microsoft.TeamFoundation.WorkItemTracking.Client.QueryFolder]}

    foreach($f in $folders)
    {
        if (($f.Path -like $folder) -or ($f.Name -like $folder))
        {
            Write-Verbose "_FindQueryFolder: Found folder `"$($f.Path)`" matching `"$folder`""
            return @{$f.Name = $f}
        }
    }

    foreach($f in $folders)
    {
        Write-Verbose "_FindQueryFolder: Starting recursive search"

        $result = _FindQueryFolder $folder $f

        if ($result)
        {
            return $result
        }
    }
}

Function _FindQuery($path, $parent)
{
    Write-Verbose "_FindQuery: Searching for $path under $($parent.Path)"

    foreach($item in $parent)
    {
        if (($item -Is [Microsoft.TeamFoundation.WorkItemTracking.Client.QueryDefinition]) -and (($item.Path -like $path) -or ($item.Name -like $path)))
        {
            # Search immediate children

            Write-Verbose "_FindQuery: Found local query `"$($item.Path)`" matching `"$path`""
            $item
        }
        elseif ($item -Is [Microsoft.TeamFoundation.WorkItemTracking.Client.QueryFolder])
        {
            # Search descendants recursively

            Write-Verbose "_FindQuery: Starting recursive search"
            _FindQuery $path $item
        }
        else
        {
            Write-Verbose "_FindQuery: Skipped `"$($item.Path)`" since it doesn't match $path"
        }
    }
}

Function _NormalizeQueryPath($Path, $ProjectName)
{
    if([string]::IsNullOrWhiteSpace($Path))
    {
        return [string]::Empty
    }

    $newPath = [System.Text.RegularExpressions.Regex]::Replace($Path, '//{2,}', '/')

    if ($newPath.StartsWith("/"))
    {
        $newPath = $newPath.Substring(1)
    }

    if ($newPath.EndsWith('/'))
    {
        $newPath = $newPath.Substring(0, $newPath.Length-1)
    }

    if ($newPath -notlike "$ProjectName*")
    {
        $newPath = "$ProjectName/$newPath"
    }

    return $newPath
}
