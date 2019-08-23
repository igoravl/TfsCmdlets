<#
.SYNOPSIS
    Creates a new Work Item Iteration in the given Team Project.

.PARAMETER Iteration
    Specifies the path of the new Iteration. When supplying a path, use a backslash ("\\") between the path segments. Leading and trailing backslashes are optional. The last segment in the path will be the iteration name.

.PARAMETER StartDate
    Specifies the start of a timed iteration, such as a sprint. Enter a string that represents the date and time, such as "12/01/2015" or a DateTime object, such as one from a Get-Date command. When omitted, no start date is set.

.PARAMETER FinishDate
    Specifies the end of a timed iteration, such as a sprint. Enter a string that represents the date and time, such as "12/01/2015" or a DateTime object, such as one from a Get-Date command. When omitted, no finish date is set.

.PARAMETER Project
    HELP_PARAM_PROJECT

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
    System.String
#>
Function New-TfsIteration
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType('Microsoft.TeamFoundation.Server.NodeInfo')]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
        [Alias("Path")]
        [string]
        $Iteration,

        [Parameter()]
        [DateTime]
        $StartDate,
    
        [Parameter()]
        [DateTime]
        $FinishDate,

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
        if($PSCmdlet.ShouldProcess($Iteration, 'Create iteration path'))
        {
            $node = _NewCssNode -Path $Iteration -Scope Iteration -Project $Project -Collection $Collection -StartDate $StartDate -FinishDate $FinishDate

            if ($Passthru)
            {
                return $node
            }
        }
    }
}
