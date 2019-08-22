<#
.SYNOPSIS
    Deletes one or more Work Item Areas.

.PARAMETER Area
    HELP_PARAM_AREA

.PARAMETER MoveTo
    Specifies the new area path for the work items currently assigned to the area being deleted, if any. When omitted, defaults to the root area

.PARAMETER Project
    HELP_PARAM_PROJECT

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
#>
Function Remove-TfsArea
{
    [CmdletBinding(ConfirmImpact='High', SupportsShouldProcess=$true)]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
        [Alias("Path")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})] 
        [object]
        $Area,

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
        $areas = Get-TfsArea -Area $Area -Project $Project -Collection $Collection | Sort-Object -Property Path -Descending

        foreach($item in $areas)
        {
            $projectName = $item.Path.Split("\\")[1]

            if (-not ($PSCmdlet.ShouldProcess($projectName, "Delete Area '$($item.RelativePath)' and move orphaned work items to area '$MoveTo'")))
            {
                continue
            }

            _DeleteCssNode -Node $item -MoveToNode $MoveTo -Scope Area -Project $projectName -Collection $Collection
        }
    }
}
