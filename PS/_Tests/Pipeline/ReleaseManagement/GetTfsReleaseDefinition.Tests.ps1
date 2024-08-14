& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context '__AllParameterSets' {
        # Get-TfsReleaseDefinition
        # [[-Definition] <Object>]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should return all release definitions' {
            $result = Get-TfsReleaseDefinition -Project $tfsProject
            $result | Should -BeOfType 'Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.ReleaseDefinition'
            $result.Name | Sort-Object | Should -Be @('PartsUnlimitedE2E', 'TestProject-CD')
        }

        It 'Should support wildcards' {
            $result = Get-TfsReleaseDefinition -Project $tfsProject -Definition 'Parts*'
            $result | Should -BeOfType 'Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.ReleaseDefinition'
            $result.Name | Should -Be 'PartsUnlimitedE2E'
        }
    } 
}