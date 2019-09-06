#define ITEM_TYPE Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode
Function Move-TfsClassificationNode
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter(Mandatory=$true, Position=0)]
        [Alias('Area')]
        [Alias('Iteration')]
        [Alias('Path')]
        [object]
        $Node,

        [Parameter(Mandatory=$true, Position=1)]
        [Alias('MoveTo')]
        [object]
        $Destination,

        [Parameter()]
        [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup]
        $StructureGroup,

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

    Process
    {
        GET_STRUCTURE_GROUP

        GET_TEAM_PROJECT($tp,$tpc)

        $structureGroup = _GetStructureGroup $structureGroup

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

        CALL_ASYNC($client.CreateOrUpdateClassificationNodeAsync($patch, $tp.Name, $structureGroup, $destinationNode.RelativePath.SubString(1)), "Error moving node $($sourceNode.RelativePath) to $($destinationNode.RelativePath)")

        if($Passthru.IsPresent)
        {
            return $result
        }
    }
}

Set-Alias -Name Move-TfsArea -Value Move-TfsClassificationNode
Set-Alias -Name Move-TfsIteration -Value Move-TfsClassificationNode