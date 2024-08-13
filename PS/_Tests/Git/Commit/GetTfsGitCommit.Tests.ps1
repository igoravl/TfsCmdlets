& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {
    
    BeforeAll {
        $repoName = 'PartsUnlimited'
        $commitSha = 'd682d84dc3a35101d66455bfe1e33fd23cb4369c'
        $commitComment = 'Merged PR 35: code correction'
    }
    
    Context 'Get by commit SHA' {
        # Get-TfsGitCommit
        # [-Commit] <Object>
        # [-Repository <Object>]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should throw on missing required parameters' {
            { Get-TfsGitCommit -Commit $commitSha -Project $tfsProject } | Should -Throw # Missing Repository
            { Get-TfsGitCommit -Commit $commitSha -Repository $repoName } | Should -Throw # Missing Project
        }

        It 'Should return a commit object' {
            $commit = Get-TfsGitCommit -Commit $commitSha -Repository $repoName -Project $tfsProject
            $commit | Should -BeOfType [Microsoft.TeamFoundation.SourceControl.WebApi.GitCommitRef]
            $commit.Comment | Should -Be $commitComment
        }

        It 'Should include links' {
            (Get-TfsGitCommit -Commit $commitSha -Repository $repoName -Project $tfsProject).Links | Should -Not -BeNullOrEmpty
        }

        It 'Should include push data' {
            (Get-TfsGitCommit -Commit $commitSha -Repository $repoName -Project $tfsProject).Push | Should -Not -BeNullOrEmpty
        }

        It 'Should include work items' {
            (Get-TfsGitCommit -Commit $commitSha -Repository $repoName -Project $tfsProject -IncludeWorkItems).WorkItems | Should -Not -BeNullOrEmpty
        }

        It 'Should include user image URL' {
            (Get-TfsGitCommit -Commit $commitSha -Repository $repoName -Project $tfsProject).Author.ImageUrl | Should -Not -BeNullOrEmpty
        }
    }
    
    Context 'Search commits' {
        # Get-TfsGitCommit
        # [-Author <string>]
        # [-Committer <string>]
        # [-CompareVersion <GitVersionDescriptor>]
        # [-FromCommit <string>]
        # [-FromDate <datetime>]
        # [-ItemPath <string>]
        # [-ToCommit <string>]
        # [-ToDate <datetime>]
        # [-ExcludeDeletes]
        # [-IncludeLinks]
        # [-IncludePushData]
        # [-IncludeUserImageUrl]
        # [-ShowOldestCommitsFirst]
        # [-Skip <int>]
        # [-Top <int>]
        # [-Repository <Object>]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]
    }
    
    Context 'Get by tag' {
        # Get-TfsGitCommit
        # -Tag <string>
        # [-Author <string>]
        # [-Committer <string>]
        # [-CompareVersion <GitVersionDescriptor>]
        # [-FromCommit <string>]
        # [-FromDate <datetime>]
        # [-ItemPath <string>]
        # [-ToCommit <string>]
        # [-ToDate <datetime>]
        # [-ExcludeDeletes]
        # [-IncludeLinks]
        # [-IncludePushData]
        # [-IncludeUserImageUrl]
        # [-ShowOldestCommitsFirst]
        # [-Skip <int>]
        # [-Top <int>]
        # [-Repository <Object>]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]
    }
    
    Context 'Get by branch' {
        # Get-TfsGitCommit
        #  -Branch <string>
        #  [-Author <string>]
        #  [-Committer <string>]
        #  [-CompareVersion <GitVersionDescriptor>]
        #  [-FromCommit <string>]
        #  [-FromDate <datetime>]
        #  [-ItemPath <string>]
        #  [-ToCommit <string>]
        #  [-ToDate <datetime>]
        #  [-ExcludeDeletes]
        #  [-IncludeLinks]
        #  [-IncludePushData]
        #  [-IncludeUserImageUrl]
        #  [-ShowOldestCommitsFirst]
        #  [-Skip <int>]
        #  [-Top <int>]
        #  [-Repository <Object>]
        #  [-Project <Object>]
        #  [-Collection <Object>]
        #  [-Server <Object>] [<CommonParameters>]
    }
}