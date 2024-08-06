& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Integration Tests' {

        It 'Should get pipelines' {
            Get-TfsBuildDefinition -Project $tfsProject `
            | Select-Object -ExpandProperty Name `
            | Should -Be 'PartsUnlimitedE2E'
        }

    }
}
