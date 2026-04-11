& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    # Undo-TfsGitRepositoryRemoval
    # [-Repository] <Object>
    # [-Project <Object>]
    # [-Collection <Object>]
    # [-Server <Object>] [<CommonParameters>]

    BeforeAll {
        $repoPrefix = "UndoTest_$([guid]::NewGuid().ToString('N').Substring(0, 8))"
        $repoByName = "${repoPrefix}_ByName"
        $repoPipeline = "${repoPrefix}_Pipeline"
        $repoWhatIf = "${repoPrefix}_WhatIf"
        $repoWildcard = "${repoPrefix}_Wild"

        # Create temporary repositories
        $null = New-TfsGitRepository -Repository $repoByName -Project $tfsProject
        $null = New-TfsGitRepository -Repository $repoPipeline -Project $tfsProject
        $null = New-TfsGitRepository -Repository $repoWhatIf -Project $tfsProject
        $null = New-TfsGitRepository -Repository $repoWildcard -Project $tfsProject

        # Soft-delete them (moves to recycle bin)
        Remove-TfsGitRepository -Repository $repoByName -Project $tfsProject -Confirm:$false
        Remove-TfsGitRepository -Repository $repoPipeline -Project $tfsProject -Confirm:$false
        Remove-TfsGitRepository -Repository $repoWhatIf -Project $tfsProject -Confirm:$false
        Remove-TfsGitRepository -Repository $repoWildcard -Project $tfsProject -Confirm:$false
    }

    AfterAll {
        # Clean up any restored repositories
        @($repoByName, $repoPipeline, $repoWhatIf, $repoWildcard) | ForEach-Object {
            $repo = Get-TfsGitRepository -Repository $_ -Project $tfsProject -ErrorAction SilentlyContinue
            if ($repo) {
                Remove-TfsGitRepository -Repository $_ -Project $tfsProject -Hard -Force
            }
        }

        # Clean up any still-deleted repositories from the recycle bin
        Get-TfsGitRepository -Deleted -Project $tfsProject -ErrorAction SilentlyContinue | Where-Object {
            $_.Name -like "${repoPrefix}_*"
        } | ForEach-Object {
            Remove-TfsGitRepository -Repository $_.Id -Project $tfsProject -Hard -Force -ErrorAction SilentlyContinue
        }
    }

    Context 'Restore deleted repository by name' {

        It 'Should restore a deleted repository by name' {
            $repo = Undo-TfsGitRepositoryRemoval -Repository $repoByName -Project $tfsProject
            $repo | Should -BeOfType 'Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository'
            $repo.Name | Should -Be $repoByName
        }

        It 'Should make the repository visible again after restore' {
            $repo = Get-TfsGitRepository -Repository $repoByName -Project $tfsProject
            $repo | Should -Not -BeNullOrEmpty
            $repo.Name | Should -Be $repoByName
        }
    }

    Context 'Restore deleted repository from pipeline' {

        It 'Should restore a deleted repository piped from Get-TfsGitRepository -Deleted' {
            $repo = Get-TfsGitRepository -Repository $repoPipeline -Deleted -Project $tfsProject | Undo-TfsGitRepositoryRemoval -Project $tfsProject
            $repo | Should -BeOfType 'Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository'
            $repo.Name | Should -Be $repoPipeline
        }
    }

    Context 'Restore with wildcard' {

        It 'Should restore deleted repositories matching a wildcard pattern' {
            $repo = Undo-TfsGitRepositoryRemoval -Repository "${repoPrefix}_Wild*" -Project $tfsProject
            $repo | Should -BeOfType 'Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository'
            $repo.Name | Should -Be $repoWildcard
        }
    }

    Context 'WhatIf support' {

        It 'Should not restore when using -WhatIf' {
            Undo-TfsGitRepositoryRemoval -Repository $repoWhatIf -Project $tfsProject -WhatIf *>$null
            $deleted = Get-TfsGitRepository -Repository $repoWhatIf -Deleted -Project $tfsProject
            $deleted | Should -Not -BeNullOrEmpty
            $deleted.Name | Should -Be $repoWhatIf
        }
    }
}

