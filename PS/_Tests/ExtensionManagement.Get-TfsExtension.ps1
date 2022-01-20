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

        It 'Should get by extension id' {
            Get-TfsExtension 'gittools.gitversion' `
            | Select-Object -ExpandProperty ExtensionName `
            | Should -Be 'GitVersion'
        }

        It 'Should get by extension name' {
            Get-TfsExtension 'gitversion' `
            | Select-Object -ExpandProperty ExtensionName `
            | Should -Be 'GitVersion'

            Get-TfsExtension 'gitvers*' `
            | Select-Object -ExpandProperty ExtensionName `
            | Should -Be 'GitVersion'
        }

        It 'Should get by publisher name' {
            Get-TfsExtension -Publisher 'gittools' `
            | Select-Object -ExpandProperty ExtensionName `
            | Should -Be 'GitVersion'

            Get-TfsExtension -Publisher 'gitto*' `
            | Select-Object -ExpandProperty ExtensionName `
            | Should -Be 'GitVersion'
        }

        AfterAll {
            Disconnect-TfsTeamProjectCollection
        }
    }
}
