#=================================
# Team Project cmdlets
#=================================

Function Get-TfsTeamProject
{
    param
    (
        [Parameter(ValueFromPipeline=$true)]
        [string[]] 
        $Name = '*',

        [Parameter()]
        [switch]
        $All,

        [Parameter()]
        [switch]
        $Extended
    )

    Begin
    {
        $tpc = Get-TfsTeamProjectCollection -Current
        $projects = _GetAllProjects $tpc $All
    }

    Process
    {
        foreach($project in $projects)
        {
            foreach($pattern in $Name)
            {
                if ($project.Name -like $pattern)
                {
                    $return = [ordered] @{
                        Name = $project.Name;
                        Uri = $project.Uri;
                        Status = $project.Status
                    }

                    if ($Extended.IsPresent)
                    {
                        $return += _GetExtendedInfo $project.Uri
                    }

                    [PSCustomObject] $return
                } 
            }
        }
    }
}

Function _GetAllProjects
{
    param
    (
        $tpc,

        $All
    )

    Process
    {
        $css = $tpc.GetService([type]'Microsoft.TeamFoundation.Server.ICommonStructureService')

        if ($All.IsPresent)
        {
            return $css.ListAllProjects()
        }
        else
        {
            return $css.ListProjects()
        }
    }
}

Function _GetExtendedInfo
{
    param
    (
        $uri
    )

    Process
    {
        $projectName = $null
        $projectState = $null
        $templateId = $null
        $projectProperties = $null

        $css = $tpc.GetService([type]'Microsoft.TeamFoundation.Server.ICommonStructureService')
        $css.GetProjectProperties($uri, [ref] $projectName, [ref] $projectState, [ref] $templateId, [ref] $projectProperties)
        $projectProperties = _FormatProjectProperties $projectProperties

        $store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')
        $id = $store.Projects[$projectName].Id

        return [ordered] @{
            Id = $id;
            ProcessTemplate = $projectProperties["Process Template"];
            Properties = $projectProperties
        }
    }
}

Function _FormatProjectProperties
{
    param
    (
        $projectProperties
    )

    $return = [ordered] @{}
    $projectProperties | foreach { $return.Add($_.Name, $_.Value) }

    return $return
}
