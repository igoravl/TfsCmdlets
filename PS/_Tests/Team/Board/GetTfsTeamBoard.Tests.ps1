& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context '__AllParameterSets' {
        # Get-TfsTeamBoard
        # [[-Board] <Object>]
        # [-Team <Object>]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should return all team boards' {
            $result = Get-TfsTeamBoard -Project $tfsProject -Team "$tfsProject Team"
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Work.WebApi.Board'
            $result.Name | Sort-Object | Should -Be @('Backlog items', 'Epics', 'Features')
        }

        It 'Should return a single team board by name' {
            $result = Get-TfsTeamBoard -Board 'Epics' -Project $tfsProject -Team "$tfsProject Team"
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Work.WebApi.Board'
            $result.Name | Should -Be 'Epics'
        }

        It 'Should support wildcards' {
            $result = Get-TfsTeamBoard 'F*' -Project $tfsProject -Team "$tfsProject Team"
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Work.WebApi.Board'
            $result.Name | Should -Be 'Features'
        }
    } 
}