<#
.SYNOPSIS
Gets one or more Work Item Areas from a given Team Project.

.PARAMETER Area
${HelpParam_Area}

.PARAMETER Project
${HelpParam_Project}

.PARAMETER Collection
${HelpParam_Collection}

.INPUTS
Microsoft.TeamFoundation.WorkItemTracking.Client.Project
System.String

.EXAMPLE
Get-TfsArea
Returns all area paths in the currently connected Team Project (as defined by a previous call to Connect-TfsTeamProject)

.EXAMPLE
Get-TfsArea 'Fabrikam Web Team' -Project 'Fabrikam Fiber'
Returns information on an area path called 'Fabrikam Web Team' in a team project called 'Fabrikam Fiber'

.EXAMPLE
Get-TfsArea '\**\Support' -Project Tailspin
Performs a recursive search and returns all area paths named 'Support' that may exist in a team project called Tailspin
#>
Function Get-TfsArea
{
    [CmdletBinding()]
    [OutputType([Microsoft.TeamFoundation.Server.NodeInfo])]
    Param
    (
        [Parameter(Position=0)]
        [Alias("Path")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [uri]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})]
        [SupportsWildcards()]
        [object]
        $Area = '\**',

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        return _GetCssNodes -Node $Area -Scope Area -Project $Project -Collection $Collection
    }
}
