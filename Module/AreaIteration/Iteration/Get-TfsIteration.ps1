#define ITEM_TYPE Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode
<#
.SYNOPSIS
    Gets one or more Work Item Iterations from a given Team Project.

.PARAMETER Iteration
    HELP_PARAM_ITERATION

.PARAMETER Project
    HELP_PARAM_PROJECT

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String

#>
Function Get-TfsIteration
{
    [CmdletBinding()]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter(Position=0)]
        [Alias("Path")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [uri]) -or ($_ -is [ITEM_TYPE])})]
        [SupportsWildcards()]
        [object]
        $Iteration = '\\**',

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        return _GetNode -Path $Iteration -StructureGroup Iterations -Project $Project -Collection $Collection
    }
}
