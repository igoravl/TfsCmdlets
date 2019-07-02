<#
.SYNOPSIS
Determines whether the specified Areas Paths exist.

.PARAMETER Area
HELP_PARAM_AREA

.PARAMETER Project
HELP_PARAM_PROJECT

.PARAMETER Collection
HELP_PARAM_COLLECTION

.INPUTS
Microsoft.TeamFoundation.WorkItemTracking.Client.Project
System.String

.EXAMPLE
Test-TfsArea 'Fabrikam Web Team' -Project 'Fabrikam Fiber'
Returns $true if an area path called 'Fabrikam Web Team' exists in a team project called 'Fabrikam Fiber'

#>
Function Test-TfsArea
{
    [CmdletBinding()]
    [OutputType('bool')]
    Param
    (
        [Parameter(Position=0)]
        [Alias("Path")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [uri]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})]
        [SupportsWildcards()]
        [object]
        $Area = '\\**',

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
            return [bool] (_GetCssNodes -Node $Area -Scope Area -Project $Project -Collection $Collection)
        }
        catch
        {
            Write-Warning "Error testing path: $_"
            return $false
        }
    }
}
