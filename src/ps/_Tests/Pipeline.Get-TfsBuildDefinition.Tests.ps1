. $PSScriptRoot/_TestSetup.ps1

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Integration Tests' {

        It 'Should get pipelines' {
            Get-TfsBuildDefinition -Project $tfsProject `
            | Select-Object -ExpandProperty Name `
            | Should -Be 'PartsUnlimitedE2E'
        }

    }
}
