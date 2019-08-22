<#
.SYNOPSIS
Deletes one or more Work Item Iterations.

.PARAMETER Iteration
HELP_PARAM_ITERATION

.PARAMETER MoveTo
Specifies the new iteration path for the work items currently assigned to the iteration being deleted, if any. When omitted, defaults to the root iteration

.PARAMETER Project
HELP_PARAM_PROJECT

.PARAMETER Collection
HELP_PARAM_COLLECTION

.INPUTS
Microsoft.TeamFoundation.WorkItemTracking.Client.Project
System.String
#>
Function Remove-TfsIteration
{
    [CmdletBinding(ConfirmImpact='High', SupportsShouldProcess=$true)]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
        [SupportsWildcards()]
        [Alias("Path")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})] 
        [object]
        $Iteration,

        [Parameter(Position=1)]
        [Alias("NewPath")]
        [ValidateScript({ ($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo]) })] 
        [object]
        $MoveTo = '\\',

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $iterations = Get-TfsIteration -Iteration $Iteration -Project $Project -Collection $Collection | Sort-Object -Property Path -Descending

        foreach($i in $iterations)
        {
            $projectName = $i.Path.Split("\\")[1]

            if (-not ($PSCmdlet.ShouldProcess($projectName, "Delete Iteration '$($i.RelativePath)' and move orphaned work items to iteration '$MoveTo'")))
            {
                continue
            }

            _DeleteCssNode -Node $i -MoveToNode $MoveTo -Scope Iteration -Project $projectName -Collection $Collection
        }
    }
}
