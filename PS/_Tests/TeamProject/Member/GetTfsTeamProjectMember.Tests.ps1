& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context '__AllParameterSets' {
        # Get-TfsTeamProjectMember
        # [-Member <Object>]
        # [-AsIdentity]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should return all members' {
            $result = Get-TfsTeamProjectMember -Project $tfsProject
            $result | Should -BeOfType 'TfsCmdlets.Models.TeamProjectMember'
            $result.Name | Sort-Object | Should -Be @('Brian Keller', 'Igor Abade (T-Shooter)')
        }

        It 'Should return a single member by name' {
            $result = Get-TfsTeamProjectMember -Member 'Igor Abade (T-Shooter)' -Project $tfsProject
            $result | Should -BeOfType 'TfsCmdlets.Models.TeamProjectMember'
            $result.Name | Should -Be 'Igor Abade (T-Shooter)'
        }

        It 'Should return a single member by email' {
            $result = Get-TfsTeamProjectMember -Member 'igor@tshooter.com.br' -Project $tfsProject
            $result | Should -BeOfType 'TfsCmdlets.Models.TeamProjectMember'
            $result.Name | Should -Be 'Igor Abade (T-Shooter)'
        }

        It 'Should support wildcards' {
            $result = Get-TfsTeamProjectMember -Project $tfsProject -Member 'Brian K*'
            $result | Should -BeOfType 'TfsCmdlets.Models.TeamProjectMember'
            $result.Name | Should -Be 'Brian Keller'
            $result = Get-TfsTeamProjectMember -Project $tfsProject -Member 'brian@*'
            $result | Should -BeOfType 'TfsCmdlets.Models.TeamProjectMember'
            $result.Name | Should -Be 'Brian Keller'
        }

        It 'Should return as identity' {
            $result = Get-TfsTeamProjectMember 'Igor Abade (T-Shooter)' -Project $tfsProject -AsIdentity
            $result | Should -BeOfType 'Microsoft.VisualStudio.Services.Identity.Identity'
            $result.DisplayName | Should -Be 'Igor Abade (T-Shooter)'
        }
    } 
}