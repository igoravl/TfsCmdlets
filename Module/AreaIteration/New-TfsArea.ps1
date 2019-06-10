<#
.SYNOPSIS
    Creates a new Work Item Area in the given Team Project.

.PARAMETER Area
    Specifies the path of the new Area. When supplying a path, use a backslash ("\\") between the path segments. Leading and trailing backslashes are optional. The last segment in the path will be the area name.

.PARAMETER Project
    HELP_PARAM_PROJECT

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
    System.String
#>
Function New-TfsArea
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
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
        if($PSCmdlet.ShouldProcess($Area, 'Create Area Path'))
        {
            $node = _NewCssNode -Path $Area -Scope Area -Project $Project -Collection $Collection

            if ($Passthru)
            {
                return $node
            }
        }
    }
}
