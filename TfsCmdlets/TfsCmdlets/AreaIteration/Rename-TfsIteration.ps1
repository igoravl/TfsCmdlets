<#
.SYNOPSIS
    Renames an Iteration ("Iteration Path").

.PARAMETER Iteration
    ${HelpParam_Iteration}

.PARAMETER NewName
    Specifies the new name of the iteration. Enter only a name, not a path and name. If you enter a path that is different from the path that is specified in the Iteration parameter, Rename-TfsIteration generates an error. To rename and move an item, use the Move-TfsIteration cmdlet.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

#>
Function Rename-TfsIteration
{
    [CmdletBinding()]
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
        $Collection
    )

    Process
    {
        Set-TfsIteration -Iteration $Iteration -NewName $NewName -Project $Project -Collection $Collection
    }
}
