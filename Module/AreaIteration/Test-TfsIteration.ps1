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
    [OutputType('Microsoft.TeamFoundation.Server.NodeInfo')]
    Param
    (
        [Parameter(Position=0)]
        [Alias("Path")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [uri]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})]
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
        try
        {
            return [bool] (_GetCssNodes -Node $Iteration -Scope Iteration -Project $Project -Collection $Collection)
        }
        catch
        {
            Write-Warning "Error testing path: $_"
            return $false
        }
    }
}
