<#
.SYNOPSIS
    Create a new Area ("Area Path") in the given Team Project.

.PARAMETER Area
    Specifies the path of the new Area.
    When supplying a path, use a backslash ("\") between the path segments. Leading and trailing backslashes are optional.  The last segment in the path will be the area name.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

#>
Function New-TfsArea
{
    [CmdletBinding()]
    Param
    (
        [Parameter(Mandatory=$true, Position=0)]
        [Alias("Path")]
        [string]
        $Area,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        return _NewCssNode -Path $Area -Scope Area -Project $Project -Collection $Collection
    }
}
