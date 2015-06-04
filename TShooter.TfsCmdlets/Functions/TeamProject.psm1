#=================================
# Team Project cmdlets
#=================================

Function Get-TfsTeamProject
{
    [CmdletBinding()]
    Param
    (
        [Parameter(Position=0)]
        [string] 
        $Name = '*',

        [Parameter(ValueFromPipeline=$true)]
        [Microsoft.TeamFoundation.Client.TfsTeamProjectCollection]
        $Collection
    )

    Process
    {
        $tpc = Get-TfsTeamProjectCollection $Collection
        $wiStore = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')

        $projects = _GetAllProjects $tpc | ? Name -Like $Name

        foreach($project in $projects)
        {
            $wiStore.Projects[$project.Name]
        }
    }
}

Function _GetAllProjects
{
    param ($tpc)

    $css = $tpc.GetService([type]'Microsoft.TeamFoundation.Server.ICommonStructureService')

    return $css.ListAllProjects() | ? Status -eq WellFormed
}