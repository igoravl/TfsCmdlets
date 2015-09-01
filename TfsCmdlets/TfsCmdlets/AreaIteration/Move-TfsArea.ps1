<#
.SYNOPSIS
    Moves an Area from one parent area to another in the same Team Project.

.PARAMETER Area
    ${HelpParam_Area}

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

#>
Function Move-TfsArea
{
    [CmdletBinding()]
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
        $Collection
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

        return $cssService.GetNode($node.Uri)
    }
}
