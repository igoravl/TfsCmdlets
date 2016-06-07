<#
.SYNOPSIS
    Gets one or more Iterations ("Iteration Paths") from a given Team Project.

.PARAMETER Iteration
    ${HelpParam_Iteration}

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String

#>
Function Get-TfsIteration
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
        $Iteration = '\**',

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        return _GetCssNodes -Node $Iteration -Scope Iteration -Project $Project -Collection $Collection
    }
}
