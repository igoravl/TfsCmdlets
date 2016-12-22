<#
.SYNOPSIS
    Deletes one or more Work Item Iterations.

.PARAMETER Iteration
    ${HelpParam_Iteration}

.PARAMETER MoveTo
    Specifies the new iteration path for the work items currently assigned to the iteration being deleted, if any. When omitted, defaults to the root iteration

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

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
        [Alias("Path")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})] 
        [object]
        $Iteration,

        [Parameter(Position=1)]
        [Alias("NewPath")]
        [ValidateScript({ ($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo]) })] 
        [object]
        $MoveTo = '\',

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $iterations = Get-TfsIteration -Iteration $Iteration -Project $Project -Collection $Collection | Sort -Property Path -Descending

        foreach($i in $iterations)
        {
            if ($PSCmdlet.ShouldProcess($i.RelativePath, "Delete Iteration"))
            {
                $projectName = $i.Path.Split("\")[1]
                _DeleteCssNode -Node $i -MoveToNode $MoveTo -Scope Iteration -Project $Project -Collection $Collection
            }
        }
    }
}
