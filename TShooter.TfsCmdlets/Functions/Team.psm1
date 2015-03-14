#=====================
# Team cmdlets
#=====================

Function New-TfsTeam
{
    param
    (
        [Parameter(Mandatory=$true)]
        [string] 
        $ProjectName,
    
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [string] 
        $Name,
    
        [Parameter()]
        [string] 
        $Description,
    
        [Parameter()]
        [string] 
        $Picture,
    
        [Parameter()]
        [System.DayOfWeek[]] 
        $WorkingDays,
    
        [Parameter()]
        [switch] 
        $ShowBugsInBacklog,
    
        [Parameter()]
        [switch] 
        $Default
    )

    Process
    {
        $tpc = Get-TfsTeamProjectCollection -Current

        $teamProject = _GetTeamProject $ProjectName
        $team = _CreateTeam $teamProject $Name $Description

        

    }
}

Function Get-TfsTeam
{
    param
    (
        [Parameter(Mandatory=$true)]
        [string] 
        $ProjectName,
    
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [string] 
        $Name
    )

    Process
    {
        $team = _GetTeam $ProjectName $Name

        return [PSCustomObject] [ordered] @{
            Name = $team.Name;
            Description = $team.Description;
            Picture = "(Not Implemented)";
            WorkingDays = "(Not Implemented)";
            TeamFieldValues = "(Not Implemented)";
            ShowBugsInBacklog = "(Not Implemented)";
            Default = "(Not Implemented)";
            Id = $team.Identity.TeamFoundationId
        }
    }
}

Function Set-TfsTeam
{
    param
    (
        [Parameter(Mandatory=$true)]
        [string] 
        $ProjectName,
    
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [string] 
        $Name,
    
        [Parameter()]
        [string] 
        $NewName,
    
        [Parameter()]
        [string] 
        $Description,
    
        [Parameter()]
        [string] 
        $Picture,
    
        [Parameter()]
        [System.DayOfWeek[]] 
        $WorkingDays,
    
        [Parameter()]
        [switch] 
        $ShowBugsInBacklog
    )

    Process
    {
        $team = _GetTeam -ProjectName $ProjectName -Name $Name

        if ($NewName)
        {
        }
    }
}

Function Remove-TfsTeam
{
}

Function Add-TfsTeamFieldValue
{
    
}

Function Set-TfsTeamFieldValue
{

}

Function Remove-TfsTeamFieldValue
{
    
}

# =================
# Helper Functions
# =================

Function _GetTeamProject
{
    param
    (
        [Parameter()]
        [string] 
        $ProjectName
    )

    Process
    {
        $cssService = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService3")
        return $cssService.GetProjectFromName($projectName)
    }
}

Function _GetTeam
{
    param
    (
        [Parameter()]
        [string] 
        $ProjectName,
    
        [Parameter()]
        [string] 
        $Name
    )

    Begin
    {
        $tpc = Get-TfsTeamProjectCollection -Current
        $teamService = $tpc.GetService([type]"Microsoft.TeamFoundation.Client.TfsTeamService")
    }

    Process
    {
        $teamProject = _GetTeamProject $projectName
        $team = ($teamService.QueryTeams($teamProject.Uri) | where Name -eq $name)

        return $team
    }
}

Function _CreateTeam
{
    param
    (
    )

    Process
    {
        $teamService = $tpc.GetService([type]"Microsoft.TeamFoundation.Client.TfsTeamService")
        return $teamService.CreateTeam($teamProject.Uri, $Name, $Description, $null)
    }
}

Function _SetTeamSettings
{
    param
    (
        $team, 
        
        [string[]]
        $teamFieldValues        
    )
}

function Get-TfsTeamSettingsConfigurationService {
    Param(
        [Microsoft.TeamFoundation.Client.TfsTeamProjectCollection] $TfsCollection
        )
    return $TfsCollection.GetService([ Microsoft.TeamFoundation.ProcessConfiguration.Client.TeamSettingsConfigurationService]);
} 
 
function Add-TfsTeamField {
Param(
       [parameter(Mandatory=$true)][Microsoft.TeamFoundation.Client.TfsTeamProjectCollection] $TfsCollection,
       [parameter(Mandatory=$true)][Microsoft.TeamFoundation.Server.ProjectInfo] $TfsTeamProject,
       [parameter(Mandatory=$true)][Microsoft.TeamFoundation.Client.TeamFoundationTeam] $TfsTeam,
       [parameter(Mandatory=$true)][String] $TeamFieldValue
       )
 
    $TfsTeamConfigService = Get-TfsTeamSettingsConfigurationService $TfsCollection
    $TfsTeamConfig = $TfsTeamConfigService.GetTeamConfigurations([Guid[]]($TfsTeam.Identity.TeamFoundationId))
 
    $newTeamFieldValue = New-Object Microsoft.TeamFoundation.ProcessConfiguration.Client.TeamFieldValue
    $newTeamFieldValue.Value = $TeamFieldValue
 
    $TfsTeamConfig.TeamSettings.TeamFieldValues = [Microsoft.TeamFoundation.ProcessConfiguration.Client.TeamFieldValue[]]($newTeamFieldValue)
    $TfsTeamConfig.TeamSettings.BacklogIterationPath = "$($TfsTeamProject.Name)" 
    #$TfsTeamConfig.TeamSettings.IterationPaths = [string[]]("$($TfsTeamProject.Name)")
 
    $TfsTeamConfigService.SetTeamSettings($TfsTeam.Identity.TeamFoundationId,$TfsTeamConfig.TeamSettings)
} 