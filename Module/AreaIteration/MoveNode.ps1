#define ITEM_TYPE Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode
Function _MoveNode
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter()]
        [SupportsWildcards()]
        [Alias('Area')]
        [Alias('Iteration')]
        [Alias('Path')]
        [object]
        $Node,

        [Parameter()]
        [Alias('MoveTo')]
        [object]
        $Destination,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection,

        [Parameter()]
        [switch]
        $Passthru
    )

    GET_TEAM_PROJECT($tp,$tpc)

    $structureGroup = _GetStructureGroup

    $sourceNode = _GetNode -Node $Node -Project $Project -Collection $Collection

    if(-not $sourceNode)
    {
        throw "Invalid or non-existent $StructureGroup path '$Node'"
    }

    _Log "Source node: '$($sourceNode.FullPath)'"

    $destinationNode = _GetNode -Node $Destination -Project $Project -Collection $Collection
    
    if(-not $destinationNode)
    {
        throw "Invalid or non-existent $StructureGroup path '$Node'"
    }

    _Log "Destination node: '$($destinationNode.FullPath)'"

    $moveTo = "$($destinationNode.Path)\\$($sourceNode.Name)"

    if (-not $PSCmdlet.ShouldProcess($sourceNode.FullPath, "Move node to '$moveTo'"))
    {
        return
    }

    $patch = New-Object 'ITEM_TYPE' -Property @{
        Id = $sourceNode.Id
    }

    GET_CLIENT('Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient')

    CALL_ASYNC($client.CreateOrUpdateClassificationNodeAsync($patch, $tp.Name, $structureGroup, $destinationNode.RelativePath.TrimStart('\\')), "Error moving node $($sourceNode.RelativePath) to $($destinationNode.RelativePath)")

    if($Passthru.IsPresent)
    {
        return $result
    }
}