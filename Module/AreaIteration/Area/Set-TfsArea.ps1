<#
.SYNOPSIS
Modifies the name and/or the position of a Work Item Area.

.PARAMETER Area
HELP_PARAM_AREA

.PARAMETER NewName
Specifies the new name of the area. Enter only a name, not a path and name. If you enter a path that is different from the path that is specified in the area parameter, Rename-Tfsarea generates an error. To rename and move an item, use the Move-Tfsarea cmdlet.

.PARAMETER MoveBy
Reorders an area by moving it either up or down inside its parent. A positive value moves an area down, whereas a negative one moves it up.

.PARAMETER Project
HELP_PARAM_PROJECT

.PARAMETER Collection
HELP_PARAM_COLLECTION

.INPUTS
Microsoft.TeamFoundation.WorkItemTracking.Client.Project
System.String
#>
Function Set-TfsArea
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType('Microsoft.TeamFoundation.Server.NodeInfo')]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
        [ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})] 
        [Alias("Path")]
        [object]
        $Area,

        [Parameter()]
        [string]
        $NewName,

        [Parameter()]
        [int]
        $MoveBy,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $node = Get-TfsArea -Area $Area -Project $Project -Collection $Collection

        if (-not $node)
        {
            throw "Invalid or non-existent area $Area"
        }

        $cssService = _GetCssService -Project $Project -Collection $Collection

        if ($NewName)
        {
            if ($PSCmdlet.ShouldProcess($Area, "Rename area to $NewName"))
            {
                $cssService.RenameNode($node.Uri, $NewName)
            }
        }

        if ($MoveBy)
        {
            if ($PSCmdlet.ShouldProcess($Area, "Reorder area by moving it $MoveBy positions (negative is up, positive is down)"))
            {
                $cssService.ReorderNode($node.Uri, $MoveBy)
            }
        }

        return $cssService.GetNode($node.Uri)
    }
}
