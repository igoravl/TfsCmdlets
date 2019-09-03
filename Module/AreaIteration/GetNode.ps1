#define ITEM_TYPE Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode
Function _GetNode
{
    [CmdletBinding()]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter()]
        [SupportsWildcards()]
        [object]
        $Path = '\\**',

        [Parameter()]
        [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup]
        $StructureGroup,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        CHECK_ITEM($Path)
        
        GET_TEAM_PROJECT($tp,$tpc)

        GET_CLIENT('Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient')

        if(_IsWildcard $Path)
        {
            $depth = 1
            $pattern = _NormalizeNodePath -Project $tp.Name -Scope $StructureGroup -Path $Path -IncludeScope -IncludeTeamProject -IncludeLeadingBackslash
            $Path = '/'

            _Log "Preparing to recursively search for pattern '$pattern'"
        }
        else
        {
            $Path = _NormalizeNodePath -Project $tp.Name -Scope $StructureGroup -Path $Path -IncludeLeadingBackslash
            $depth = 0

            _Log "Getting $StructureGroup under path '$Path'"
        }

        CALL_ASYNC($client.GetClassificationNodeAsync($tp.Name,$StructureGroup,$Path,$depth), "Error retrieving $StructureGroup from path '$Path'")

        if(-not ($pattern))
        {
            return $result
        }

        _GetNodeRecursively -Pattern $pattern -Node $result -StructureGroup $StructureGroup -Project $tp.Name -Client $client
    }
}

Function _GetNodeRecursively($Pattern, $Node, $StructureGroup, $Project, $Client, $Depth = 2)
{
    _Log "Searching for pattern '$Pattern' under $($Node.Path)"

    if($Node.HasChildren -and ($Node.Children.Count -eq 0))
    {
        _Log "Fetching child nodes for node '$($Node.Path)'"

        CALL_ASYNC($client.GetClassificationNodeAsync($Project,$StructureGroup,$Node.RelativePath, $Depth), "Error retrieving $StructureGroup from path '$($Node.RelativePath)'")
        $Node = $result
    }

    if($Node.Path -like $Pattern)
    {
        _Log "$($Node.Path) matches pattern $Pattern. Returning node."
        Write-Output $Node
    }

    foreach($c in $Node.Children)
    {
        _GetNodeRecursively -Pattern $Pattern -Node $c -StructureGroup $StructureGroup -Project $Project -Client $Client -Depth $Depth
    }
}