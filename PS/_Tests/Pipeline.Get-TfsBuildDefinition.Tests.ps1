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

        It 'Should get pipelines' {
            Get-TfsBuildDefinition -Project $tfsProject `
            | Select-Object -ExpandProperty Name `
            | Should -Be 'PartsUnlimitedE2E'
        }

        AfterAll {
            Disconnect-TfsTeamProjectCollection
        }
    }
}
