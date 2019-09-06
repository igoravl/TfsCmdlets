#define ITEM_TYPE Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode
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
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
        [Alias("Path")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [uri]) -or ($_ -is [type]'ITEM_TYPE')})]
        [object]
        $Area,

        [Parameter(Position=1)]
        [Alias('MoveTo')]
        [ValidateScript({($_ -is [string]) -or ($_ -is [uri]) -or ($_ -is [type]'ITEM_TYPE')})]
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
        return _MoveNode @PSBoundParameters
    }
}
