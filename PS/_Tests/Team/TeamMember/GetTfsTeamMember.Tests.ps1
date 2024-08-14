& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context '__AllParameterSets' {
        # Get-TfsTeamMember
        # [[-Member] <string>]
        # [-Team <Object>]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should return all team members' {
            $result = Get-TfsTeamMember -Project $tfsProject -Team "$tfsProject Team"
            $result | Should -BeOfType 'Microsoft.VisualStudio.Services.Identity.Identity'
            $result.DisplayName | Sort-Object | Should -Be @('Brian Keller', 'Igor Abade (T-Shooter)')
        }

        It 'Should return a single team member by name' {
            $result = Get-TfsTeamMember -Member 'Igor Abade (T-Shooter)' -Project $tfsProject -Team "$tfsProject Team"
            $result | Should -BeOfType 'Microsoft.VisualStudio.Services.Identity.Identity'
            $result.DisplayName | Should -Be 'Igor Abade (T-Shooter)'
        }

        It 'Should return a single team member by email' {
            $result = Get-TfsTeamMember -Member 'igor@tshooter.com.br' -Project $tfsProject -Team "$tfsProject Team"
            $result | Should -BeOfType 'Microsoft.VisualStudio.Services.Identity.Identity'
            $result.DisplayName | Should -Be 'Igor Abade (T-Shooter)'
        }

        It 'Should support wildcards' {
            $result = Get-TfsTeamMember -Project $tfsProject -Team "$tfsProject Team" -Member 'Igor A*'
            $result | Should -BeOfType 'Microsoft.VisualStudio.Services.Identity.Identity'
            $result.DisplayName | Should -Be 'Igor Abade (T-Shooter)'
            $result = Get-TfsTeamMember -Project $tfsProject -Team "$tfsProject Team" -Member 'igor@*'
            $result | Should -BeOfType 'Microsoft.VisualStudio.Services.Identity.Identity'
            $result.DisplayName | Should -Be 'Igor Abade (T-Shooter)'
        }
    } 
}