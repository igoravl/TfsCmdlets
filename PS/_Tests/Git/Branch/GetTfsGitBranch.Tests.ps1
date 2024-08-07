& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Get by name' {
        # Get-TfsGitBranch
        #  [[-Branch] <Object>]
        #  [-Repository <Object>]
        #  [-Project <Object>]
        #  [-Collection <Object>]
        #  [-Server <Object>] [<CommonParameters>]

        It 'Should return all branches' {
            $result = Get-TfsGitBranch -Repository 'PartsUnlimited' -Project $tfsProject
            $result | Should -BeOfType 'Microsoft.TeamFoundation.SourceControl.WebApi.GitBranchStats'
            $result.Name | Sort-Object | Should -Be @('AddTerms&Conditions', 'CodedUITest', 'demo-start', 'DependencyValidation', 'e2e-complete', 'FixSearchFunctionality', 'IntelliTest', 'LiveUnitTesting', 'master', 'PerfAndLoadTesting', 'sjbdemo')
        }

        It 'Should support wildcards' {
            $result = Get-TfsGitBranch 'd*' -Repository 'PartsUnlimited' -Project $tfsProject
            $result | Should -BeOfType 'Microsoft.TeamFoundation.SourceControl.WebApi.GitBranchStats'
            $result.Name | Sort-Object | Should -Be @('demo-start', 'DependencyValidation')
        }
    }
    
    Context 'Get default' {
        # Get-TfsGitBranch
        #  -Default
        #  [-Repository <Object>]
        #  [-Project <Object>]
        #  [-Collection <Object>]
        #  [-Server <Object>] [<CommonParameters>]

        It 'Should return the default branch' {
            $result = Get-TfsGitBranch -Default -Repository 'PartsUnlimited' -Project $tfsProject
            $result | Should -BeOfType 'Microsoft.TeamFoundation.SourceControl.WebApi.GitBranchStats'
            $result.Name | Should -Be 'master'
        }
    }

    Context 'Type And Format' {
        It 'Should add Project and Repository properties' {
            $result = Get-TfsGitBranch 'master' -Repository 'PartsUnlimited' -Project $tfsProject
            $result.Project | Should -Be $tfsProject
            $result.Repository | Should -Be 'PartsUnlimited'
        }
    }
}