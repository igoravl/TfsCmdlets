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

        It 'Should throw on parameterless invocation' {
            { Get-TfsWiki } | Should -Throw
        }

        It 'Should get all wikis' {
            Get-TfsWiki -Project $tfsProject | Select-Object -ExpandProperty Name | Sort-Object | Should -Be @('Docs', 'TestProject_wiki')
        }

        It 'Should get project wikis' {
            Get-TfsWiki -Project $tfsProject -ProjectWiki | Select-Object -ExpandProperty Name | Sort-Object | Should -Be @('TestProject_wiki')
        }

        AfterAll {
            Disconnect-TfsTeamProjectCollection
        }
    }
}
