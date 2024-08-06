& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Integration Tests' {

        It 'Should throw on parameterless invocation' {
            { Get-TfsWiki } | Should -Throw
        }

        It 'Should get all wikis' {
            Get-TfsWiki -Project $tfsProject | Select-Object -ExpandProperty Name | Sort-Object | Should -Be @('Docs', "${tfsProject}_wiki")
        }

        It 'Should get project wikis' {
            Get-TfsWiki -Project $tfsProject -ProjectWiki | Select-Object -ExpandProperty Name | Sort-Object | Should -Be @("${tfsProject}_wiki")
        }

    }
}
