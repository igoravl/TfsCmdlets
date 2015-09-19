<#

.SYNOPSIS
    Renames an Area ("Area Path")

.PARAMETER Area
    ${HelpParam_Area}

.PARAMETER NewName
    Specifies the new name of the area. Enter only a name, not a path and name. If you enter a path that is different from the path that is specified in the Area parameter, Rename-TfsArea generates an error. To rename and move an item, use the Move-TfsArea cmdlet.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

#>
Function Rename-TfsArea
{
    [CmdletBinding()]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
        [ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})] 
        [Alias("Path")]
        [object]
        $Area,

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
        Set-TfsArea -Area $Area -NewName $NewName -Project $Project -Collection $Collection
    }
}
