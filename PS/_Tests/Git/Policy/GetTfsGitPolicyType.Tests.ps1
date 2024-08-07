& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context '__AllParameterSets' {
        # Get-TfsGitPolicyType
        # [[-PolicyType] <Object>]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should return all policy types' {
            $result = Get-TfsGitPolicyType -Project $tfsProject
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Policy.WebApi.PolicyType'
            $result.DisplayName | Sort-Object | Should -Be @('Build', 'Comment requirements', 'Commit author email validation', `
                                                            'File name restriction', 'File size restriction', 'Git repository settings', `
                                                            'GitRepositorySettingsPolicyName', 'Minimum number of reviewers', `
                                                            'Path Length restriction', 'Proof Of Presence', 'Require a merge strategy', `
                                                            'Required reviewers', 'Reserved names restriction', 'Secrets scanning restriction', `
                                                            'Secrets scanning restriction', 'Status', 'Work item linking')
        }

        It 'Should support wildcards' {
            $result = Get-TfsGitPolicyType -Project $tfsProject -PolicyType 'B*'
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Policy.WebApi.PolicyType'
            $result.DisplayName | Should -Be @('Build')
        }

        It 'Should return a single policy type' {
            $result = Get-TfsGitPolicyType -Project $tfsProject -PolicyType 'Build'
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Policy.WebApi.PolicyType'
            $result.DisplayName | Should -Be 'Build'
        } 
    }
}
