<#
.SYNOPSIS
    Determines whether the specified Iterations Paths exist.

.PARAMETER Iteration
    HELP_PARAM_ITERATION

.PARAMETER Project
    HELP_PARAM_PROJECT

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String

.EXAMPLE
    Test-TfsIteration 'Fabrikam Web Team' -Project 'Fabrikam Fiber'
    Returns $true if an iteration path called 'Fabrikam Web Team' exists in a team project called 'Fabrikam Fiber'

#>
Function Test-TfsIteration
{
    [CmdletBinding()]
    [OutputType([bool])]
    Param
    (
        [Parameter(Position=0)]
        [Alias("Path")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [uri]) -or ($_ -is [ITEM_TYPE])})]
        [SupportsWildcards()]
        [object]
        $Iteration,

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        return _TestNode @PSBoundParameters
    }
}
