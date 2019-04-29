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
Function Rename-TfsIteration
{
    [CmdletBinding(ConfirmImpact='Medium')]
    [OutputType([Microsoft.TeamFoundation.Server.NodeInfo])]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
        [ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})] 
        [Alias("Path")]
        [object]
        $Iteration,

        [Parameter(Position=1)]
        [string]
        $NewName,

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
        $result = Set-TfsIteration -Iteration $Iteration -NewName $NewName -Project $Project -Collection $Collection

        if ($Passthru)
        {
            return $result
        }
    }
}
