& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Get by ID or Name' {
        # Get-TfsGitRepository
        # [[-Repository] <Object>]
        # [-IncludeParent]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should return the repository by ID' {
            $repository = Get-TfsGitRepository -Repository '5db56f26-27b9-44a1-b906-814d03982840' -Project $tfsProject
            $repository.Name | Should -Be 'PartsUnlimited'
            $repository | Should -BeOfType 'Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository'
        }

        It 'Should return the repository by Name' {
            $repository = Get-TfsGitRepository -Repository 'PartsUnlimited' -Project $tfsProject
            $repository.Name | Sort-Object | Should -Be 'PartsUnlimited'
            $repository | Should -BeOfType 'Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository'
        }

        It 'Should return multiple repositories by Name' {
            $repositories = Get-TfsGitRepository -Repository 'PartsUnlimited', 'OtherRepo' -Project $tfsProject
            $repositories.Name | Sort-Object | Should -Be @('OtherRepo', 'PartsUnlimited')
        }

        It 'Should support wildcards' {
            $repository = Get-TfsGitRepository -Repository '*Repo' -Project $tfsProject
            $repository.Name | Sort-Object | Should -Be @('DisableRepo', 'OtherRepo')
            $repository | Should -BeOfType 'Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository'
        }

        It 'Should return the parent of a forked repo with IncludeParent' {
            $repository = Get-TfsGitRepository -Repository 'AgileGit' -IncludeParent -Project $tfsProject
            $repository.Name | Should -Be 'AgileGit'
            $repository.ParentRepository | Should -Not -BeNullOrEmpty
            $repository | Should -BeOfType 'Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository'
        }

        It 'Should return the repository by Name with Project' {
            $repository = Get-TfsGitRepository -Repository 'PartsUnlimited' -Project $tfsProject
            $repository.Name | Should -Be 'PartsUnlimited'
            $repository | Should -BeOfType 'Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository'
        }
    }
    
    Context 'Get default' {
        # Get-TfsGitRepository
        #  -Default
        #  [-IncludeParent]
        #  [-Project <Object>]
        #  [-Collection <Object>]
        #  [-Server <Object>] [<CommonParameters>]

        It 'Should return the default repository' {
            $repository = Get-TfsGitRepository -Default -Project $tfsProject
            $repository.Name | Should -Be $tfsProject
            $repository | Should -BeOfType 'Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository'
        }
    }

    Context 'Get repositories including recycle bin' {
        # Get-TfsGitRepository
        # [[-Repository] <Object>]
        # [-IncludeRecycleBin]
        # [-IncludeParent]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should accept IncludeRecycleBin parameter' {
            { Get-TfsGitRepository -Repository '*' -IncludeRecycleBin -Project $tfsProject } | Should -Not -Throw
        }

        It 'Should include recycle bin repositories when IncludeRecycleBin is specified' {
            $allRepos = @(Get-TfsGitRepository -Repository '*' -Project $tfsProject)
            $allReposWithRecycleBin = @(Get-TfsGitRepository -Repository '*' -IncludeRecycleBin -Project $tfsProject)
            
            $allReposWithRecycleBin.Count | Should -BeGreaterOrEqual $allRepos.Count
        }

        It 'Should support wildcards with recycle bin repositories' {
            { Get-TfsGitRepository -Repository 'Test*' -IncludeRecycleBin -Project $tfsProject } | Should -Not -Throw
        }

        It 'Should work with IncludeParent and IncludeRecycleBin together' {
            { Get-TfsGitRepository -Repository '*' -IncludeParent -IncludeRecycleBin -Project $tfsProject } | Should -Not -Throw
        }
    }
}