#define ITEM_TYPE Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode
Function _MoveNode
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter()]
        [SupportsWildcards()]
        [object]
        $Node,

        [Parameter()]
        [SupportsWildcards()]
        [object]
        $MoveTo,

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

    GET_TEAM_PROJECT($tp,$tpc)

    $sourceNode = _GetNode -Node $Node -StructureGroup $StructureGroup -Project $Project -Collection $Collection

    if(-not $sourceNode)
    {
        throw "Invalid or non-existent $StructureGroup path '$Node'"
    }

    $destinationNode = _GetNode -Node $MoveTo -StructureGroup $StructureGroup -Project $Project -Collection $Collection

    if(-not $destinationNode)
    {
        throw "Invalid or non-existent $StructureGroup path '$Node'"
    }

    GET_CLIENT('Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient')
}