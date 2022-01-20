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

        It 'Should not throw on parameterless invocation' {
            { Get-TfsTeamProject } | Should -Not -Throw
        }

        It 'Should get all projects' {
            Get-TfsTeamProject | Select-Object -ExpandProperty Name | Sort-Object | Should -Be @('AgileGit', 'TestProject', 'TfsCmdlets')
        }

        It 'Should not get process info without IncludeDetails' {
            Get-TfsTeamProject | Select-Object -ExpandProperty ProcessTemplate | Should -Be @('(N/A)', '(N/A)', '(N/A)')
        }

        It 'Should get process info with IncludeDetails' {
            Get-TfsTeamProject -IncludeDetails | Select-Object -ExpandProperty ProcessTemplate | Sort-Object | Should -Be @('Agile', 'Agile', 'Scrum')
        }

        It 'Should get deleted projects' {
            Get-TfsTeamProject -Deleted | Select-Object -ExpandProperty Name | Sort-Object | Should -Be @('DeletedProject')
        }

        AfterAll {
            Disconnect-TfsTeamProjectCollection
        }
    }
}
