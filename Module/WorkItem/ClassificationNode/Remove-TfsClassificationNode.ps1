#define ITEM_TYPE Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode
<#
.SYNOPSIS
    Deletes one or more Work Item Areas.

.PARAMETER Area
    HELP_PARAM_AREA

.PARAMETER MoveTo
    Specifies the new area path for the work items currently assigned to the area being deleted, if any. When omitted, defaults to the root area

.PARAMETER Project
    HELP_PARAM_PROJECT

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
#>
Function Remove-TfsClassificationNode
{
    [CmdletBinding(ConfirmImpact='High', SupportsShouldProcess=$true)]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [Alias("Area")]
        [Alias("Iteration")]
        [Alias("Path")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [type]'ITEM_TYPE')})] 
        [SupportsWildcards()]
        [object]
        $Node,

        [Parameter(Position=1)]
        [Alias("NewPath")]
        [ValidateScript({ ($_ -is [string]) -or ($_ -is [type]'ITEM_TYPE') })] 
        [object]
        $MoveTo = '\\',

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
        GET_STRUCTURE_GROUP

        $nodes = Get-TfsClassificationNode -Node $Node -StructureGroup $StructureGroup -Project $Project -Collection $Collection
        $moveToNode =  Get-TfsClassificationNode -Node $MoveTo -StructureGroup $StructureGroup -Project $Project -Collection $Collection

        if(-not $moveToNode)
        {
            throw "Invalid or non-existent node '$MoveTo'. To remove nodes, supply a valid node in the -MoveTo argument"
        }

        _Log "Remove nodes and move orphaned work items no node '$($moveToNode.FullPath)'"

        GET_TEAM_PROJECT($tp,$tpc)

        $client = Get-TfsRestClient 'Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient' -Collection $tpc

        foreach($node in $nodes)
        {
            if(-not ($PSCmdlet.ShouldProcess($node.TeamProject, "Remove node $($node.RelativePath)")))
            {
                continue
            }

            CALL_ASYNC($client.DeleteClassificationNodeAsync($node.TeamProject,$StructureGroup,$node.RelativePath,$moveToNode.Id), "Error removing node '$($node.FullPath)'")
        }
    }
}

Set-Alias -Name Remove-TfsArea -Value Remove-TfsClassificationNode
Set-Alias -Name Remove-TfsIteration -Value Remove-TfsClassificationNode