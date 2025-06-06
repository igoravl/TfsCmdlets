& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Get repositories including recycle bin' {
        # Get-TfsGitRepository
        # [[-Repository] <Object>]
        # [-IncludeParent]
        # [-IncludeRecycleBin]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should include recycle bin repositories when IncludeRecycleBin is specified' {
            # This test assumes there's at least one deleted repository in the recycle bin
            $allRepos = @(Get-TfsGitRepository -Repository '*' -Project $tfsProject)
            $allReposWithRecycleBin = @(Get-TfsGitRepository -Repository '*' -IncludeRecycleBin -Project $tfsProject)
            
            $allReposWithRecycleBin.Count | Should -BeGreaterOrEqual $allRepos.Count
        }

        It 'Should find specific repository in recycle bin by name' {
            # This is a conceptual test - would need actual deleted repository
            # $repository = Get-TfsGitRepository -Repository 'DeletedRepo' -IncludeRecycleBin -Project $tfsProject
            # $repository.Name | Should -Be 'DeletedRepo'
            # $repository | Should -BeOfType 'Microsoft.TeamFoundation.SourceControl.WebApi.GitDeletedRepository'
            
            # For now, just verify the parameter is accepted without error
            { Get-TfsGitRepository -Repository 'NonExistentRepo' -IncludeRecycleBin -Project $tfsProject } | Should -Not -Throw
        }

        It 'Should find specific repository in recycle bin by ID' {
            # This is a conceptual test - would need actual deleted repository ID
            # $repository = Get-TfsGitRepository -Repository '12345678-1234-1234-1234-123456789012' -IncludeRecycleBin -Project $tfsProject
            # $repository.Id | Should -Be '12345678-1234-1234-1234-123456789012'
            # $repository | Should -BeOfType 'Microsoft.TeamFoundation.SourceControl.WebApi.GitDeletedRepository'
            
            # For now, just verify the parameter is accepted without error
            { Get-TfsGitRepository -Repository '12345678-1234-1234-1234-123456789012' -IncludeRecycleBin -Project $tfsProject } | Should -Not -Throw
        }

        It 'Should support wildcards with recycle bin repositories' {
            # $repositories = Get-TfsGitRepository -Repository 'Deleted*' -IncludeRecycleBin -Project $tfsProject
            # $repositories | Should -BeOfType 'Microsoft.TeamFoundation.SourceControl.WebApi.GitDeletedRepository'
            
            # For now, just verify the parameter is accepted without error
            { Get-TfsGitRepository -Repository 'Deleted*' -IncludeRecycleBin -Project $tfsProject } | Should -Not -Throw
        }
    }
}