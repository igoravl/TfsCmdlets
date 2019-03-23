Function _NormalizeQueryPath($Path, $RootFolder, $ProjectName)
{
    if([string]::IsNullOrWhiteSpace($Path))
    {
        return [string]::Empty
    }

    $newPath = [System.Text.RegularExpressions.Regex]::Replace($Path, '\\+|/{2,}', '/')

    if ($newPath.StartsWith("/"))
    {
        $newPath = $newPath.Substring(1)
    }

    if ($newPath.EndsWith('/'))
    {
        $newPath = $newPath.Substring(0, $newPath.Length-1)
    }

    $pattern = "($ProjectName/)?($RootFolder/)?(.+)"
    $match = [regex]::Match($newPath, $pattern)

    return "$ProjectName/$RootFolder/$($match.Groups[$match.Groups.Count-1])"
}

Function _RegisterQueryHelper()
{
    if (([System.Management.Automation.PSTypeName]'TfsCmdlets.QueryHelper').Type)
    {
        return
    }

    Add-Type -Language CSharp -ReferencedAssemblies 'Microsoft.TeamFoundation.WorkItemTracking.Client' `
        -TypeDefinition @'
${File:CSharp\QueryHelper.cs}
'@
}

Function _GetQueryFoldersRecursively
{
    Param
    (
        [Parameter(ValueFromPipeline=$true)]
        [Microsoft.TeamFoundation.WorkItemTracking.Client.QueryFolder2]
        $Folder
    )
    
    Process
    {
        $Folder.GetChildrenAsync().Wait()

        $Folder.GetChildren() | Where-Object {$_ -Is [Microsoft.TeamFoundation.WorkItemTracking.Client.QueryFolder2]} | ForEach-Object {
            Write-Output $_
            _GetQueryFoldersRecursively $_
        }
    }
}

Function _GetQueriesRecursively
{
    Param
    (
        [Parameter(ValueFromPipeline=$true)]
        [Microsoft.TeamFoundation.WorkItemTracking.Client.QueryFolder2]
        $Folder
    )
    
    Process
    {
        $Folder.GetChildrenAsync().Wait()

        foreach($i in $Folder.GetChildren())
        {
            if ($i -is [Microsoft.TeamFoundation.WorkItemTracking.Client.QueryFolder2])
            {
                _GetQueriesRecursively $i
            }
            else
            {
                Write-Output $i
            }
        }
    }
}
