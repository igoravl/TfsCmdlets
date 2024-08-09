& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    $commitSha = ''

    Context 'Get by commit SHA' {
        # Get-TfsGitCommit
        # [-Commit] <Object>
        # [-IncludeLinks]
        # [-IncludePushData]
        # [-IncludeUserImageUrl]
        # [-Repository <Object>]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should return a commit object' {
            $commit = Get-TfsGitCommit -Commit $commitSha -Repository 'PartsUnlimited' -Project $tfsProject
            $commit | Should -BeOfType [Microsoft.TeamFoundation.SourceControl.WebApi.GitCommitRef]
        }

        It 'Should include links' {
            $commit = Get-TfsGitCommit -Commit $commitSha -Repository 'PartsUnlimited' -Project $tfsProject -IncludeLinks
            $commit | Should -BeOfType [Microsoft.TeamFoundation.SourceControl.WebApi.GitCommitRef]
            $commit.Links | Should -Not -BeNullOrEmpty
        }

        It 'Should include push data' {
            $commit = Get-TfsGitCommit -Commit $commitSha -Repository 'PartsUnlimited' -Project $tfsProject -IncludePushData
            $commit | Should -BeOfType [Microsoft.TeamFoundation.SourceControl.WebApi.GitCommitRef]
            $commit.Push | Should -Not -BeNullOrEmpty
        }

        It 'Should include user image URL' {
            $commit = Get-TfsGitCommit -Commit $commitSha -Repository 'PartsUnlimited' -Project $tfsProject -IncludeUserImageUrl
            $commit | Should -BeOfType [Microsoft.TeamFoundation.SourceControl.WebApi.GitCommitRef]
            $commit.Author.ImageUrl | Should -Not -BeNullOrEmpty
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