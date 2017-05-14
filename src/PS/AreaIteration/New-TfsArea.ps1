<#
.SYNOPSIS
    Creates a new Work Item Area in the given Team Project.

.PARAMETER Area
    Specifies the path of the new Area. When supplying a path, use a backslash ("\") between the path segments. Leading and trailing backslashes are optional. The last segment in the path will be the area name.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    System.String
#>
Function New-TfsArea
{
    [CmdletBinding(ConfirmImpact='Medium')]
    [OutputType([Microsoft.TeamFoundation.Server.NodeInfo])]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
        [Alias("Path")]
        [string]
        $Area,

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
        $node = _NewCssNode -Path $Area -Scope Area -Project $Project -Collection $Collection

        if ($Passthru)
        {
            return $node
        }
    }
}
