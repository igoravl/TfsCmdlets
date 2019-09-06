#define ITEM_TYPE Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode
<#
.SYNOPSIS
    Renames a Work Item Iteration.

.PARAMETER Iteration
    HELP_PARAM_ITERATION

.PARAMETER NewName
    Specifies the new name of the iteration. Enter only a name, not a path and name. If you enter a path that is different from the path that is specified in the Iteration parameter, Rename-TfsIteration generates an error. To rename and move an item, use the Move-TfsIteration cmdlet.

.PARAMETER Project
    HELP_PARAM_PROJECT

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
#>
Function Rename-TfsClassificationNode
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [ValidateScript({($_ -is [string]) -or ($_ -is [ITEM_TYPE])})] 
        [Alias("Area")]
        [Alias("Iteration")]
        [Alias("Path")]
        [object]
        $Node,

        [Parameter(Mandatory=$true, Position=1)]
        [string]
        $NewName,

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

        GET_CLIENT('Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient')

        $nodeToRename = Get-TfsClassificationNode -Node $Node -StructureGroup $StructureGroup -Project $Project -Collection $Collection

        if(-not $PSCmdlet.ShouldProcess($nodeToRename.FullPath, "Rename node to '$NewName'"))
        {
            return
        }

        $patch = New-Object 'ITEM_TYPE' -Property @{
            Name = $NewName
        }
    
        CALL_ASYNC($client.UpdateClassificationNodeAsync($patch, $tp.Name, $structureGroup, $nodeToRename.RelativePath.SubString(1)), "Error renaming node $node")

        if ($Passthru)
        {
            return $result
        }
    }
}

Set-Alias -Name Rename-TfsArea -Value Rename-TfsClassificationNode
Set-Alias -Name Rename-TfsIteration -Value Rename-TfsClassificationNode