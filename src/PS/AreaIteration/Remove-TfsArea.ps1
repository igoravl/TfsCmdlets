<#
.SYNOPSIS
    Deletes one or more Work Item Areas.

.PARAMETER Area
    ${HelpParam_Area}

.PARAMETER MoveTo
    Specifies the new area path for the work items currently assigned to the area being deleted, if any. When omitted, defaults to the root area

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

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
        $Areas = Get-TfsArea -Area $Area -Project $Project -Collection $Collection | Sort -Property Path -Descending

        foreach($i in $Areas)
        {
            if ($PSCmdlet.ShouldProcess($i.RelativePath, "Delete Area"))
            {
                $projectName = $i.Path.Split("\")[1]
                _DeleteCssNode -Node $i -MoveToNode $MoveTo -Scope Area -Project $projectName -Collection $Collection
            }
        }
    }
}
