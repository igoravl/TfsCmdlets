#define ITEM_TYPE Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode
<#
.SYNOPSIS
    Modifies the name, position and/or the dates of a Work Item Iteration.

.PARAMETER Iteration
    HELP_PARAM_ITERATION

.PARAMETER NewName
    Specifies the new name of the iteration. Enter only a name, not a path and name. If you enter a path that is different from the path that is specified in the Iteration parameter, Rename-TfsIteration generates an error. To rename and move an item, use the Move-TfsIteration cmdlet.

.PARAMETER MoveBy
    Reorders an iteration by moving it either up or down inside its parent. A positive value moves an iteration down, whereas a negative one moves it up.

.PARAMETER StartDate
    Sets the start date of the iteration. To clear the start date, set it to $null. Note that when clearing a date, both must be cleared at the same time (i.e. setting both StartDate and FinishDate to $null)

.PARAMETER FinishDate
    Sets the finish date of the iteration. To clear the finish date, set it to $null. Note that when clearing a date, both must be cleared at the same time (i.e. setting both StartDate and FinishDate to $null)

.PARAMETER Project
    HELP_PARAM_PROJECT

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
#>
Function Set-TfsClassificationNode
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter(Position=0, Mandatory=$true, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [Alias("Area")]
        [Alias("Iteration")]
        [Alias("Path")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [ITEM_TYPE])})] 
        [SupportsWildcards()]
        [object]
        $Node,

        [Parameter()]
        [string]
        $StructureGroup,

        [Parameter()]
        [int]
        $MoveBy,

        [Parameter()]
        [Nullable[DateTime]]
        $StartDate,
    
        [Parameter()]
        [Nullable[DateTime]]
        $FinishDate,

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

        $nodeToSet = Get-TfsClassificationNode -Node $Node -StructureGroup $StructureGroup -Project $Project -Collection $Collection

        if (-not $nodeToSet)
        {
            throw "Invalid or non-existent node $Node"
        }

        GET_TEAM_PROJECT($tp,$tpc)

        $client = Get-TfsRestClient 'Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient' -Collection $tpc

        if ($PSBoundParameters.ContainsKey('MoveBy'))
        {
            Write-Warning "Reordering of areas/iterations is deprecated, as Azure DevOps UX keeps areas and iterations properly sorted. MoveBy argument ignored."
        }

        if ($StructureGroup -eq 'Iterations' -and ($PSBoundParameters.ContainsKey("StartDate") -or $PSBoundParameters.ContainsKey("FinishDate")))
        {
            if (-not ($PSBoundParameters.ContainsKey("StartDate") -and $PSBoundParameters.ContainsKey("FinishDate")))
            {
                throw "When setting iteration dates, both start and finish dates must be supplied."
            }

            if($PSCmdlet.ShouldProcess($nodeToSet.RelativePath, "Set iteration start date to '$StartDate' and finish date to '$FinishDate'"))
            {
                $patch = New-Object 'ITEM_TYPE' -Property @{
                    attributes = _NewDictionary @([string], [object]) @{
                        startDate = $StartDate
                        finishDate = $FinishDate
                    }
                }

                CALL_ASYNC($client.UpdateClassificationNodeAsync($patch, $tp.Name, $structureGroup, $nodeToSet.RelativePath.SubString(1)), "Error setting dates on iteration '$($nodeToSet.FullPath)'")
            }
        }

        if($Passthru)
        {
            return Get-TfsClassificationNode -Node $Node -StructureGroup $StructureGroup -Project $Project -Collection $Collection
        }
    }
}

Set-Alias -Name Set-TfsArea -Value Set-TfsClassificationNode
Set-Alias -Name Set-TfsIteration -Value Set-TfsClassificationNode