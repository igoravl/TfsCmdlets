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
        $teamProject = Get-TfsTeamProject $projectName
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

function _GetTfsTeamSettingsConfigurationService {
    Param(
        [Microsoft.TeamFoundation.Client.TfsTeamProjectCollection] $TfsCollection
        )
    return $TfsCollection.GetService([ Microsoft.TeamFoundation.ProcessConfiguration.Client.TeamSettingsConfigurationService]);
} 
 
function _AddTfsTeamField {
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