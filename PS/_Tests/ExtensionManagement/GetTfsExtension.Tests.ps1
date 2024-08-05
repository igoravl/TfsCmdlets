& "$($PSScriptRoot.Split('_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Integration Tests' {

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

    }
}
