<#
.SYNOPSIS
    Moves a Work Item Area from its parent area to another one in the same Team Project.

.PARAMETER Area
    HELP_PARAM_AREA

.PARAMETER Destination
    Specifies the name, URI or path of an Area Path that will become the new parent of the given source area

.PARAMETER Project
    HELP_PARAM_PROJECT

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
    Microsoft.TeamFoundation.Server.NodeInfo
    System.String
    System.Uri
#>
Function Move-TfsArea
{
    [CmdletBinding(ConfirmImpact='Medium')]
    [OutputType('Microsoft.TeamFoundation.Server.NodeInfo')]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
        [Alias("Path")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [uri]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})]
        [object]
        $Area,

        [Parameter(Position=1l)]
        [ValidateScript({($_ -is [string]) -or ($_ -is [uri]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})]
        [object]
        $Destination,

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
        $node = Get-TfsArea -Area $Area -Project $Project -Collection $Collection

        if (-not $node)
        {
            throw "Invalid or non-existent area $Area"
        }

        $destinationNode = Get-TfsArea -Area $Destination -Project $Project -Collection $Collection

        if (-not $node)
        {
            throw "Invalid or non-existent destination area $Destination"
        }

        $cssService = _GetCssService -Project $Project -Collection $Collection
        $cssService.MoveBranch($node.Uri, $destinationNode.Uri)
        $node = $cssService.GetNode($node.Uri)

        if ($Passthru)
        {
            return $node
        }
    }
}
