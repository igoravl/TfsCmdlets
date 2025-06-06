& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Restore by repository object' {
        # Undo-TfsGitRepositoryRemoval
        # [-Repository] <Object>
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should throw on missing required parameters' {
            { Undo-TfsGitRepositoryRemoval } | Should -Throw
        }

        # Note: These tests would require a deleted repository to be available
        # For now, we'll just test parameter validation

        It 'Should throw on invalid repository parameter' {
            { Undo-TfsGitRepositoryRemoval -Repository $null -Project $tfsProject } | Should -Throw
        }
    }

    Context 'Restore by repository ID' {
        # Undo-TfsGitRepositoryRemoval
        # [-Repository] <Object>
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should accept GUID as repository parameter' {
            # This would require an actual deleted repository ID to test
            # For now, just verify the cmdlet exists and accepts parameters
            $repo = '5db56f26-27b9-44a1-b906-814d03982840'
            { Undo-TfsGitRepositoryRemoval -Repository $repo -Project $tfsProject -WhatIf } | Should -Not -Throw
        }
    }
}