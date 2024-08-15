& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context '__AllParameterSets' {
        # Get-TfsTeamAdmin
        # [[-Admin] <string>]
        # [-Team <Object>]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should return all team admins' {
            $result = Get-TfsTeamAdmin -Project $tfsProject -Team "$tfsProject Team"
            $result | Should -BeOfType 'Microsoft.VisualStudio.Services.Identity.Identity'
            $result.DisplayName | Sort-Object | Should -Be @('Igor Abade (T-Shooter)')
        }

        It 'Should return a single team admin by name' {
            $result = Get-TfsTeamAdmin -Admin 'Igor Abade (T-Shooter)' -Project $tfsProject -Team "$tfsProject Team"
            $result | Should -BeOfType 'Microsoft.VisualStudio.Services.Identity.Identity'
            $result.DisplayName | Should -Be 'Igor Abade (T-Shooter)'
        }

        It 'Should return a single team admin by email' {
            $result = Get-TfsTeamAdmin -Admin 'igor@tshooter.com.br' -Project $tfsProject -Team "$tfsProject Team"
            $result | Should -BeOfType 'Microsoft.VisualStudio.Services.Identity.Identity'
            $result.DisplayName | Should -Be 'Igor Abade (T-Shooter)'
        }

        It 'Should support wildcards' {
            $result = Get-TfsTeamAdmin -Project $tfsProject -Team "$tfsProject Team" -Admin 'Igor A*'
            $result | Should -BeOfType 'Microsoft.VisualStudio.Services.Identity.Identity'
            $result.DisplayName | Should -Be 'Igor Abade (T-Shooter)'
            $result = Get-TfsTeamAdmin -Project $tfsProject -Team "$tfsProject Team" -Admin 'igor@*'
            $result | Should -BeOfType 'Microsoft.VisualStudio.Services.Identity.Identity'
            $result.DisplayName | Should -Be 'Igor Abade (T-Shooter)'
        }
    } 
}