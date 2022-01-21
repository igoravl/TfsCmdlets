BeforeAll {
    $setupFilePath = (Join-Path $PSCommandPath.Substring(0, $PSCommandPath.IndexOf('_Tests') + 6) '_TestSetup.ps1')
    . $setupFilePath
}

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Integration Tests' {

        BeforeAll  {
            Connect-TfsTeamProjectCollection -Collection $tfsCollectionUrl -PersonalAccessToken $tfsAccessToken
            $PSDefaultParameterValues['*:ErrorAction'] = 'Stop'
        }

        It 'Should get by ID and revision' {
            (Get-TfsWorkItem -ID 150).Rev | Should -Be 3
            (Get-TfsWorkItem -ID 150 -Revision 2).Rev | Should -Be 2
        }

        It 'Should get by AsOf' {
            (Get-TfsWorkItem 150 -AsOf (Get-Date)).Rev | Should -Be 3
            (Get-TfsWorkItem -ID 150 -Revision 2).Rev | Should -Be 2
        }

        It 'Should get deleted WIs' {
            (Get-TfsWorkItem -Deleted).Id | Should -Be 180
        }

        AfterAll {
            Disconnect-TfsTeamProjectCollection
        }
    }
}
