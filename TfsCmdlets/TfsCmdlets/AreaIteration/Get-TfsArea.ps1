<#
.SYNOPSIS
    Gets one or more Areas ("Area Paths") from a given Team Project.

.PARAMETER Area
    ${HelpParam_Area}

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
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
