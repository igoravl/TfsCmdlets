& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context '__AllParameterSets' {
        # Get-TfsGitBranchPolicy
        # [[-PolicyType] <Object>]
        # [-Branch <Object>]
        # [-Repository <Object>]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should return all branch policies' {
            $result = Get-TfsGitBranchPolicy -Branch 'master' -Repository 'PartsUnlimited' -Project $tfsProject
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration'
            $result.DisplayName | Sort-Object | Should -Be @('Build', 'Minimum number of reviewers', 'Require a merge strategy', 'Status')
        }

        It 'Should return a single policy' {
            $result = Get-TfsGitBranchPolicy 'Build' -Branch 'master' -Repository 'PartsUnlimited' -Project $tfsProject
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration'
            $result.DisplayName | Should -Be 'Build'
        }

        It 'Should support wildcards' {
            $result = Get-TfsGitBranchPolicy 'B*' -Branch 'master' -Repository 'PartsUnlimited' -Project $tfsProject
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration'
            $result.DisplayName | Should -Be @('Build')
        }
    } 
}
