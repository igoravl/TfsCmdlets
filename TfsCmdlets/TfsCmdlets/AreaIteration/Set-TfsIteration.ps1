<#
.SYNOPSIS
    Changes the value of a property of an Iteration.

.PARAMETER Iteration
    ${HelpParam_Iteration}

.PARAMETER NewName
    Specifies the new name of the iteration. Enter only a name, not a path and name. If you enter a path that is different from the path that is specified in the Iteration parameter, Rename-TfsIteration generates an error. To rename and move an item, use the Move-TfsIteration cmdlet.

.PARAMETER MoveBy
    Reorders an iteration by moving it either up or down inside its parent. A positive value moves an iteration down, whereas a negative one moves it up.

.PARAMETER StartDate
    Sets the start date of the iteration. To clear the start date, set it to $null. Note that when clearing a date, both must be cleared at the same time (i.e. setting both StartDate and FinishDate to $null)

.PARAMETER FinishDate
    Sets the finish date of the iteration. To clear the finish date, set it to $null. Note that when clearing a date, both must be cleared at the same time (i.e. setting both StartDate and FinishDate to $null)

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

#>
Function Set-TfsIteration
{
    [CmdletBinding()]
    Param
    (
        [Parameter(Position=0, Mandatory=$true, ValueFromPipeline=$true)]
        [Alias("Path")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})] 
        [object]
        $Iteration = '\**',

        [Parameter()]
        [string]
        $NewName,

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
        $Collection
    )

    Process
    {
        $node = Get-TfsIteration -Iteration $Iteration -Project $Project -Collection $Collection

        if (-not $node)
        {
            throw "Invalid or non-existent iteration $Iteration"
        }

        $cssService = _GetCssService -Project $Project -Collection $Collection
        $cssService4 = _GetCssService -Project $Project -Collection $Collection -Version 4

        if ($NewName)
        {
            $cssService.RenameNode($node.Uri, $NewName)
        }

        if ($MoveBy)
        {
            $cssService.ReorderNode($node.Uri, $MoveBy)
        }

        if ($StartDate -or $FinishDate)
        {
            if (-not $PSBoundParameters.ContainsKey("StartDate"))
            {
                $StartDate = $node.StartDate
            }

            if (-not $PSBoundParameters.ContainsKey("FinishDate"))
            {
                $FinishDate = $node.FinishDate
            }

            [void]$cssService4.SetIterationDates($node.Uri, $StartDate, $FinishDate)
        }

        return $cssService.GetNode($node.Uri)
    }
}
