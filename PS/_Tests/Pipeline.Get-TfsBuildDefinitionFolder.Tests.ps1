. $PSScriptRoot/_TestSetup.ps1

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Integration Tests' {

        It 'Should get all folders' {
            Get-TfsBuildDefinitionFolder -Project $tfsProject `
            | Select-Object -ExpandProperty Name `
            | Should -Be @('\', 'CD Pipelines')
        }

    }
}
