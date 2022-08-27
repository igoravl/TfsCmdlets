. $PSScriptRoot/_TestSetup.ps1

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Integration Tests' {

        It 'Should throw on parameterless invocation' {
            { Get-TfsWiki } | Should -Throw
        }

        It 'Should get all wikis' {
            Get-TfsWiki -Project $tfsProject | Select-Object -ExpandProperty Name | Sort-Object | Should -Be @('Docs', 'TestProject_wiki')
        }

        It 'Should get project wikis' {
            Get-TfsWiki -Project $tfsProject -ProjectWiki | Select-Object -ExpandProperty Name | Sort-Object | Should -Be @('TestProject_wiki')
        }

    }
}
